using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource soundSource;

    private float timeSinceLastShot = 0f;

    private Transform player;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        if (GameManager.instance != null && GameManager.instance.GetPlayerInstance() != null)
        {
            player = GameManager.instance.GetPlayerInstance().transform;
        }
    }


    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= (1 / equippedWeapon.FireRate))
        {
            Fire();
            timeSinceLastShot = 0f;
        }
    }

    private void Fire()
    {
        if (equippedWeapon.ProjectilePrefab != null && player != null)
        {
            var direction = (player.position - firePoint.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

            GameObject projectile = Instantiate(equippedWeapon.ProjectilePrefab, firePoint.position, firePoint.rotation);
            
            if (projectile.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = firePoint.up * equippedWeapon.ProjectileSpeed;
            }

            if (soundSource != null && equippedWeapon.ProjectileSound != null)
            {
                soundSource.PlayOneShot(equippedWeapon.ProjectileSound);
            }
        }
    }

}
