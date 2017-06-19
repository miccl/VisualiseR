using System.Collections.Generic;
using UnityEngine;

namespace VisualiseR.Presentation
{
    public static class Positions
    {
        public static readonly Vector3 HOST_POS = new Vector3(11, 5, 17);
        
        public static readonly Vector3 CLIENT_ADJUSTMENT = new Vector3(0, 2, 0);

        public static List<Vector3> GetRowSeatPositions(Vector3 startPosition, Vector3 endPosition, int amountOfSeats)
        {
            List<Vector3> positions = new List<Vector3>();
            if (startPosition.x > endPosition.x)
            {
                var temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                return GetRowSeatPositions(startPosition, endPosition, amountOfSeats);
            }
            Vector3 spaceBetweenSeats = DifferenceBetweenVectors(startPosition, endPosition) / (amountOfSeats - 1);
            var currentPos = startPosition;
            while (startPosition.x <= currentPos.x && currentPos.x <= endPosition.x)
            {
                positions.Add(currentPos + CLIENT_ADJUSTMENT);
                currentPos += spaceBetweenSeats;
            }
            return positions;
        }

        public static Vector3 DifferenceBetweenVectors(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v2.x - v1.x,
                v2.y - v1.y,
                v2.z - v1.z);
        }
    }
}