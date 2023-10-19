using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using Zenject;

namespace PointnClick
{
    public class ToolBoxController : MonoBehaviour, IContainer
    {
        public static ToolBoxController Instance;

        private int m_maxToolQuantity;
        private OperationType m_operationType;

        private List<ToolController> m_toolsList = new();

        [SerializeField] private Vector2 m_startPosition;
        [SerializeField] private Vector2 m_deltas;
        [SerializeField] private int m_rowsQuantity;

        private void Awake()
        {
            if (Instance is null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        [Inject]
        private void Initialize(int maxQuantity, OperationType operationType)
        {
            m_maxToolQuantity = maxQuantity - 5;
            m_operationType = operationType;
        }

        public Vector2 GenerateCoordinates(Vector2 startPosition, Vector2 deltas, int rowsQuantity, int index)
        {
            int row = index / rowsQuantity;
            int column = index % rowsQuantity;

            float xPosition = startPosition.x + deltas.x * column;
            float yPosition = startPosition.y + deltas.y * row;

            return new Vector2(xPosition, yPosition);
        }

        public void AddTool(ToolController tool)
        {
            if (m_toolsList.Contains(tool) || m_toolsList.Count == m_maxToolQuantity) return;

            m_toolsList.Add(tool);
            ToolInstantiator.Instance.RemoveTool(tool);
            tool.SetNewPosition(GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, m_toolsList.Count - 1));
        }

        public void RemoveTool(ToolController tool)
        {
            if (!m_toolsList.Contains(tool)) return;

            m_toolsList.Remove(tool);
            ToolInstantiator.Instance.AddTool(tool);

            for (int i = 0; i < m_toolsList.Count; i++)
            {
                ToolController controller = m_toolsList[i];
                controller.SetNewPosition(GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, i));
                controller.Move();
            }
        }

        private void CheckTools()
        {

        }
    }
}
