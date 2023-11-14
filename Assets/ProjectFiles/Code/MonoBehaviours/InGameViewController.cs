using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PointnClick
{
    public class InGameViewController : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onInteraction;
        private SpriteRenderer m_spriteRenderer;
        private Collider2D m_collider;

        private void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_collider = GetComponent<Collider2D>();
        }

        private void OnMouseEnter() => OnInteraction();

        private void OnTriggerEnter2D(Collider2D other) => OnInteraction();

        private void OnInteraction()
        {
            m_onInteraction.Invoke();
            gameObject.SetActive(false);
        }

        public void ToggleReturnTimer(bool setActive)
        {
            if (!setActive)
                StartCoroutine(ReturnToActiveTimer());
            else
                StopCoroutine(ReturnToActiveTimer());

            m_spriteRenderer.enabled = setActive;
            m_collider.enabled = setActive;
        }

        private IEnumerator ReturnToActiveTimer()
        {
            float timeToActivate = 4;
            float timer = 0;

            while (timer < timeToActivate)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            m_spriteRenderer.enabled = true;
            m_collider.enabled = true;
        }
    }
}
