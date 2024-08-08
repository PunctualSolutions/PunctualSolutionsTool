#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Area
{
    public class BoxArea : AreaBase
    {
        [field: SerializeField] public Vector2 SpawnRange { get; private set; }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new(SpawnRange.x, 0, SpawnRange.y));
        }

        public override List<Vector3> GenerateRandomPoints(float minDist, int count)
        {
            var maxTries = 10 * count;
            var points   = new List<Vector3>();
            var tries    = 0;
            while (points.Count < count && tries < maxTries)
            {
                var randomPoint = GetRandomPosition();
                if (IsPointValid(points, randomPoint, minDist)) points.Add(randomPoint);
                tries++;
            }

            if (points.Count < count) throw new("无法在给定范围内生成足够数量的点");
            return points;
        }

        public override Vector3 GetRandomPosition()
        {
            var range = SpawnRange;
            var x     = Random.Range(-range.x / 2, range.x / 2);
            var z     = Random.Range(-range.y / 2, range.y / 2);
            return new Vector3(x, 0, z) + transform.position;
        }

        static bool IsPointValid(List<Vector3> points, Vector3 point, float minDist) => points.All(otherPoint => !(Vector3.Distance(point, otherPoint) < minDist));
    }
}