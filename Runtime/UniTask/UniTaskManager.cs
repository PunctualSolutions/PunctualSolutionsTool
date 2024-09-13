using PunctualSolutions.Tool.Singleton;
using UnityEngine;

namespace PunctualSolutions.Tool.UniTask
{
    public class UniTaskManager : MonoBehaviour, IMonoSingleton<UniTaskManager>
    {
        public static UniTaskManager Instance => MonoSingleton<UniTaskManager>.Instance;
    }
}