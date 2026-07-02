#nullable enable

using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityExtensions.Tests
{
    public class GameObjectExtensionsTest
    {
        private GameObject _targetGameObject = null!;

        [SetUp]
        public void SetUp()
        {
            _targetGameObject = new GameObject("GameObject Extension Test");
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

            var actual = _targetGameObject.SafeGetComponent<BoxCollider>();

            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public void SafeGetComponentReturnsNullWhenComponentIsMissingTest()
        {
            var actual = _targetGameObject.SafeGetComponent<BoxCollider>();

            Assert.That(actual == null, Is.True);
        }

        [Test]
        public void GetHierarchyPathReturnsEmptyForNullGameObjectTest()
        {
            GameObject? target = null;

            Assert.That(target.GetHierarchyPath(), Is.Empty);
        }

        [Test]
        public void GetHierarchyPathReturnsTransformPathWithoutSceneTest()
        {
            var child = new GameObject("Child");
            var grandChild = new GameObject("GrandChild");
            child.transform.SetParent(_targetGameObject.transform);
            grandChild.transform.SetParent(child.transform);

            var path = grandChild.GetHierarchyPath(false);

            Assert.That(path, Is.EqualTo("GameObject Extension Test/Child/GrandChild"));
        }

        [Test]
        public void GetHierarchyPathIncludesSceneNameTest()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            sceneName = string.IsNullOrEmpty(sceneName) ? "Unsaved Scene" : sceneName;

            var path = _targetGameObject.GetHierarchyPath();

            Assert.That(path, Is.EqualTo(sceneName + "/GameObject Extension Test"));
        }

        [Test]
        public void GetHierarchyPathExpandsStorageForLongNamesTest()
        {
            _targetGameObject.name = "A root name longer than sixteen characters";
            var child = new GameObject("A child name longer than sixteen characters");
            child.transform.SetParent(_targetGameObject.transform);

            var path = child.GetHierarchyPath(false);

            Assert.That(path, Is.EqualTo("A root name longer than sixteen characters/A child name longer than sixteen characters"));
        }
    }
}