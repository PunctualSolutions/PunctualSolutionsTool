#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Area
{
    public abstract class AreaBase : MonoBehaviour, IArea
    {
        public abstract List<Vector3> GenerateRandomPoints(float minDist, int count);

        public abstract Vector3 GetRandomPosition();
    }
}