#nullable enable

using NUnit.Framework;
using UnityEngine;

namespace UnityExtensions.Tests
{
    public class TransformExtensionsTest
    {
        private GameObject _targetGameObject = null!;
        private Transform _transform = null!;

        [SetUp]
        public void SetUp()
        {
            _targetGameObject = new GameObject("Transform Extension Test");
            _transform = _targetGameObject.transform;
            _transform.position = new Vector3(10f, 20f, 30f);
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(_targetGameObject);
        }

        [Test]
        public void SetPosXTest()
        {
            _transform.SetPosX(-1f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(-1f, 20f, 30f)));
        }

        [Test]
        public void AddPosXTest()
        {
            _transform.AddPosX(2f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(12f, 20f, 30f)));
        }

        [Test]
        public void SubPosXTest()
        {
            _transform.SubPosX(3f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(7f, 20f, 30f)));
        }

        [Test]
        public void SetPosYTest()
        {
            _transform.SetPosY(-2f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, -2f, 30f)));
        }

        [Test]
        public void AddPosYTest()
        {
            _transform.AddPosY(4f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, 24f, 30f)));
        }

        [Test]
        public void SubPosYTest()
        {
            _transform.SubPosY(5f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, 15f, 30f)));
        }

        [Test]
        public void SetPosZTest()
        {
            _transform.SetPosZ(-3f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, 20f, -3f)));
        }

        [Test]
        public void AddPosZTest()
        {
            _transform.AddPosZ(6f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, 20f, 36f)));
        }

        [Test]
        public void SubPosZTest()
        {
            _transform.SubPosZ(7f);

            Assert.That(_transform.position, Is.EqualTo(new Vector3(10f, 20f, 23f)));
        }
    }
}
