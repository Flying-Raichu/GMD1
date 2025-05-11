using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private float regenerationRate = 5f;

        public float health = 100f;

        private float displayedHealth = 100f;

        void Update()
        {
            displayedHealth = Mathf.Lerp(displayedHealth, health, Time.deltaTime * 10f);
            healthBar.fillAmount = displayedHealth / 100f;

            Regenerate();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, 100);
        }

        private void Regenerate()
        {
            health += regenerationRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, 100f);
        }
    }
}
