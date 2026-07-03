#nullable enable

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Vector3"/>.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Extracts the X and Z components from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="target">The target Vector3.</param>
        /// <returns>A <see cref="Vector2"/> where the X component of the target is stored in X, and the Z component is stored in Y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZ(in this Vector3 target) => new Vector2(target.x, target.z);

        /// <summary>
        /// Extracts the Y and Z components from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="target">The target Vector3.</param>
        /// <returns>A <see cref="Vector2"/> where the Y component of the target is stored in X, and the Z component is stored in Y.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZ(in this Vector3 target) => new Vector2(target.y, target.z);
    }
}