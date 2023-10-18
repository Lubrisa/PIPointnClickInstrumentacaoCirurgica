using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

namespace PointnClick
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Vector2 m_startPosition;
        [SerializeField] private Vector2 m_deltas;
        [SerializeField] private int m_rowsQuantity;

        [SerializeField] private ToolController m_toolPrefab;

        [SerializeField] private int m_remainingLives = 3;

        [Inject]
        private void Initialize(List<ToolData> toolsData, OperationType operationType, int toolsQuantity)
        {
            ToolData[] toolPool = GenerateToolPool(toolsData, operationType, toolsQuantity);

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
                int index = new System.Random().Next(0, itemPool.Count);
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
                tool.Initialize(positionList[i], GenerateCoordinates(m_startPosition, m_deltas, m_rowsQuantity, i));
            }
        }

        private Vector2 GenerateCoordinates(Vector2 startPosition, Vector2 deltas, int rowsQuantity, int index)
        {
            int row = index / rowsQuantity;
            int column = index % rowsQuantity;

            float xPosition = deltas.x * row;
            float yPosition = deltas.y * column;

            return new Vector2(xPosition, yPosition);
        }

        private void CheckAnswer()
        {

        }

        private void Win()
        {

        }

        private void DealDamage()
        {

        }

        private void Lose()
        {

        }
    }
}
