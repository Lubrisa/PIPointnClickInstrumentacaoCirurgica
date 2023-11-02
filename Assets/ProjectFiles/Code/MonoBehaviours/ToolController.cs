using System.Collections;
using System.Linq;
using UnityEngine;
using ScriptableObjectArchitecture;
using ModestTree;

namespace PointnClick
{
    public class ToolController : MonoBehaviour, IDraggable
    {
        private ToolData m_toolData;
        private Vector2 m_currentPosition;

        [SerializeField] private ToolnameTooltip m_tooltipPrefab;
        private ToolnameTooltip m_tooltipInstance;
        private bool m_dragging;

        [SerializeField] private BoolGameEvent m_onReturn;

        public void Initialize(ToolData toolData, Vector2 initialPosition)
        {
            SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = toolData.ToolSprite;

            m_toolData = toolData;
            m_currentPosition = initialPosition;

            transform.position = m_currentPosition;
        }

        public IEnumerator MoveTowards()
        {
            if (!m_dragging)
            {
                m_onReturn.Raise(false);

                float movementTime = 2f;
                float currentMovementTime = 0f;

                while (Vector2.Distance(transform.position, m_currentPosition) > 0.1f)
                {
                    currentMovementTime += Time.deltaTime;
                    transform.position = Vector2.Lerp(transform.position, m_currentPosition, currentMovementTime / movementTime);
                    yield return new WaitForEndOfFrame();
                }

                m_onReturn.Raise(true);
            }
        }

        public void Move() => StartCoroutine(MoveTowards());

        private void OnMouseDown() => m_dragging = true;

        public void OnMouseDrag()
        {
            Vector2 newPosition = GetWorldMousePosition(Input.mousePosition);

            transform.position = newPosition;
        }

        private void OnMouseUp()
        {
            m_dragging = false;
            DestroyTooltip();

            Move();
        }

        private Vector2 GetWorldMousePosition(Vector3 virtualMousePosition)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(virtualMousePosition);

            return new Vector2(worldMousePosition.x, worldMousePosition.y);
        }

        public void SetNewPosition(Vector2 newPosition) => m_currentPosition = newPosition;

        public bool CheckOperationMatch(OperationType operationToCompare) =>
            m_toolData.Operations.Any(operation => operation == operationToCompare);

        private void OnMouseEnter()
        {
            if (m_dragging) return;

            m_tooltipInstance = Instantiate(m_tooltipPrefab);
            m_tooltipInstance.Initialize(m_toolData.ToolName);
        }

        private void OnMouseExit()
        {
            if (!m_dragging) DestroyTooltip();
        }

        private void DestroyTooltip()
        {
            if (m_tooltipInstance is null) return;

            Destroy(m_tooltipInstance.gameObject);
            m_tooltipInstance = null;
        }
    }
}
