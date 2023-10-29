using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PointnClick
{
    public class InGameAnimationManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onAnimationEnd;

        public void ReorganizeTools() => m_onAnimationEnd.Invoke(); 
    }
}
