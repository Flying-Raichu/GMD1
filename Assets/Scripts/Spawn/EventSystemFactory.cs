using UnityEngine;

namespace Spawn
{
    public class EventSystemFactory : SpawnFactory
    {
        [SerializeField] private GameObject eventSystemPrefab;

        public override GameObject GetSpawnable()
        {
            GameObject eventSystem = Instantiate(eventSystemPrefab);
            return eventSystem;
        }
    }
}