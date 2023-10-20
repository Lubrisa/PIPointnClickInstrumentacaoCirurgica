using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace PointnClick
{
    public class GameOptionsHolder : MonoBehaviour
    {
        public static GameOptionsHolder Instance { get; private set; }

        private OperationType m_operationType;
        private int m_toolsQuantity;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else if (Instance != this) Destroy(gameObject);
        }

        public void SaveOperationTypeData(ToolData data) => m_operationType = data.OperationsTypes[0];

        public void SaveDifficultyData(int data) => m_toolsQuantity = data;

        public GameData GetData()
        {
            StartCoroutine(AutoDestroy());
            return new GameData(m_operationType, m_toolsQuantity);
        }

        private IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }

    public class GameData
    {
        public OperationType Operation { get; set; }
        public int ToolsQuantity { get; set; }

        public GameData(OperationType operationType, int toolsQuantity)
        {
            Operation = operationType;
            ToolsQuantity = toolsQuantity;
        }
    }
}
