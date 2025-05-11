using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image shieldBar;
        [SerializeField] private float timeAfterDmgToRegen = 3f;
        public float shield = 100f;
        public float maxShield = 100f;
        
        private float displayedShield = 100f;
        private float timeSinceLastShot;

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            
            displayedShield = Mathf.Lerp(displayedShield, shield, Time.deltaTime * 10f);
            shieldBar.fillAmount = displayedShield / 100f;

            if (timeSinceLastShot >= timeAfterDmgToRegen && shield < 100)
            {
                Regenerate();
            }
        }

        public void TakeDamage(float damage)
        {
            shield -= damage;
            shield = Mathf.Clamp(shield, 0, 100);
            
            timeSinceLastShot = 0f;
        }

        public void Heal(float heal) // TODO: if doing shield upgrades, change this for max health
        {
            shield += heal;
            shield = Mathf.Clamp(shield, 0, 100);
        }

        private void Regenerate()
        {
            shield += maxShield;
            shield = Mathf.Clamp(shield, 0f, 100f);
        }
    }
}