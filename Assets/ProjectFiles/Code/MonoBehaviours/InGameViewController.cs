using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PointnClick
{
    public class InGameViewController : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onMouseOver;

        private void OnMouseEnter() => m_onMouseOver.Invoke();

        private void OnTriggerEnter2D(Collider2D other) => m_onMouseOver.Invoke();
    }
}
