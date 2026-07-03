#nullable enable

using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityExtensions.Tests
{
    public class RectTransformTest
    {
        private RectTransform _rectTransform = null!;
        private GameObject _targetGameObject = null!;

        [SetUp]
        public void SetUp()
        {
            _targetGameObject = new GameObject("Test");
            _rectTransform = _targetGameObject.AddComponent<RectTransform>();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(_targetGameObject);
        }

        [Test]
        [TestCase(100f, 100f)]
        [TestCase(0f, 0f)]
        [TestCase(-100f, -100f)]
        [TestCase(float.MaxValue, float.MinValue)]
        [TestCase(float.Epsilon, -float.Epsilon)]
        [TestCase(0.1f, 0.01f)]
        [TestCase(-0.1f, 1.5f)]
        [TestCase(-50f, 0f)]
        [TestCase(0f, 75f)]
        public void SetSizeFloatTest(float width, float height)
        {
            _rectTransform.SetSize(width, height);
            Assert.That(_rectTransform.rect.width, Is.EqualTo(width));
            Assert.That(_rectTransform.rect.height, Is.EqualTo(height));
        }

        [Test]
        public void SetSizeVector2Test()
        {
            _rectTransform.SetSize(new Vector2(120f, 35f));

            Assert.That(_rectTransform.rect.width, Is.EqualTo(120f));
            Assert.That(_rectTransform.rect.height, Is.EqualTo(35f));
        }

        [Test]
        public void SetWidthAndSetHeightTest()
        {
            _rectTransform.SetSize(10f, 20f);

            _rectTransform.SetWidth(40f);
            _rectTransform.SetHeight(80f);

            Assert.That(_rectTransform.rect.width, Is.EqualTo(40f));
            Assert.That(_rectTransform.rect.height, Is.EqualTo(80f));
        }

        [Test]
        public void GetSizeTest()
        {
            _rectTransform.SetSize(24f, 48f);

            Assert.That(_rectTransform.GetSize(), Is.EqualTo(new Vector2(24f, 48f)));
        }

        [Test]
        public void SetFullStretchTest()
        {
            _rectTransform.SetFullStretch(1f, 2f, 3f, 4f);

            Assert.That(_rectTransform.anchorMin, Is.EqualTo(Vector2.zero));
            Assert.That(_rectTransform.anchorMax, Is.EqualTo(Vector2.one));
            Assert.That(_rectTransform.offsetMin, Is.EqualTo(new Vector2(1f, 4f)));
            Assert.That(_rectTransform.offsetMax, Is.EqualTo(new Vector2(-2f, -3f)));
        }

        [Test]
        public void GetLocalCornersTest()
        {
            _rectTransform.SetSize(10f, 20f);
            var corners = new Vector3[4];
            Span<Vector3> span = corners;

            var result = _rectTransform.GetLocalCorners(span);

            Assert.That(result.Length, Is.EqualTo(4));
            AssertVector3(result[0], new Vector3(-5f, -10f, 0f));
            AssertVector3(result[1], new Vector3(-5f, 10f, 0f));
            AssertVector3(result[2], new Vector3(5f, 10f, 0f));
            AssertVector3(result[3], new Vector3(5f, -10f, 0f));
        }

        [Test]
        public void GetLocalCornersReturnsEmptyWhenSpanIsTooShortTest()
        {
            LogAssert.Expect(LogType.Error, "Calling GetLocalCorners with an array that is null or has less than 4 elements.");
            var corners = new Vector3[3];
            Span<Vector3> span = corners;

            var result = _rectTransform.GetLocalCorners(span);

            Assert.That(result.IsEmpty, Is.True);
        }

        [Test]
        public void GetWorldCornersTest()
        {
            _rectTransform.SetSize(10f, 20f);
            _rectTransform.position = new Vector3(2f, 3f, 4f);
            _rectTransform.rotation = Quaternion.Euler(0f, 0f, 30f);
            var actual = new Vector3[4];
            Span<Vector3> span = actual;
            var expected = new Vector3[4];
            _rectTransform.GetWorldCorners(expected);

            var result = _rectTransform.GetWorldCorners(span);

            Assert.That(result.Length, Is.EqualTo(4));
            for (var i = 0; i < expected.Length; i++)
            {
                AssertVector3(result[i], expected[i]);
            }
        }

        [Test]
        public void GetWorldCornersReturnsEmptyWhenSpanIsTooShortTest()
        {
            LogAssert.Expect(LogType.Error, "Calling GetWorldCorners with a fourCornersSpan that is empty or has less than 4 elements.");
            var corners = Array.Empty<Vector3>();
            Span<Vector3> span = corners;

            var result = _rectTransform.GetWorldCorners(span);

            Assert.That(result.IsEmpty, Is.True);
        }

        private static void AssertVector3(Vector3 actual, Vector3 expected)
        {
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.0001f));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(0.0001f));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.0001f));
        }
    }
}