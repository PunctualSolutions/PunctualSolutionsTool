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
    }
}