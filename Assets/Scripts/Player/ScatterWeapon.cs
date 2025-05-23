using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "ScatterWeapon", menuName = "ScriptableObjects/ScatterWeapon")]
    public class ScatterWeapon : Weapon
    {
        private float[] angles = { -15, 0f, 15 };
        public override void Fire(Transform firePoint, GameObject shooter, AudioSource soundSource)
        {
            Quaternion baseRotation = firePoint.rotation; // not having this causes issues when the player turns, the angles might change between updates
            
            foreach (float angle in angles)
            {
                Quaternion rotation = baseRotation * Quaternion.Euler(0, 0, angle);
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
                Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
                Collider2D shooterCollider = shooter.GetComponent<Collider2D>();
                
                // so shots don't instantly despawn
                if (projectileCollider && shooterCollider) Physics2D.IgnoreCollision(projectileCollider, shooterCollider);

                Projectile obj = projectile.GetComponent<Projectile>();
                obj.shooter = shooter;

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb) rb.linearVelocity = rotation * Vector2.up * projectileSpeed;
            }

            if (soundSource) soundSource.PlayOneShot(projectileSound);
        }
    }
}