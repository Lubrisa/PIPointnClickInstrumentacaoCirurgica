using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class LevelSelectionManager : MonoBehaviour
    {

        [SerializeField] private ToolDataGameEvent m_onOperationTypeSet;
        [SerializeField] private BoolGameEvent m_onStartButtonActivation;

        public void ReturnToMainMenu() => SceneManager.LoadScene(0);

        public void SetOperationType(ToolData data)
        {
            m_onOperationTypeSet.Raise(data);
            m_onStartButtonActivation.Raise(true);
        }

        public void StartGame() => SceneManager.LoadScene(3);
    }
}
