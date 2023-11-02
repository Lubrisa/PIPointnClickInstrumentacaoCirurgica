using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PointnClick
{
    public class ColorSwitch : MonoBehaviour
    {
        [SerializeField] private Color m_activeColor;
        [SerializeField] private Color m_deactiveColor;
        [SerializeField] private Image[] m_buttonsImages;


        public void SwitchColors()
        {
            GetComponent<Image>().color = m_activeColor;
            foreach (Image image in m_buttonsImages)
                image.color = m_deactiveColor;
        }
    }
}
