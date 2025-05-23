using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "DefaultWeapon", menuName = "ScriptableObjects/DefaultWeapon")]
    public class DefaultWeapon : Weapon
    {
        public override void Fire(Transform firePoint, GameObject shooter, AudioSource soundSource)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
            Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
            Collider2D shooterCollider = shooter.GetComponent<Collider2D>();
            if (projectileCollider && shooterCollider) Physics2D.IgnoreCollision(projectileCollider, shooterCollider);

            Projectile obj = projectile.GetComponent<Projectile>();
            obj.shooter = shooter;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = firePoint.up * projectileSpeed;

            if (soundSource) soundSource.PlayOneShot(projectileSound);
        }
    }
}