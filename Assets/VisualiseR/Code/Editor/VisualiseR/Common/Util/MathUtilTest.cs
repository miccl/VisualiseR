using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    public class MathUtilTest
    {
        [TestFixture]
        public class ComputeSpawnPositionMethod
        {
            [Test]
            public void SimpleSpawnPositions()
            {
                //Start Position
                Vector3 actualStartPosition = MathUtil.ComputeSpawnPosition(5, 0, 2);
                Vector3 expectedStartPosition = new Vector3(5, 2, 0);
                Assert.IsTrue(actualStartPosition == expectedStartPosition);

                //Middle position
                Vector3 actualMiddlePosition = MathUtil.ComputeSpawnPosition(5, 45, 2);
                Vector3 expectedMiddlePosition = new Vector3(3.5f, 2, 3.5f);
                Assert.IsTrue(actualMiddlePosition == expectedMiddlePosition);
                //End position
                Vector3 actualEndPosition = MathUtil.ComputeSpawnPosition(5, 90, 2);
                Vector3 expectedEndPosition = new Vector3(0, 2, 5);
                Assert.IsTrue(actualEndPosition == expectedEndPosition);
            }

            [Test]
            public void EndPosition()
            {
                //Start Position
            }
        }

        [TestFixture]
        public class ComputeSpawnPositionMethodFromFront
        {
            [Test]
            public void SimpleSpawnPositions()
            {
                //Start Position
                Vector3? actualStartPosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 0, 2);
                Vector3 expectedStartPosition = new Vector3(0, 2, 5);
//                StringAssert.AreEqualIgnoringCase(actualStartPosition.ToString(), "hallo");
                AssertVectorsAreEqual(expectedStartPosition, (Vector3) actualStartPosition);
//                //Middle position
                Vector3? actualMiddlePosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 45, 2);
                Vector3 expectedMiddlePosition = new Vector3(-3.5f, 2f, 3.5f);
                AssertVectorsAreEqual(expectedMiddlePosition, (Vector3) actualMiddlePosition);
//                StringAssert.AreEqualIgnoringCase(actualMiddlePosition.ToString(),"E" + expectedMiddlePosition);
//                Assert.IsTrue(actualMiddlePosition == expectedMiddlePosition);

                //End position
                Vector3? actualEndPosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 90, 2);
                Vector3 expectedEndPosition = new Vector3(-5, 2, 0);
                AssertVectorsAreEqual(expectedEndPosition, (Vector3) actualEndPosition);
            }

            [Test]
            public void SimpleSpawnPositions360Degree()
            {
                //Start Position
                Vector3? actualStartPosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 0, 2);
                Vector3 expectedStartPosition = new Vector3(0, 2, 5);
//                StringAssert.AreEqualIgnoringCase(actualStartPosition.ToString(), "hallo");
                AssertVectorsAreEqual(expectedStartPosition, (Vector3) actualStartPosition);
//                //Middle position
                Vector3? actualMiddlePosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 45, 2);
                Vector3 expectedMiddlePosition = new Vector3(-3.5f, 2f, 3.5f);
                AssertVectorsAreEqual(expectedMiddlePosition, (Vector3) actualMiddlePosition);
//                StringAssert.AreEqualIgnoringCase(actualMiddlePosition.ToString(),"E" + expectedMiddlePosition);
//                Assert.IsTrue(actualMiddlePosition == expectedMiddlePosition);

                //End position
                Vector3? actualEndPosition = MathUtil.ComputeSpawnPositionFromStartPosition(5, 90, 2);
                Vector3 expectedEndPosition = new Vector3(-5, 2, 0);
                AssertVectorsAreEqual(expectedEndPosition, (Vector3) actualEndPosition);
            }
        }


        [TestFixture]
        public class ComputeSpawnPositionsWithAngleMethod
        {
            [Test]
            public void Angle30()
            {
                //given
                float spawnDistance = 5;
                float angleBetweenElements = 45;
                int radius = 90;
                float posY = 2;

                //when
                List<Vector3> positions =
                    MathUtil.ComputeSpawnPositionsWithAngle(spawnDistance, angleBetweenElements, radius, posY);

                //then
                Assert.AreEqual(positions.Count, 3);
                AssertVectorsAreEqual(positions[0], new Vector3(0, posY, spawnDistance));
                AssertVectorsAreEqual(positions[1], new Vector3(-3.5f, posY, 3.5f));
                AssertVectorsAreEqual(positions[positions.Count - 1], new Vector3(-spawnDistance, posY, 0));
            }
        }

        [TestFixture]
        public class ComputeSpawnPositionsWithElements
        {
            [Test]
            public void Simple()
            {
                //given
                float spawnDistance = 5;
                int numberOfElements = 3;
                int radius = 90;
                float posY = 2;

                //when
                List<Vector3> positions =
                    MathUtil.ComputeSpawnPositionsWithElements(spawnDistance, numberOfElements, radius, posY);

                //then
                Assert.AreEqual(positions.Count, 3);
                AssertVectorsAreEqual(positions[0], new Vector3(0, posY, spawnDistance));
                AssertVectorsAreEqual(positions[1], new Vector3(-3.5f, posY, 3.5f));
                AssertVectorsAreEqual(positions[positions.Count - 1], new Vector3(-spawnDistance, posY, 0));
            }
        }


        internal static void AssertVectorsAreEqual(Vector3 expectedVector, Vector3 actualVector)
        {
            float tolerance = 0.1f;
            Assert.AreEqual(expectedVector.x, actualVector.x, tolerance);
            Assert.AreEqual(expectedVector.y, actualVector.y, tolerance);
            Assert.AreEqual(expectedVector.z, actualVector.z, tolerance);
        }
    }
}