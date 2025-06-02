using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "BombWeapon", menuName = "ScriptableObjects/BombWeapon")]
    public class BombWeapon : Weapon
    {
        [SerializeField] private float detonationDelay = 2f;
        public override void Fire(Transform firePoint, GameObject shooter, AudioSource soundSource)
        {
            GameObject bomb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Collider2D bombCollider = bomb.GetComponent<Collider2D>();
            Collider2D shooterCollider = shooter.GetComponent<Collider2D>();
            if (bombCollider && shooterCollider) Physics2D.IgnoreCollision(bombCollider, shooterCollider);

            BombProjectile bombComponent = bomb.GetComponent<BombProjectile>();
            bombComponent.shooter = shooter;
            bombComponent.explosionDelay = detonationDelay;

            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = firePoint.up * projectileSpeed;

            if (soundSource) soundSource.PlayOneShot(projectileSound);
        }
    }
}