#nullable enable

using NUnit.Framework;
using UnityEngine;

namespace UnityExtensions.Tests
{
    public class ComponentExtensionsTest
    {
        private GameObject _targetGameObject = null!;

        [SetUp]
        public void SetUp()
        {
            _targetGameObject = new GameObject("Component Extension Test");
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(_targetGameObject);
        }

        [Test]
        public void SafeGetComponentReturnsExistingComponentTest()
        {
            var expected = _targetGameObject.AddComponent<BoxCollider>();

            var actual = _targetGameObject.transform.SafeGetComponent<BoxCollider>();

            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public void SafeGetComponentReturnsNullWhenComponentIsMissingTest()
        {
            var actual = _targetGameObject.transform.SafeGetComponent<BoxCollider>();

            Assert.That(actual == null, Is.True);
        }
    }
}
