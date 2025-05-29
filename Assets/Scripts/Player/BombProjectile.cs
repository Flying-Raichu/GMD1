using System.Collections;
using UnityEditor.PackageManager;

namespace Player
{
    using UnityEngine;

    public class BombProjectile : Projectile
    {
        public float explosionDelay;
        [SerializeField] private GameObject explosionEffect;
        public float explosionRadius;

        private bool hasExploded = false;
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(BlinkBeforeExplosion());
            Invoke(nameof(Explode), explosionDelay);
        }
        
        private IEnumerator BlinkBeforeExplosion()
        {
            float blinkInterval = 0.25f;
            float timer = 0f;
            while (timer < explosionDelay - 0.25f)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(blinkInterval);
                timer += blinkInterval;
            }
            spriteRenderer.enabled = true;
        }

        private void Explode()
        {
            if (hasExploded) return; 
            hasExploded = true;

            if (explosionEffect)
            {
                GameObject bomb = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                bomb.GetComponent<ExplosionEffect>().maxScale = explosionRadius + 1;
            }
            

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy") && hit.TryGetComponent<EnemyHealth>(out var enemyHealth))
                {
                    enemyHealth.TakeDamage(damage);
                }
                
                else if (!hit.CompareTag("Player")) Destroy(hit.gameObject);
            }

            Destroy(gameObject);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == shooter || !hasExploded) return;
        }
    }

}