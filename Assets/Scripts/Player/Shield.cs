using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image shieldHealthBar;
        [SerializeField] private float regenerationRate = 5f;
        [SerializeField] private float timeAfterDmgToRegen = 3f;
        public float health = 100f;
        
        private float displayedHealth = 100f;
        private float timeSinceLastShot;

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            
            displayedHealth = Mathf.Lerp(displayedHealth, health, Time.deltaTime * 10f);
            shieldHealthBar.fillAmount = displayedHealth / 100f;

            if (timeSinceLastShot >= timeAfterDmgToRegen && health < 100)
            {
                Regenerate();
            }

            if (Input.GetKeyDown(KeyCode.Return)) TakeDamage(20);
            
            else if (Input.GetKeyDown(KeyCode.Space)) Heal(20);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, 100);
            
            timeSinceLastShot = 0f;
        }

        public void Heal(float heal) // TODO: if doing shield upgrades, change this for max health
        {
            health += heal;
            health = Mathf.Clamp(health, 0, 100);
        }

        private void Regenerate()
        {
            health += regenerationRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, 100f);
        }
    }
}