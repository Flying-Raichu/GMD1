using UnityEngine;

// the actual weapon class attached to the player. works with companion weapon script
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private Transform firePoint; // spawn position, aka player
    [SerializeField] private AudioSource soundSource;
    
    private float timeSinceLastShot = 0f;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= (1 / equippedWeapon.FireRate) && InputManager.instance.RTriggerPressed()) // makes it so that fire rate is in bullets / second, not seconds / bullet
        {
            Fire();
            timeSinceLastShot = 0f;
        }
    }

    private void Fire()
    {
        if (equippedWeapon.ProjectilePrefab != null)
        {
            GameObject projectile = Instantiate(equippedWeapon.ProjectilePrefab, firePoint.position, firePoint.rotation);
            IgnoreSpawnCollision(projectile);
            
            Projectile obj = projectile.GetComponent<Projectile>();
            obj.shooter = gameObject;
            
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                Vector2 direction = firePoint.up;
                rb.linearVelocity = direction * equippedWeapon.ProjectileSpeed;
            }
            
            if (soundSource != null)
            {
                soundSource.PlayOneShot(equippedWeapon.ProjectileSound);
            }
        }
    }

    /*
     * so that bullet doesn't instantly despawn at instantiation, since it technically collides with the player.
     * if put in the projectile's OnCollisionEnter, physics is already applied at that point, which messes with
     * initial velocity
     */
    private void IgnoreSpawnCollision(GameObject projectile)
    {
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();
        Collider2D shooterCollider = GetComponent<Collider2D>();

        if (projectileCollider != null && shooterCollider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, shooterCollider);
        }
    }
}
