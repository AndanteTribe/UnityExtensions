#nullable enable

using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityExtensions.Internal
{
    /// <summary>
    /// Writes compact JSON to a temporary native character buffer.
    /// </summary>
    /// <remarks>
    /// Dispose within the current frame. Do not copy this writer after writing to it.
    /// Callers must balance containers and write a property name before each object value;
    /// JSON structure and raw values are not validated.
    /// </remarks>
    internal ref struct JsonWriter
    {
        private NativeArray<char> _buffer;
        private int _written;
        private bool _needsComma;
        private bool _disposed;

        public void WriteStartObject()
        {
            EnsureCapacity(_needsComma ? 2 : 1);
            if (_needsComma)
            {
                _buffer[_written++] = ',';
            }

            _buffer[_written++] = '{';
            _needsComma = false;
        }

        public void WriteEndObject()
        {
            EnsureCapacity(1);
            _buffer[_written++] = '}';
            _needsComma = true;
        }

        public void WriteStartArray()
        {
            EnsureCapacity(_needsComma ? 2 : 1);
            if (_needsComma)
            {
                _buffer[_written++] = ',';
            }

            _buffer[_written++] = '[';
            _needsComma = false;
        }

        public void WriteEndArray()
        {
            EnsureCapacity(1);
            _buffer[_written++] = ']';
            _needsComma = true;
        }

        public void WritePropertyName(ReadOnlySpan<char> name)
        {
            if (_needsComma)
            {
                EnsureCapacity(1);
                _buffer[_written++] = ',';
            }

            WriteQuoted(name);
            EnsureCapacity(1);
            _buffer[_written++] = ':';
            _needsComma = false;
        }

        public void WriteStringValue(ReadOnlySpan<char> value)
        {
            if (_needsComma)
            {
                EnsureCapacity(1);
                _buffer[_written++] = ',';
            }

            WriteQuoted(value);
            _needsComma = true;
        }

        public void WriteNullValue()
        {
            EnsureCapacity(_needsComma ? 5 : 4);
            if (_needsComma)
            {
                _buffer[_written++] = ',';
            }

            "null".AsSpan().CopyTo(_buffer.AsSpan()[_written..]);
            _written += 4;
            _needsComma = true;
        }

        /// <summary>
        /// Writes a serialized Unity object, or JSON null for a null or destroyed object.
        /// </summary>
        /// <remarks>
        /// Uses EditorJsonUtility in the Editor. In players, uses com.unity.runtime-scene-serialization
        /// when the package is installed, otherwise JsonUtility. The JsonUtility fallback supports
        /// only MonoBehaviour and ScriptableObject among Unity object types.
        /// </remarks>
        public void WriteObjectValue(UnityEngine.Object? value)
        {
            if (value == null)
            {
                WriteNullValue();
                return;
            }

#if UNITY_EDITOR
            WriteRawValue(UnityEditor.EditorJsonUtility.ToJson(value));
#else
#if ENABLE_SERIALIZTION
            // Select scene-object adapters instead of the UnityEngine.Object reference adapter.
            var json = value switch
            {
                UnityEngine.GameObject gameObject => Unity.RuntimeSceneSerialization.SceneSerialization.ToJson(gameObject),
                UnityEngine.Component component => Unity.RuntimeSceneSerialization.SceneSerialization.ToJson(component),
                _ => Unity.RuntimeSceneSerialization.SceneSerialization.ToJson(value)
            };
#else
            var json = UnityEngine.JsonUtility.ToJson(value);
#endif
            WriteStartObject();
            WritePropertyName(value.GetType().Name);
            WriteRawValue(json);
            WriteEndObject();
#endif
        }

        /// <summary>
        /// Writes one complete JSON value, such as the result of JsonUtility.ToJson, without validation.
        /// </summary>
        public void WriteRawValue(ReadOnlySpan<char> json)
        {
            EnsureCapacity(checked(json.Length + (_needsComma ? 1 : 0)));
            if (_needsComma)
            {
                _buffer[_written++] = ',';
            }

            json.CopyTo(_buffer.AsSpan()[_written..]);
            _written += json.Length;
            _needsComma = true;
        }

        public override string ToString()
        {
            if (_disposed)
            {
                ThrowObjectDisposedException();
            }

            return _buffer.IsCreated ? _buffer.AsSpan()[.._written].ToString() : string.Empty;
        }

        public void Dispose()
        {
            if (_buffer.IsCreated)
            {
                _buffer.Dispose();
                _buffer = default;
            }

            _disposed = true;
        }

        private void WriteQuoted(ReadOnlySpan<char> value)
        {
            EnsureCapacity(checked(value.Length + 2));
            _buffer[_written++] = '"';
            var start = 0;
            for (var i = 0; i < value.Length; i++)
            {
                var character = value[i];
                if (character != '"' && character != '\\' && character >= ' ' && !char.IsSurrogate(character))
                {
                    continue;
                }

                var literal = value[start..i];
                literal.CopyTo(_buffer.AsSpan()[_written..]);
                _written += literal.Length;
                var escape = character switch
                {
                    '"' => '"',
                    '\\' => '\\',
                    '\b' => 'b',
                    '\f' => 'f',
                    '\n' => 'n',
                    '\r' => 'r',
                    '\t' => 't',
                    _ => 'u'
                };
                // Reserve the escape, the remaining input, and the closing quote.
                EnsureCapacity(checked(value.Length - i + (escape == 'u' ? 6 : 2)));
                _buffer[_written++] = '\\';
                _buffer[_written++] = escape;
                if (escape == 'u')
                {
                    const string hex = "0123456789abcdef";
                    _buffer[_written++] = hex[character >> 12];
                    _buffer[_written++] = hex[(character >> 8) & 15];
                    _buffer[_written++] = hex[(character >> 4) & 15];
                    _buffer[_written++] = hex[character & 15];
                }

                start = i + 1;
            }

            value[start..].CopyTo(_buffer.AsSpan()[_written..]);
            _written += value.Length - start;
            _buffer[_written++] = '"';
        }

        private void EnsureCapacity(int additionalLength)
        {
            if (_disposed)
            {
                ThrowObjectDisposedException();
            }

            var required = checked(_written + additionalLength);
            if (_buffer.IsCreated && required <= _buffer.Length)
            {
                return;
            }

            var capacity = Math.Max(required, _buffer.IsCreated ? checked(_buffer.Length * 2) : 256);
            var buffer = new NativeArray<char>(capacity, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            if (_buffer.IsCreated)
            {
                _buffer.AsSpan()[.._written].CopyTo(buffer.AsSpan());
                _buffer.Dispose();
            }

            _buffer = buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowObjectDisposedException() => throw new ObjectDisposedException(nameof(JsonWriter));
    }
}