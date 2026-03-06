#nullable enable

using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// Extension methods for <see cref="GameObject"/>.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Safe <see cref="GameObject.GetComponent{T}"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SafeGetComponentExample : MonoBehaviour
        /// {
        ///     private HingeJoint _hinge;
        ///
        ///     private void Update()
        ///     {
        ///         _hinge ??= this.gameObject.SafeGetComponent<HingeJoint>();
        ///         _hinge.useSpring = false;
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="gameObject">The target <see cref="GameObject"/>.</param>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <returns>The retrieved component instance. If the component cannot be obtained, returns System's null.</returns>
        public static T? SafeGetComponent<T>(this GameObject gameObject) where T : Component
        {
            gameObject.TryGetComponent<T>(out var component);
            return component;
        }

        /// <summary>
        /// Gets the hierarchy path of a game object.
        /// </summary>
        /// <param name="gameObject">The target game object.</param>
        /// <param name="includeScene">Whether to include the scene name.</param>
        /// <returns>The hierarchy path.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class GetHierarchyPathExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         // Include scene name (default)
        ///         Debug.Log(this.gameObject.GetHierarchyPath());
        ///         // Exclude scene name
        ///         Debug.Log(this.gameObject.GetHierarchyPath(false));
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetHierarchyPath(this GameObject? gameObject, bool includeScene = true)
        {
            if (gameObject == null)
            {
                return "";
            }

            var container = new NativeArray<char>(16, Allocator.Temp);
            var written = 0;
            try
            {
                if (includeScene)
                {
                    var sceneName = gameObject.scene.name;
                    sceneName = string.IsNullOrEmpty(sceneName) ? "Unsaved Scene" : sceneName;
                    AppendLiteral(ref container, ref written, sceneName);
                    AppendLiteral(ref container, ref written, "/");
                }

                GetTransformPath(gameObject.transform, ref container, ref written);
                return container.AsSpan()[..written].ToString();
            }
            finally
            {
                container.Dispose();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void GetTransformPath(Transform transform, ref NativeArray<char> container, ref int written)
            {
                if (transform.parent != null)
                {
                    GetTransformPath(transform.parent, ref container, ref written);
                    AppendLiteral(ref container, ref written, "/");
                }
                AppendLiteral(ref container, ref written, transform.name);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void AppendLiteral(ref NativeArray<char> container, ref int written, ReadOnlySpan<char> literal)
            {
                if (literal.TryCopyTo(container.AsSpan()[written..]))
                {
                    written += literal.Length;
                    return;
                }

                var newSize = container.Length + literal.Length;
                var newContainer = new NativeArray<char>(newSize, Allocator.Temp);
                container.AsSpan().CopyTo(newContainer);
                literal.CopyTo(newContainer.AsSpan()[written..]);
                written += literal.Length;
                container.Dispose();
                container = newContainer;
            }
        }
    }
}


