#nullable enable

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// <see cref="Component"/>の拡張メソッド.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// 安全な<see cref="Component.GetComponent{T}"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using AndanteTribe.Utils.Unity;
        /// using UnityEngine;
        ///
        /// public class SafeGetComponentExample : MonoBehaviour
        /// {
        ///     private HingeJoint _hinge;
        ///
        ///     private void Update()
        ///     {
        ///         hinge ??= this.SafeGetComponent<HingeJoint>();
        ///         hinge.useSpring = false;
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="self">対象の<see cref="Component"/>.</param>
        /// <typeparam name="T">取得したいコンポーネントの型.</typeparam>
        /// <returns>取得したコンポーネントインスタンス.なお取得不可の場合はSystemのnullを返す.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SafeGetComponent<T>(this Component self) where T : Component
        {
            self.gameObject.TryGetComponent<T>(out var component);
            return component;
        }
    }
}
