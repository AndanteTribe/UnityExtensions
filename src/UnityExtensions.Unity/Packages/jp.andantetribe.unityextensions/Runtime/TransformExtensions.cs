#nullable enable

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Transform"/>.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Sets the X component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="x">The X value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetX(this Transform transform, in float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        /// <summary>
        /// Adds to the X component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="x">The X value to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddX(this Transform transform, in float x)
        {
            var position = transform.position;
            position.x += x;
            transform.position = position;
        }

        /// <summary>
        /// Subtracts from the X component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="x">The X value to subtract.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubX(this Transform transform, in float x)
        {
            var position = transform.position;
            position.x -= x;
            transform.position = position;
        }

        /// <summary>
        /// Sets the Y component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="y">The Y value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetY(this Transform transform, in float y)
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
        }

        /// <summary>
        /// Adds to the Y component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="y">The Y value to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddY(this Transform transform, in float y)
        {
            var position = transform.position;
            position.y += y;
            transform.position = position;
        }

        /// <summary>
        /// Subtracts from the Y component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="y">The Y value to subtract.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubY(this Transform transform, in float y)
        {
            var position = transform.position;
            position.y -= y;
            transform.position = position;
        }

        /// <summary>
        /// Sets the Z component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="z">The Z value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetZ(this Transform transform, in float z)
        {
            var position = transform.position;
            position.z = z;
            transform.position = position;
        }

        /// <summary>
        /// Adds to the Z component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="z">The Z value to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddZ(this Transform transform, in float z)
        {
            var position = transform.position;
            position.z += z;
            transform.position = position;
        }

        /// <summary>
        /// Subtracts from the Z component of <see cref="Transform.position"/>.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="z">The Z value to subtract.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubZ(this Transform transform, in float z)
        {
            var position = transform.position;
            position.z -= z;
            transform.position = position;
        }
    }
}