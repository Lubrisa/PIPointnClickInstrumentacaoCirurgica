using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PointnClick
{
    public class ToolnameTooltip : MonoBehaviour
    {
        [SerializeField] private Canvas m_popupCanvas;
        [SerializeField] private GameObject m_popupCanvasObject;
        [SerializeField] private RectTransform m_popupObject;
        [SerializeField] private TMP_Text m_infoText;
        [SerializeField] private Vector3 m_offset;
        [SerializeField] private float m_padding;

        public void Initialize(string text)
        {
            m_infoText.text = text;    
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_popupObject);        
        }

        private void Update() => FollowCursor();

        private void FollowCursor()
        {
            Vector3 newPosition = Input.mousePosition + m_offset;
            newPosition.z = 0f;

            m_popupObject.transform.position = newPosition;
        }
    }
}
