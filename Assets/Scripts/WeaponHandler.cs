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
}
