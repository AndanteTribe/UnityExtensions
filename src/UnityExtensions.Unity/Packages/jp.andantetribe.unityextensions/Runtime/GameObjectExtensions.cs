#nullable enable

using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// <see cref="GameObject"/>の拡張メソッド.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// ゲームオブジェクトの階層パスを取得する.
        /// </summary>
        /// <param name="gameObject">対象のゲームオブジェクト.</param>
        /// <param name="includeScene">シーン名を含めるかどうか.</param>
        /// <returns>階層パス.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using AndanteTribe.Utils.Unity;
        /// using UnityEngine;
        ///
        /// public class GetHierarchyPathExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         // シーン名を含める (デフォルト)
        ///         Debug.Log(this.gameObject.GetHierarchyPath());
        ///         // シーン名を含めない
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
            if (includeScene)
            {
                var sceneName = gameObject.scene.name;
                sceneName = string.IsNullOrEmpty(sceneName) ? "Unsaved Scene" : sceneName;
                AppendLiteral(ref container, ref written, sceneName);
                AppendLiteral(ref container, ref written, "/");
            }

            GetTransformPath(gameObject.transform, ref container, ref written);
            var result = new string(container.AsSpan()[..written]);
            container.Dispose();
            return result;

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


