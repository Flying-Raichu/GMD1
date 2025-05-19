using UnityEngine;

namespace Spawn
{
    public class AsteroidFactory : SpawnFactory
    {
        [SerializeField] private AsteroidSpawner spawnerPrefab;
        
        public override GameObject GetSpawnable()
        {
            GameObject spawnerInstance = Instantiate(spawnerPrefab.gameObject, Vector3.zero, Quaternion.identity);
            AsteroidSpawner spawner = spawnerInstance.GetComponent<AsteroidSpawner>();
            
            spawner.Initialize();
            
            return spawnerInstance;
        }
    }
}