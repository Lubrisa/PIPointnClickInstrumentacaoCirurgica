using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartGame() => SceneManager.LoadScene(1);

        public void Credits() => SceneManager.LoadScene(6);

        public void Exit()
        {
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
