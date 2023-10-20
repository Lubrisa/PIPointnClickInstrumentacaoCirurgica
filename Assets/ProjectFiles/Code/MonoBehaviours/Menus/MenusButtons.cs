using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class MenusButtons : MonoBehaviour
    {
        public void Continue() => SceneManager.LoadScene(2);

        public void Return() => SceneManager.LoadScene(0);
    }
}
