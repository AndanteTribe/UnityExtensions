#nullable enable

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Component"/>.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Safe <see cref="Component.GetComponent{T}"/>.
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
        ///         _hinge ??= this.SafeGetComponent<HingeJoint>();
        ///         _hinge.useSpring = false;
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="self">The target <see cref="Component"/>.</param>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <returns>The retrieved component instance. If the component cannot be obtained, returns System's null.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SafeGetComponent<T>(this Component self) where T : Component
        {
            self.gameObject.TryGetComponent<T>(out var component);
            return component;
        }
    }
}