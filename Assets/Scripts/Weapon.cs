using UnityEngine;

// stores stats about the currently equipped weapon
[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject 
{
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip projectileSound;
    
    public int Damage { get => damage; set => damage = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public GameObject ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public AudioClip ProjectileSound { get => projectileSound; set => projectileSound = value; }
}
