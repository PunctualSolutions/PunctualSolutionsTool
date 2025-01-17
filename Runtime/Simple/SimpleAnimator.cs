using UnityEngine;

namespace PunctualSolutions.Tool.Simple
{
    public class SimpleAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void SetTrigger(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }

        public void SetFloat(string parameterName, float value)
        {
            animator.SetFloat(parameterName, value);
        }

        public void SetBoolTrue(string parameterName)
        {
            SetBool(parameterName, true);
        }

        public void SetBoolFalse(string parameterName)
        {
            SetBool(parameterName, false);
        }
        public void SetBool(string parameterName, bool value)
        {
            animator.SetBool(parameterName, value);
        }
    }
}
