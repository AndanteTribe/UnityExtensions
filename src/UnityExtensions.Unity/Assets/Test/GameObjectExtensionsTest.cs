#nullable enable

using System;
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

        [Test]
        public void GetHierarchyJsonReturnsJsonNullForNullGameObjectTest()
        {
            GameObject? target = null;

            Assert.That(target.GetHierarchyJson(), Is.EqualTo("null"));
        }

        [Test]
        public void GetHierarchyJsonReturnsJsonNullForDestroyedGameObjectTest()
        {
            var target = new GameObject("Destroyed");
            UnityEngine.Object.DestroyImmediate(target);

            Assert.That(target.GetHierarchyJson(), Is.EqualTo("null"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetHierarchyJsonIncludesRootPathAndLeafTest(bool includeScene)
        {
            var result = JsonUtility.FromJson<HierarchyData>(_targetGameObject.GetHierarchyJson(includeScene));

            Assert.That(result.root, Is.EqualTo(_targetGameObject.GetHierarchyPath(includeScene)));
            Assert.That(result.tree.gameObject.GameObject.Name, Is.EqualTo(_targetGameObject.name));
            Assert.That(result.tree.gameObject.GameObject.IsActive, Is.True);
            Assert.That(result.tree.components, Has.Length.EqualTo(1));
            Assert.That(result.tree.children, Is.Empty);
        }

        [Test]
        public void GetHierarchyJsonPreservesInactiveDescendantsAndSiblingOrderTest()
        {
            var child = new GameObject("Inactive child");
            child.transform.SetParent(_targetGameObject.transform);
            child.SetActive(false);
            var grandchild = new GameObject("Grandchild");
            grandchild.transform.SetParent(child.transform);
            var sibling = new GameObject("Sibling");
            sibling.transform.SetParent(_targetGameObject.transform);
            child.AddComponent<BoxCollider>();
            sibling.AddComponent<BoxCollider>();
            sibling.AddComponent<BoxCollider>();

            var result = JsonUtility.FromJson<HierarchyData>(_targetGameObject.GetHierarchyJson(false));

            Assert.That(result.tree.children, Has.Length.EqualTo(2));
            var first = result.tree.children[0];
            var second = result.tree.children[1];
            Assert.That(first.gameObject.GameObject.Name, Is.EqualTo(child.name));
            Assert.That(first.gameObject.GameObject.IsActive, Is.False);
            Assert.That(first.components, Has.Length.EqualTo(2));
            Assert.That(first.children, Has.Length.EqualTo(1));
            Assert.That(first.children[0].gameObject.GameObject.Name, Is.EqualTo(grandchild.name));
            Assert.That(first.children[0].components, Has.Length.EqualTo(1));
            Assert.That(first.children[0].children, Is.Empty);
            Assert.That(second.gameObject.GameObject.Name, Is.EqualTo(sibling.name));
            Assert.That(second.components, Has.Length.EqualTo(3));
            Assert.That(second.children, Is.Empty);
        }

#if UNITY_EDITOR
        [Test]
        public void GetHierarchyJsonIncludesSerializedComponentValuesTest()
        {
            _targetGameObject.transform.localPosition = new Vector3(1, 2, 3);
            var collider = _targetGameObject.AddComponent<BoxCollider>();
            collider.center = new Vector3(4, 5, 6);
            collider.enabled = false;

            var json = _targetGameObject.GetHierarchyJson();

            Assert.That(json, Does.Contain(UnityEditor.EditorJsonUtility.ToJson(_targetGameObject.transform)));
            Assert.That(json, Does.Contain(UnityEditor.EditorJsonUtility.ToJson(collider)));
        }
#endif

        // Only the fields relevant to the assertions are deserialized.
        // The native Editor serializer and runtime scene serializer use different field names.
        // Field names must match the JSON keys for JsonUtility deserialization.
#pragma warning disable IDE1006
        // ReSharper disable InconsistentNaming
        [Serializable]
        private class HierarchyData
        {
            public string root = null!;
            public NodeData tree = null!;
        }

        [Serializable]
        private class NodeContents
        {
            public GameObjectWrapper gameObject = null!;
            public ComponentData[] components = null!;
        }

        // Match the fixture's three levels without a recursive type that exceeds Unity's serialization depth limit.
        [Serializable]
        private class NodeData : NodeContents
        {
            public ChildData[] children = null!;
        }

        [Serializable]
        private class ChildData : NodeContents
        {
            public LeafData[] children = null!;
        }

        [Serializable]
        private class LeafData : NodeContents
        {
            public ComponentData[] children = null!;
        }

        [Serializable]
        private class GameObjectWrapper
        {
            public GameObjectData GameObject = null!;
        }

        [Serializable]
        private class GameObjectData
        {
            public string? m_Name = null;
            public string? name = null;
            public bool m_IsActive = false;
            public bool active = false;

            public string? Name => m_Name ?? name;
            public bool IsActive => m_Name != null ? m_IsActive : active;
        }

        [Serializable]
        private class ComponentData
        {
        }
        // ReSharper restore InconsistentNaming
#pragma warning restore IDE1006
    }
}