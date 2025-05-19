using UnityEngine;

namespace Spawn
{
    public class EnemyFactory : SpawnFactory
    {
        [SerializeField] private EnemyManager managerPrefab;
        
        public override GameObject GetSpawnable()
        {
            GameObject spawnerInstance = Instantiate(managerPrefab.gameObject, Vector3.zero, Quaternion.identity);
            EnemyManager spawner = spawnerInstance.GetComponent<EnemyManager>();
            
            spawner.Initialize();
            
            return spawnerInstance;
        }
    }
}