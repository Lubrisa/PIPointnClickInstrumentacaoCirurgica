using System.Collections;
using UnityEngine;

namespace PointnClick
{
    public class ToolController : MonoBehaviour, IDraggable
    {
        private OperationType m_operationType;
        public Vector2 CurrentPosition { get; private set; }

        public void Initialize(ToolData toolData, Vector2 initialPosition)
        {
            SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();

            CurrentPosition = initialPosition;
            m_operationType = toolData.GetOperationType;
            spriteRenderer.sprite = toolData.ToolSprite;
        }

        public IEnumerator MoveTowards()
        {
            while (Vector2.Distance(transform.position, CurrentPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, CurrentPosition, 0.1f);
                yield return new WaitForSeconds(1f);
            }
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

        public void SetNewPosition(Vector2 newPosition)
        {
            CurrentPosition = newPosition;
            StartCoroutine(MoveTowards());
        }
    }
}
