using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

namespace PointnClick
{
    public class ToolInstantiator : MonoBehaviour, IContainer
    {
        public static ToolInstantiator Instance;

        [SerializeField] private List<ToolController> m_toolsList = new();

        [SerializeField] private Vector2 m_startPosition;
        [SerializeField] private Vector2 m_deltas;
        [SerializeField] private int m_rowsQuantity;

        [SerializeField] private ToolController m_toolPrefab;

        private void Awake()
        {
            if (Instance is null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        [Inject]
        private void Initialize(List<ToolData> toolsData, OperationType operationType, int toolsQuantity)
        {
            ToolData[] toolPool = GenerateToolPool(toolsData, operationType, toolsQuantity * 3);

            InstantiateTools(toolPool);
        }

        private ToolData[] GenerateToolPool(List<ToolData> tools, OperationType operationType, int toolsQuantity)
        {
            List<ToolData> rightTools = tools.Where(x => x.GetOperationType == operationType).ToList();
            List<ToolData> wrongTools = SortToolsPool(tools.Except(rightTools).ToList(), toolsQuantity - rightTools.Count);
            List<ToolData> finalToolPool = rightTools.Concat(wrongTools).ToList();

            return RandomizeToolsPosition(finalToolPool);
        }

        private List<ToolData> SortToolsPool(List<ToolData> itemPool, int quantity)
        {
            List<ToolData> toolsPool = itemPool;
            List<ToolData> finalPool = new();

            for (int i = 0; i < quantity; i++)
            {
                int index = new System.Random().Next(0, toolsPool.Count);
                finalPool.Add(toolsPool[index]);
                toolsPool.Remove(toolsPool[index]);
            }

            return finalPool;
        }

        private ToolData[] RandomizeToolsPosition(List<ToolData> toolPool)
        {
            ToolData[] finalPool = new ToolData[toolPool.Count];

            for (int i = 0; i < toolPool.Count; i++)
            {
                // Getting the data to be inserted.
                ToolData tool = toolPool[i];
                // Setting the position to be inserted.
                int position;
                do
                    position = new System.Random().Next(0, toolPool.Count);
                while (finalPool[position] is not null);

                // Setting the data in the position.
                finalPool[position] = tool;
            }

            return finalPool;
        }

        private void InstantiateTools(ToolData[] positionList)
        {
            for (int i = 0; i < positionList.Length; i++)
            {
                ToolController tool = Instantiate(m_toolPrefab);
                m_toolsList.Add(tool);
                tool.Initialize(positionList[i], GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, i));
            }
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
            if (m_toolsList.Contains(tool)) return;

            m_toolsList.Add(tool);
            tool.SetNewPosition(GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, m_toolsList.Count - 1));
        }

        public void RemoveTool(ToolController tool)
        {
            if (!m_toolsList.Contains(tool)) return;

            m_toolsList.Remove(tool);
            for (int i = 0; i < m_toolsList.Count; i++)
            {
                ToolController controller = m_toolsList[i];
                controller.SetNewPosition(GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, i));
                controller.Move();
            }
        }
    }
}
