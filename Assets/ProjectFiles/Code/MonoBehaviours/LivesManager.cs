using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointnClick
{
    public class LivesManager : MonoBehaviour
    {
        private int m_remainingLives = 3;
        [SerializeField] private GameObject[] m_lives;

        public void LoseLife()
        {
            transform.GetChild(m_remainingLives - 1).gameObject.SetActive(false);
            m_remainingLives--;

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (!(m_remainingLives == 0)) return;

            SceneManager.LoadScene(4);
        }
    }
}
