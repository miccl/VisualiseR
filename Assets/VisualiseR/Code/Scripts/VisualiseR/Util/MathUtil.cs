using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace VisualiseR.Util
{
    public static class MathUtil
    {
        public static List<Vector3> ComputeSpawnPositionsWithElements(float spawnDistance, int numberOfElements,
            int radius, float startAngle, float posY)
        {
            List<Vector3> positions = new List<Vector3>();

            float angleBetweenElements = radius / (float) (numberOfElements - 1);
            float currentAngle = 0;
            for (int i = 0; i < numberOfElements; i++)
            {
                Vector3? currPos = ComputeSpawnPositionFromStartPosition(spawnDistance, currentAngle, startAngle, posY);
                if (currPos == null)
                {
                    break;
                }
                positions.Add((Vector3) currPos);
                currentAngle += angleBetweenElements;
            }
            return positions;
        }

        public static List<Vector3> ComputeSpawnPositionsWithAngle(float spawnDistance, float angleBetweenElements,
            int radius, float startAngle, float posY)
        {
            List<Vector3> positions = new List<Vector3>();
            float currAngle = 0;
            while (currAngle <= radius)
            {
                Vector3? currPos = ComputeSpawnPositionFromStartPosition(spawnDistance, currAngle, startAngle, posY);
                if (currPos == null)
                {
                    break;
                }
                positions.Add((Vector3) currPos);
                currAngle += angleBetweenElements;
            }

            return positions;
        }

        public static List<Vector3> ComputeSomething(float spawnDistance, int numberOfElements, float startAngle, float endAngle, float minAngleBetweenElements,
            float maxStages, float startPosY, float posYDistance)
        {
            float radius = endAngle - startAngle;
            int stages = 1;
            bool isWorking = false;
            float angleBetweenElements = minAngleBetweenElements;

            while (stages <= maxStages)
            {
                angleBetweenElements = (stages * radius) / (numberOfElements - 1*stages);
                if (angleBetweenElements >= minAngleBetweenElements)
                {
                    isWorking = true;
                    break;
                }
                stages++;
            }

            if (!isWorking)
            {
                float times = radius / minAngleBetweenElements;
                times = (float) Math.Floor(times);
                angleBetweenElements = radius / times;
            }

            return ComputeSpawnPositions(spawnDistance, angleBetweenElements, startAngle, endAngle, startPosY, posYDistance,
                stages);


        }

        public static List<Vector3> ComputeSpawnPositions(float spawnDistance, float angleBetweenElements,
            float startAngle, float endAngle, float startPosY, float posYDistance, float yTimes)
        {
            List<Vector3> positions = new List<Vector3>();
            float currAngle = startAngle;
            float currY = startPosY;
            for (int i = 0; i < yTimes; i++)
            {
                while (currAngle >= startAngle && currAngle <= endAngle)
                {
                    Vector3 currPos = ComputeSpawnPosition(spawnDistance, currAngle, currY);
                    positions.Add(currPos);
                    currAngle += angleBetweenElements;
                }
                currY += posYDistance;
                currAngle = 0;
            }
            return positions;
        }

        [CanBeNull]
        public static Vector3? ComputeSpawnPositionFromStartPosition(float spawnDistance, float spawnAngle,
            float startAngle, float posY)
        {
            if (spawnAngle > 360)
            {
                return null;
            }

            float spawnAngleFromStartPosition = spawnAngle + startAngle;

            return ComputeSpawnPosition(spawnDistance, spawnAngleFromStartPosition, posY);
        }

        /// <summary>
        /// Computes position with give parameters.
        /// </summary>
        /// <param name="spawnDistance">The distance between zero position</param>
        /// <param name="spawnAngle">The angle degree.</param>
        /// <param name="posY">The position in y angle.</param>
        /// <returns></returns>
        public static Vector3 ComputeSpawnPosition(float spawnDistance, float spawnAngle, float posY)
        {
// convert from degree to radiance
            float alphaRad = spawnAngle * Mathf.Deg2Rad;

// compute x and z position based on the random value, y pos is a random value between given yPosMin and xPosMax
            float xPos = Mathf.Cos(alphaRad) * spawnDistance;
            float yPos = posY;
            float zPos = Mathf.Sin(alphaRad) * spawnDistance;
            return new Vector3(xPos, yPos, zPos);
        }
        
        /// <summary>
        /// Computes position with give parameters.
        /// </summary>
        /// <param name="spawnDistance">The distance between zero position</param>
        /// <param name="spawnAngle">The angle degree.</param>
        /// <param name="posY">The position in y angle.</param>
        /// <returns></returns>
        public static Vector3 ComputeSpawnPosition(float spawnDistance, float spawnAngle, float posX, float posY)
        {
// convert from degree to radiance
            float alphaRad = spawnAngle * Mathf.Deg2Rad;

// compute x and z position based on the random value, y pos is a random value between given yPosMin and xPosMax
            float xPos = Mathf.Cos(alphaRad) * spawnDistance;
            float yPos = posY;
            float zPos = Mathf.Sin(alphaRad) * spawnDistance;
            return new Vector3(xPos, yPos, zPos);
        }
    }
}