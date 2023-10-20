using System.Collections.Generic;
using UnityEngine;

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

            if (m_remainingLives == 0)
            {
            }
        }
    }
}
