#nullable enable

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// Extension methods for <see cref="RectTransform"/>.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Safer size setting than <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SetSizeExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         rectTransform.SetSize(100, 100);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this RectTransform rectTransform, in float width, in float height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// Safer size setting than <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SetSizeExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         var size = new Vector2(100, 100);
        ///         rectTransform.SetSize(size);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="size">The width and height size.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this RectTransform rectTransform, in Vector2 size)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        /// <summary>
        /// Safer width setting than <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SetWidthExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         rectTransform.SetWidth(100);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="width">The width.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWidth(this RectTransform rectTransform, in float width) =>
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        /// <summary>
        /// Safer height setting than <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SetHeightExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         rectTransform.SetHeight(100);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="height">The height.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHeight(this RectTransform rectTransform, in float height) =>
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

        /// <summary>
        /// Safer size retrieval than <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class GetSizeExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         Vector2 size = rectTransform.GetSize();
        ///         Debug.Log("Size : " + size.ToString());
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <returns>The size of the target.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetSize(this RectTransform rectTransform) => rectTransform.rect.size;

        /// <summary>
        /// Stretches <see cref="RectTransform"/> fully (stretch * stretch).
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class SetFullStretchExample : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         RectTransform rectTransform = (RectTransform) transform;
        ///         rectTransform.SetFullStretch();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="left">The offset of the left edge.</param>
        /// <param name="right">The offset of the right edge.</param>
        /// <param name="top">The offset of the top edge.</param>
        /// <param name="bottom">The offset of the bottom edge.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFullStretch(this RectTransform rectTransform, in float left = 0, in float right = 0, in float top = 0, in float bottom = 0)
        {
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.offsetMin = new Vector2(left, bottom);
            rectTransform.offsetMax = new Vector2(-right, -top);
        }

        /// <summary>
        /// Gets the corners of a rectangle calculated in world space.
        /// </summary>
        /// <remarks>
        /// Same as <see cref="RectTransform.GetWorldCorners(Vector3[])"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using System;
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class GetWorldCornersExampleClass : MonoBehaviour
        /// {
        ///     private RectTransform _rectTransform;
        ///
        ///     private void Start()
        ///     {
        ///         _rectTransform = (RectTransform)transform;
        ///         DisplayWorldCorners();
        ///     }
        ///
        ///     private void DisplayWorldCorners()
        ///     {
        ///         var v = (stackalloc Vector3[4]);
        ///         _rectTransform.GetWorldCorners(v);
        ///
        ///         Debug.Log("World Corners");
        ///         for (var i = 0; i < 4; i++)
        ///         {
        ///             Debug.Log("World Corner " + i + " : " + v[i]);
        ///         }
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">The <see cref="Span{T}"/> to store the retrieved corner coordinates.</param>
        /// <returns>The <see cref="ReadOnlySpan{T}"/> containing the retrieved corner coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Vector3> GetWorldCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetWorldCorners with a fourCornersSpan that is empty or has less than 4 elements.");
                return ReadOnlySpan<Vector3>.Empty;
            }

            var result = fourCornersSpan[..4];
            rectTransform.GetCalculateLocalCorners(result);
            var localToWorldMatrix = rectTransform.localToWorldMatrix;
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = localToWorldMatrix.MultiplyPoint(result[i]);
            }
            return result;
        }

        /// <summary>
        /// Gets the corners of a rectangle calculated in local space of the <see cref="RectTransform"/>.
        /// </summary>
        /// <remarks>
        /// Same as <see cref="RectTransform.GetLocalCorners(Vector3[])"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using System;
        /// using UnityExtensions;
        /// using UnityEngine;
        ///
        /// public class GetLocalCornersExampleClass : MonoBehaviour
        /// {
        ///     private RectTransform _rectTransform;
        ///
        ///     private void Start()
        ///     {
        ///         _rectTransform = (RectTransform) transform;
        ///         DisplayLocalCorners();
        ///     }
        ///
        ///     private void DisplayLocalCorners()
        ///     {
        ///         var v = (stackalloc Vector3[4]);
        ///
        ///         _rectTransform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
        ///         _rectTransform.GetLocalCorners(v);
        ///
        ///         Debug.Log("Local Corners");
        ///         for (var i = 0; i < 4; i++)
        ///         {
        ///             Debug.Log("Local Corner " + i + " : " + v[i]);
        ///         }
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">The <see cref="Span{T}"/> to store the retrieved corner coordinates.</param>
        /// <returns>The <see cref="ReadOnlySpan{T}"/> containing the retrieved corner coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Vector3> GetLocalCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
                return ReadOnlySpan<Vector3>.Empty;
            }

            rectTransform.GetCalculateLocalCorners(fourCornersSpan);
            return fourCornersSpan[..4];
        }

        /// <summary>
        /// Gets the corners of a rectangle calculated in local space of the <see cref="RectTransform"/>.
        /// </summary>
        /// <remarks>
        /// Same as <see cref="RectTransform.GetLocalCorners(Vector3[])"/>.
        /// </remarks>
        /// <param name="rectTransform">The target <see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">The <see cref="Span{T}"/> to store the retrieved corner coordinates.</param>
        /// <returns>The <see cref="ReadOnlySpan{T}"/> containing the retrieved corner coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void GetCalculateLocalCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            var rect = rectTransform.rect;
            var (x, y, xMax, yMax) = (rect.x, rect.y, rect.xMax, rect.yMax);
            stackalloc Vector3[] { new(x, y), new(x, yMax), new(xMax, yMax), new(xMax, y) }.TryCopyTo(fourCornersSpan);
        }
    }
}