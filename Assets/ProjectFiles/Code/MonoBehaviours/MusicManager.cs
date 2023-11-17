using UnityEngine;

namespace PointnClick
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip m_clip;

        void Start() => AudioController.Instance.PlayMusic(m_clip);
    }
}
