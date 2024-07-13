using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoLive.Main
{
    public class BoxArea : MonoBehaviour
    {
        [field: SerializeField] public Vector2 SpawnRange { get; private set; }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new(SpawnRange.x, 0, SpawnRange.y));
        }

        public List<Vector2> GenerateRandomPoints(Vector2 range, float minDist, int count)
        {
            var maxTries = 10 * count;
            var points   = new List<Vector2>();
            var tries    = 0;

            while (points.Count < count && tries < maxTries)
            {
                var randomPoint = new Vector2(Random.Range(range.x, range.y), Random.Range(range.x, range.y));
                if (IsPointValid(points, randomPoint, minDist)) points.Add(randomPoint);
                tries++;
            }

            if (points.Count < count) throw new("无法在给定范围内生成足够数量的点");

            return points;
        }

        static bool IsPointValid(List<Vector2> points, Vector2 point, float minDist)
        {
            return points.All(otherPoint => !(Vector2.Distance(point, otherPoint) < minDist));
        }
    }
}