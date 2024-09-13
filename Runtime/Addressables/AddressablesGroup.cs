using PunctualSolutions.Tool.Singleton;
using UnityEngine;

namespace PunctualSolutions.Tool.Addressables
{
    public class AddressablesGroup<T> : MonoBehaviour, IMonoSingleton<T> where T : MonoBehaviour, IMonoSingleton<T>
    {
    }
}