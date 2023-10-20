using System.Collections;
using UnityEngine;

namespace PointnClick
{
    public class ToolController : MonoBehaviour, IDraggable
    {
        public OperationType OperationType { get; private set; }
        private Vector2 m_currentPosition;

        public void Initialize(ToolData toolData, Vector2 initialPosition)
        {
            SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
            transform.position = initialPosition;

            m_currentPosition = initialPosition;
            OperationType = toolData.GetOperationType;
            spriteRenderer.sprite = toolData.ToolSprite;
        }

        public IEnumerator MoveTowards()
        {
            float movementTime = 2f;
            float currentMovementTime = 0f;

            while (Vector2.Distance(transform.position, m_currentPosition) > 0.1f)
            {
                currentMovementTime += Time.deltaTime;
                transform.position = Vector2.Lerp(transform.position, m_currentPosition, currentMovementTime / movementTime);
                yield return new WaitForEndOfFrame();
            }
        }

        public void OnMouseDrag()
        {
            Vector2 newPosition = GetWorldMousePosition(Input.mousePosition);

            transform.position = newPosition;
        }

        private void OnMouseUp() => Move();

        private Vector2 GetWorldMousePosition(Vector3 virtualMousePosition)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(virtualMousePosition);

            return new Vector2(worldMousePosition.x, worldMousePosition.y);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            ToolBoxController controller;

            if (other.transform.TryGetComponent<ToolBoxController>(out controller))
                controller.AddTool(this);

        }

        public void OnTriggerExit2D(Collider2D other)
        {
            ToolBoxController controller;

            if (other.TryGetComponent<ToolBoxController>(out controller))
                controller.RemoveTool(this);
        }

        public void SetNewPosition(Vector2 newPosition) => m_currentPosition = newPosition;

        public void Move() => StartCoroutine(MoveTowards());
    }
}
