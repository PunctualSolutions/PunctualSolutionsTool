using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PunctualSolutionsTool.Tool
{
    public class CustomArea : AreaBase
    {
        public Vector2[]       points;
        IReadOnlyList<Vector3> ShowPoints => points.Select(x => new Vector3(transform.position.x + x.x, transform.position.y, transform.position.z + x.y)).ToArray();

        void OnDrawGizmos()
        {
            var inPoints = ShowPoints;
            if (inPoints is not { Count: >= 2 }) return;
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
            var bounds = new Bounds(transform.position, Vector3.zero);
            foreach (var point in ShowPoints) bounds.Encapsulate(point);

            var randomPoint = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    transform.position.y,
                    Random.Range(bounds.min.z, bounds.max.z)
            );

            return randomPoint;
        }

        static bool IsPointValid(List<Vector3> points, Vector3 point, float minDist) => points.All(otherPoint => !(Vector3.Distance(point, otherPoint) < minDist));
    }
}