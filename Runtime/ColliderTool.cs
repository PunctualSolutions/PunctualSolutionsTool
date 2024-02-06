using UnityEngine;

namespace ZhengDianWaiBao.Tool
{
    public static class ColliderTool
    {
        /// <summary>
        /// 当target完全在origin内的时候为true
        /// </summary>
        /// <returns></returns>
        public static bool FullyInclusive(this Collider origin, Collider target) =>
            origin.bounds.Contains(target.bounds.max) &&
            origin.bounds.Contains(target.bounds.min);
    }
}