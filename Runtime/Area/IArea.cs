#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Area
{
    public interface IArea
    {
        List<Vector3> GenerateRandomPoints(float minDist, int count);
        Vector3       GetRandomPosition();
    }
}