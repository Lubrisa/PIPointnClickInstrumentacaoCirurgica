using UnityEngine;

namespace PointnClick
{
    public class GameOptionsHolder : MonoBehaviour
    {
        public static GameOptionsHolder Instance { get; private set; }

        public OperationType OperationType { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else if (Instance != this) Destroy(gameObject);
        }

        public void SaveOperationTypeData(ToolData data) => OperationType = data.Operations[0];
    }
}
