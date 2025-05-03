using UnityEngine;

namespace Spawn
{
    public class EventSystemFactory : SpawnFactory
    {
        [SerializeField] private GameObject eventSystemPrefab;

        public override ISpawnable GetSpawnable()
        {
            GameObject eventSystem = Instantiate(eventSystemPrefab);
            return eventSystem.GetComponent<ISpawnable>();
        }
    }
}