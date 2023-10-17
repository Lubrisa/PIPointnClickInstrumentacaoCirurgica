using UnityEngine;

namespace PointnClick
{
    public class ToolController : MonoBehaviour, IDraggable
    {
        private bool m_isClicked;

        public void OnMouseDown() => m_isClicked = true;

        public void OnMouseDrag()
        {
            if (!m_isClicked) return;

            Vector2 newPosition = GetWorldMousePosition(Input.mousePosition);

            transform.position = newPosition;
        }

        private Vector2 GetWorldMousePosition(Vector3 virtualMousePosition)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(virtualMousePosition);

            return new Vector2(worldMousePosition.x, worldMousePosition.y);
        }

        public void OnMouseUp() => m_isClicked = false;
    }
}
