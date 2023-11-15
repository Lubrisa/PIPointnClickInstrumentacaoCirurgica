using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PointnClick
{
    public class ToolInstantiator : MonoBehaviour, IContainer
    {
        public static ToolInstantiator Instance;

        private List<ToolController> m_toolsList = new();
        [SerializeField] private int m_maxToolModifier;

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
        private void Initialize(List<ToolData> toolsData, OperationType operationType)
        {
            ToolData[] toolPool = GenerateToolPool(toolsData, operationType);

            InstantiateTools(toolPool);

            SceneManager.sceneUnloaded += ClearInstance;
        }

        private ToolData[] GenerateToolPool(List<ToolData> tools, OperationType operationType)
        {
            List<ToolData> rightTools = tools
            .Where(tool => tool.Operations.Any(type => type == operationType)).ToList();
            List<ToolData> wrongTools = tools.Except(rightTools).ToList();

            // Getting the higher number multiple of ten above the total tools quantity. 
            int totalToolsQuantity = ((rightTools.Count * m_maxToolModifier / 10) + 1) * 10;
            // Checking if the total quantity is higher than the wrong tools pool size and avoiding that.
            int correctedToolsQuantity =
                totalToolsQuantity - rightTools.Count <= wrongTools.Count ?
                totalToolsQuantity : wrongTools.Count;

            List<ToolData> sortedWrongTools = SortToolsPool(wrongTools, totalToolsQuantity - rightTools.Count);

            List<ToolData> finalToolPool = rightTools.Concat(sortedWrongTools).ToList();

            return RandomizeToolsPosition(finalToolPool);
        }

        private List<ToolData> SortToolsPool(List<ToolData> itemPool, int quantity)
        {
            List<ToolData> toolsPool = itemPool;
            List<ToolData> finalPool = new();

            for (int i = 0; i < quantity; i++)
            {
                int index = new System.Random().Next(itemPool.Count);
                finalPool.Add(toolsPool[index]);
                toolsPool.Remove(toolsPool[index]);
            }

            return finalPool;
        }

        private ToolData[] RandomizeToolsPosition(List<ToolData> toolPool)
        {
            ToolData[] finalPool = new ToolData[toolPool.Count];

            for (int i = 0; i < finalPool.Length; i++)
            {
                // Getting the data to be inserted.
                ToolData tool = toolPool[i];
                // Setting the position to be inserted.
                int position;
                do
                    position = new System.Random().Next(0, finalPool.Length);
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
                ToolController tool = Instantiate(m_toolPrefab, transform);
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

            tool.transform.parent = transform;
            m_toolsList.Add(tool);

            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            tool.SetNewPosition(
                GenerateCoordinates(position2D + m_startPosition, m_deltas, m_rowsQuantity, m_toolsList.Count - 1));
        }

        public void RemoveTool(ToolController tool)
        {
            if (!m_toolsList.Contains(tool)) return;

            m_toolsList.Remove(tool);
            ResetToolsPosition();
        }

        public void ResetToolsPosition()
        {
            for (int i = 0; i < m_toolsList.Count; i++)
            {
                ToolController controller = m_toolsList[i];

                Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
                controller.SetNewPosition(
                    GenerateCoordinates(position2D + m_startPosition, m_deltas, m_rowsQuantity, i));

                controller.Move();
            }
        }

        private void ClearInstance(Scene current) => Instance = null;
    }
}
