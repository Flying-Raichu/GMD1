using UnityEngine;

// stores stats about the currently equipped weapon
public abstract class Weapon : ScriptableObject
{
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public AudioClip projectileSound;

    public abstract void Fire(Transform firePoint, GameObject shooter, AudioSource soundSource);

    public void TuneSound(AudioSource source)
    {
        source.pitch = Random.Range(0.8f, 1.2f);
    }
}
