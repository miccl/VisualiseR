using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using VisualiseR.Presentation;

namespace Test.Common.Model
{
    public class PositionTest
    {
        [TestFixture]
        public class GetRowSeatPositionsMethod
        {
            [Test]
            public void HappyPath()
            {
                //given
                Vector3 firstSeatPosition = new Vector3(11.7f, 2.6f, 4.0f);
                Vector3 lastSeatPosition = new Vector3(11.7f + 200f, 2.6f, 4.0f);
                int amountOfSeats = 5;
                //when
                List<Vector3> seatPositions =
                    Positions.GetRowSeatPositions(firstSeatPosition, lastSeatPosition, amountOfSeats);

                //then
                Assert.AreEqual(5, seatPositions.Count);
                AssertVectorsAreEqual(seatPositions[0], new Vector3(11.7f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[1], new Vector3(11.7f +50f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[2], new Vector3(11.7f + 100f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[3], new Vector3(11.7f + 150f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[4], new Vector3(11.7f + 200f, 2.6f, 4.0f));
            }
            
            [Test]
            public void FirstSeatBiggerThenLastSeat()
            {
                //given
                Vector3 firstSeatPosition = new Vector3(11.7f, 2.6f, 4.0f);
                Vector3 lastSeatPosition = new Vector3(11.7f - 200f, 2.6f, 4.0f);
                int amountOfSeats = 5;
                //when
                List<Vector3> seatPositions =
                    Positions.GetRowSeatPositions(firstSeatPosition, lastSeatPosition, amountOfSeats);

                //then
                Assert.AreEqual(5, seatPositions.Count);
                AssertVectorsAreEqual(seatPositions[0], new Vector3(11.7f - 200f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[1], new Vector3(11.7f - 150f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[2], new Vector3(11.7f - 100f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[3], new Vector3(11.7f -50f, 2.6f, 4.0f));
                AssertVectorsAreEqual(seatPositions[4], new Vector3(11.7f, 2.6f, 4.0f));
            }
            
            internal static void AssertVectorsAreEqual(Vector3 actualVector, Vector3 expectedVector)
            {
                float tolerance = 0.1f;
                Assert.AreEqual(expectedVector.x, actualVector.x, tolerance,
                    "wrong x vector (expected: " + expectedVector.x + ")");
                Assert.AreEqual(expectedVector.y, actualVector.y, tolerance, "wrong y vector");
                Assert.AreEqual(expectedVector.z, actualVector.z, tolerance, "wrong z vector");
            }

        }
    }
}