using UnityEngine;
using UnityEngine.Events;

namespace PointnClick
{
    public class InGameViewController : MonoBehaviour
    {
        public UnityEvent m_onInteraction;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            m_onInteraction.Invoke();
        }
    }
}
