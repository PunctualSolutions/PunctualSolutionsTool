using System.Collections.Generic;
using UnityEngine;

namespace PunctualSolutionsTool.Tool
{
    public interface IArea
    {
        List<Vector3> GenerateRandomPoints(float minDist, int count);
        Vector3       GetRandomPosition();
    }
}