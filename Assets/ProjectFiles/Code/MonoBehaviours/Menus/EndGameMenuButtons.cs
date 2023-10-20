using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class EndGameMenuButtons : MonoBehaviour
    {
        public void ReturnToMainMenu() => SceneManager.LoadScene(0);
    }
}
