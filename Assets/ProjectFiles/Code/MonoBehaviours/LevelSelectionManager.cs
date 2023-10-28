using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class LevelSelectionManager : MonoBehaviour
    {
        private Animator m_animator;

        [SerializeField] private ToolDataGameEvent m_onOperationTypeSet;
        [SerializeField] private IntGameEvent m_onDifficultySet;
        [SerializeField] private BoolGameEvent m_onStartButtonActivation;

        private int[] m_rightToolQuantities;

        private void Start() => m_animator = GetComponent<Animator>();

        public void ReturnToMainMenu() => SceneManager.LoadScene(0);

        public void SetOperationType(ToolData data)
        {
            m_onOperationTypeSet.Raise(data);
            m_rightToolQuantities = data.OperationsData[0].OperationsDifficulties;
        }

        public void SetDifficulty(int difficulty)
        {
            m_onDifficultySet.Raise(m_rightToolQuantities[difficulty]);
            m_onStartButtonActivation.Raise(true);
        }

        public void StartGame() => SceneManager.LoadScene(3);
    }
}
