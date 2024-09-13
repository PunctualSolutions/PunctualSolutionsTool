using PunctualSolutions.Tool.Singleton;
using UnityEngine;

namespace PunctualSolutions.Tool.Addressables
{
    public class AddressablesGroup<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
    }
}