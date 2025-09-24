using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DirectionTest
    {
    // A Test behaves as an ordinary method
        [Test]
        public void North()
        {
            Assert.AreEqual(new Vector3(0, 1, 0) Direction.North.Vector3);
        }
    }  
}
