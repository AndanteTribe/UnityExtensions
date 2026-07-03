#nullable enable

using NUnit.Framework;
using UnityEngine;

namespace UnityExtensions.Tests
{
    public class VectorExtensionsTest
    {
        [Test]
        public void XZTest()
        {
            var target = new Vector3(1f, 2f, 3f);

            Assert.That(target.XZ(), Is.EqualTo(new Vector2(1f, 3f)));
        }

        [Test]
        public void YZTest()
        {
            var target = new Vector3(1f, 2f, 3f);

            Assert.That(target.YZ(), Is.EqualTo(new Vector2(2f, 3f)));
        }
    }
}