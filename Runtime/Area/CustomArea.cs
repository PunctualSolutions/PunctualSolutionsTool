#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace PunctualSolutions.Tool.Area
{
    public class CustomArea : AreaBase
    {
        public Vector2[] points;

        IReadOnlyList<Vector3> ShowPoints => points.Select(x => new Vector3(transform.position.x + x.x, transform.position.y, transform.position.z + x.y)).ToArray();

        void OnDrawGizmos()
        {
            var inPoints = ShowPoints;
            if (inPoints is not { Count: >= 2, }) return;
            Gizmos.color = Color.yellow;
            for (var i = 0; i < inPoints.Count - 1; i++) Gizmos.DrawLine(inPoints[i], inPoints[i + 1]);
            Gizmos.DrawLine(inPoints[^1], inPoints[0]);
        }

        public override List<Vector3> GenerateRandomPoints(float minDist, int count)
        {
            var maxTries   = 10 * count;
            var pointsList = new List<Vector3>();
            var tries      = 0;
            while (pointsList.Count < count && tries < maxTries)
            {
                var randomPoint = GetRandomPosition();
                if (IsPointValid(pointsList, randomPoint, minDist)) pointsList.Add(randomPoint);
                tries++;
            }

            if (pointsList.Count < count) throw new("无法在给定范围内生成足够数量的点");
            return pointsList;
        }

        public override Vector3 GetRandomPosition()
        {
            // 计算边界框
            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (var point in points)
            {
                if (point.x < minX) minX = point.x;
                if (point.x > maxX) maxX = point.x;
                if (point.y < minY) minY = point.y;
                if (point.y > maxY) maxY = point.y;
            }

            Vector2 randomPoint;
            do
                randomPoint = new(Random.Range(minX, maxX), Random.Range(minY, maxY));
            while (!IsPointInPolygon(randomPoint));

            return new(randomPoint.x + transform.position.x, transform.position.y, randomPoint.y + transform.position.z);
        }

        bool IsPointInPolygon(Vector2 testPoint)
        {
            var result = false;
            var j      = points.Length - 1;
            for (var i = 0; i < points.Length; i++)
            {
                if (points[i].y < testPoint.y && points[j].y >= testPoint.y || points[j].y < testPoint.y && points[i].y >= testPoint.y)
                    if (points[i].x + (testPoint.y - points[i].y) / (points[j].y - points[i].y) * (points[j].x - points[i].x) < testPoint.x)
                        result = !result;
                j = i;
            }

            return result;
        }

        static bool IsPointValid(List<Vector3> points, Vector3 point, float minDist)
        {
            return points.All(otherPoint => !(Vector3.Distance(point, otherPoint) < minDist));
        }
    }
}