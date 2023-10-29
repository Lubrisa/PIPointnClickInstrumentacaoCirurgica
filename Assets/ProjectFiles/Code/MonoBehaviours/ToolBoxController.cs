using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PointnClick
{
    public class ToolBoxController : MonoBehaviour, IContainer
    {
        public static ToolBoxController Instance;

        private int m_maxToolCapacity;
        private OperationType m_operationType;
        [SerializeField] private BoolGameEvent m_onBoxChange;
        [SerializeField] private StringGameEvent m_onToolListUpdate;

        [SerializeField] private GameEvent m_onWrongAnswer;
        [SerializeField] private GameEvent m_onRightAnswer;

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
        private void Initialize(int rightToolsQuantity, OperationType operationType)
        {
            m_maxToolCapacity = rightToolsQuantity;
            m_operationType = operationType;

            SceneManager.sceneUnloaded += ClearInstance;
        }

        private void Start() => m_onToolListUpdate.Raise(ToolsLeftText());

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
            if (m_toolsList.Contains(tool) || m_toolsList.Count == m_maxToolCapacity) return;

            tool.transform.parent = transform;
            m_toolsList.Add(tool);
            ToolInstantiator.Instance.RemoveTool(tool);
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            tool.SetNewPosition(
                GenerateCoordinates(position2D + m_startPosition, m_deltas, m_rowsQuantity, m_toolsList.Count - 1));

            m_onBoxChange.Raise(m_toolsList.Count == m_maxToolCapacity);
            m_onToolListUpdate.Raise(ToolsLeftText());
        }

        public void RemoveTool(ToolController tool)
        {
            if (!m_toolsList.Contains(tool)) return;

            m_toolsList.Remove(tool);
            ToolInstantiator.Instance.AddTool(tool);

            ResetToolsPosition();

            m_onBoxChange.Raise(false);
            m_onToolListUpdate.Raise(ToolsLeftText());
        }

        public void CheckAnswer()
        {
            bool answerIsRight = true;
            foreach (var tool in m_toolsList)
            {
                if (!tool.CheckOperationMatch(m_operationType, m_maxToolCapacity))
                {
                    answerIsRight = false;
                    break;
                }
            }

            if (answerIsRight)
                SceneManager.LoadScene(5); //m_onRightAnswer.Raise();
            else
            {
                m_onWrongAnswer.Raise();
                for (int i = m_toolsList.Count - 1; i >= 0; i--)
                {
                    ToolController tool = m_toolsList[i];
                    m_toolsList.Remove(tool);
                    ToolInstantiator.Instance.AddTool(tool);
                    tool.Move();
                }
            }

            m_onToolListUpdate.Raise(ToolsLeftText());
            m_onBoxChange.Raise(false);
        }

        private string ToolsLeftText() => $"{m_toolsList.Count}/{m_maxToolCapacity}";

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
