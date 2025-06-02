using Player;
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
        if (GameManager.instance != null && PlayerManager.Instance.GetPlayer() != null)
        {
            player = PlayerManager.Instance.GetPosition();
        }
    }


    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= (1 / equippedWeapon.fireRate))
        {
            Fire();
            timeSinceLastShot = 0f;
        }
    }

    private void Fire()
    {
        if (equippedWeapon.projectilePrefab && player)
        {
            equippedWeapon.TuneSound(soundSource);
            equippedWeapon.Fire(firePoint, gameObject, soundSource);
        }
    }

}
