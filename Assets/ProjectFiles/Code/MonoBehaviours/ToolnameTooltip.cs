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

            m_popupObject.transform.position = CorrectCoordinates(newPosition);
        }

        private Vector2 CorrectCoordinates(Vector2 coordinates)
        {
            Vector2 newCoordinates = coordinates;

            // Right edge to screen edge distance.
            float rEdge = Screen.width - (coordinates.x + m_popupObject.rect.width * m_popupCanvas.scaleFactor / 2) - m_padding;
            // Left edge to screen edge distance.
            float lEdge = 0 - (coordinates.x - m_popupObject.rect.width * m_popupCanvas.scaleFactor / 2) + m_padding;
            // Top edge to screen edge distance.
            float tEdge = Screen.height - (coordinates.y + m_popupObject.rect.height * m_popupCanvas.scaleFactor) - m_padding;
            // Bottom edge to screen distance.
            float bEdge = 0 - (coordinates.y - m_popupObject.rect.height * m_popupCanvas.scaleFactor);

            newCoordinates.x = rEdge < 0 ? coordinates.x + rEdge :
                lEdge > 0 ? coordinates.x + lEdge : coordinates.x;
            newCoordinates.y = tEdge < 0 ? coordinates.y + tEdge :
                bEdge > 0 ? coordinates.y + bEdge : coordinates.y;

            return newCoordinates;
        }
    }
}
