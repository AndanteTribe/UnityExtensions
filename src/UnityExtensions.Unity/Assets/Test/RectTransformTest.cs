#nullable enable

using NUnit.Framework;
using UnityEngine;

namespace UnityExtensions.Tests
{
    public class RectTransformTest
    {
        private RectTransform _rectTransform = null!;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var targetGameObject = new GameObject("Test");
            _rectTransform = targetGameObject.AddComponent<RectTransform>();
        }

        [Test]
        [TestCase(100f,100f)]
        [TestCase(0f,0f)]
        [TestCase(-100f, -100f)]
        [TestCase(float.MaxValue, float.MinValue)]
        [TestCase(float.Epsilon, -float.Epsilon)]
        [TestCase(0.1f,0.01f)]
        [TestCase(-0.1f,1.5f)]
        [TestCase(-50f,0f)]
        [TestCase(0f,75f)]
        public void SetSizeFloatTest(float width, float height)
        {
            _rectTransform.SetSize(width, height);
            Assert.That(_rectTransform.rect.width, Is.EqualTo(width));
            Assert.That(_rectTransform.rect.height, Is.EqualTo(height));
        }
    }
}
