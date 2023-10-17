using System.Collections;
using UnityEngine;

namespace PointnClick
{
    public class ToolController : MonoBehaviour, IDraggable
    {
        private OperationType m_operationType;
        private Vector2 m_initialPosition;
        public Vector2 CurrentPosition { get; set; }

        public void Initialize(ToolData toolData, Vector2 initialPosition)
        {
            SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();

            m_initialPosition = initialPosition;
            m_operationType = toolData.GetOperationType;
            spriteRenderer.sprite = toolData.ToolSprite;
        }

        public IEnumerator MoveTowards()
        {
            yield return new WaitForSeconds(1f);
        }

        public void OnMouseDrag()
        {
            Vector2 newPosition = GetWorldMousePosition(Input.mousePosition);

            transform.position = newPosition;
        }

        private Vector2 GetWorldMousePosition(Vector3 virtualMousePosition)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(virtualMousePosition);

            return new Vector2(worldMousePosition.x, worldMousePosition.y);
        }
    }
}
