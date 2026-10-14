#nullable enable

using System;
using NUnit.Framework;
using UnityEngine;
using UnityExtensions.Internal;

namespace UnityExtensions.Tests
{
    public class JsonWriterTest
    {
        [Test]
        public void EmptyWriterReturnsEmptyStringTest()
        {
            using var writer = new JsonWriter();

            Assert.That(writer.ToString(), Is.Empty);
        }

        [Test]
        public void NestedContainersPreserveSeparatorsTest()
        {
            using var writer = new JsonWriter();
            writer.WriteStartArray();
            writer.WriteStartObject();
            writer.WritePropertyName("empty");
            writer.WriteStartArray();
            writer.WriteEndArray();
            writer.WritePropertyName("value");
            writer.WriteNullValue();
            writer.WriteEndObject();
            writer.WriteStartObject();
            writer.WriteEndObject();
            writer.WriteStartArray();
            writer.WriteStringValue("first");
            writer.WriteStringValue("second");
            writer.WriteNullValue();
            writer.WriteEndArray();
            writer.WriteEndArray();

            Assert.That(writer.ToString(), Is.EqualTo("[{\"empty\":[],\"value\":null},{},[\"first\",\"second\",null]]"));
        }

        [Test]
        public void RawValuesAreEmbeddedWithoutQuotingTest()
        {
            using var writer = new JsonWriter();
            writer.WriteStartArray();
            writer.WriteRawValue("{\"nested\":[1,true,null]}");
            writer.WriteRawValue("42");
            writer.WriteEndArray();

            Assert.That(writer.ToString(), Is.EqualTo("[{\"nested\":[1,true,null]},42]"));
        }

        [Test]
        public void NullAndDestroyedObjectsWriteJsonNullTest()
        {
            var target = new GameObject("Destroyed");
            UnityEngine.Object.DestroyImmediate(target);
            using var writer = new JsonWriter();
            writer.WriteStartArray();
            writer.WriteObjectValue(null);
            writer.WriteObjectValue(target);
            writer.WriteEndArray();

            Assert.That(writer.ToString(), Is.EqualTo("[null,null]"));
        }

        [Test]
        public void PropertyNamesAreEscapedTest()
        {
            using var writer = new JsonWriter();
            writer.WriteStartObject();
            writer.WritePropertyName("\"\\\t");
            writer.WriteStringValue("");
            writer.WriteEndObject();

            Assert.That(writer.ToString(), Is.EqualTo("{\"\\\"\\\\\\t\":\"\"}"));
        }

        [Test]
        public void StringsEscapeControlCharactersAndSurrogatesTest()
        {
            using var writer = new JsonWriter();
            writer.WriteStringValue("\"\\\b\f\n\r\t\0\u001f日本語😀\ud800");

            Assert.That(writer.ToString(), Is.EqualTo("\"\\\"\\\\\\b\\f\\n\\r\\t\\u0000\\u001f日本語\\ud83d\\ude00\\ud800\""));
        }

        [Test]
        public void BufferGrowthPreservesEarlierValuesAndEscapedSuffixTest()
        {
            var value = new string('x', 1024) + new string('\u0001', 128) + "tail";
            var escaped = new string('x', 1024);
            for (var i = 0; i < 128; i++)
            {
                escaped += "\\u0001";
            }

            using var writer = new JsonWriter();
            writer.WriteStartArray();
            writer.WriteStringValue("prefix");
            writer.WriteStringValue(value);
            writer.WriteEndArray();

            Assert.That(writer.ToString(), Is.EqualTo("[\"prefix\",\"" + escaped + "tail\"]"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposeCanBeCalledTwiceTest(bool writeValue)
        {
            var writer = new JsonWriter();
            if (writeValue)
            {
                writer.WriteNullValue();
            }

            writer.Dispose();
            writer.Dispose();
        }

        [Test]
        public void ReadingDisposedWriterThrowsTest()
        {
            var exception = Assert.Throws<ObjectDisposedException>(() =>
            {
                var writer = new JsonWriter();
                writer.WriteNullValue();
                writer.Dispose();
                _ = writer.ToString();
            });

            Assert.That(exception!.ObjectName, Is.EqualTo(nameof(JsonWriter)));
        }

        [Test]
        public void WritingDisposedWriterThrowsTest()
        {
            var exception = Assert.Throws<ObjectDisposedException>(() =>
            {
                var writer = new JsonWriter();
                writer.Dispose();
                writer.WriteStringValue("value");
            });

            Assert.That(exception!.ObjectName, Is.EqualTo(nameof(JsonWriter)));
        }
    }
}
