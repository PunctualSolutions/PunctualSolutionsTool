using System.Collections.Generic;
using UnityEngine;

namespace PunctualSolutionsTool.Tool
{
    public abstract class AreaBase : MonoBehaviour, IArea
    {
        public abstract List<Vector3> GenerateRandomPoints(float minDist, int count);

        public abstract Vector3 GetRandomPosition();
    }
}