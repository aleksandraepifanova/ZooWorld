using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.World
{
    public sealed class WorldBounds : MonoBehaviour
    {
        [SerializeField] private Transform min;
        [SerializeField] private Transform max;

        public Vector3 Min => min.position;
        public Vector3 Max => max.position;

        public bool ContainsXZ(Vector3 worldPos)
        {
            return worldPos.x >= Min.x && worldPos.x <= Max.x
                && worldPos.z >= Min.z && worldPos.z <= Max.z;
        }

        public Vector3 ClampToXZ(Vector3 worldPos)
        {
            worldPos.x = Mathf.Clamp(worldPos.x, Min.x, Max.x);
            worldPos.z = Mathf.Clamp(worldPos.z, Min.z, Max.z);
            return worldPos;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (min == null || max == null) return;

            var center = (Min + Max) * 0.5f;
            var size = new Vector3(Mathf.Abs(Max.x - Min.x), 0.05f, Mathf.Abs(Max.z - Min.z));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}

