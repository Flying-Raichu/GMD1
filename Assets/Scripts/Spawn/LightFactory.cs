using UnityEngine;

namespace Spawn
{
    public class LightFactory : SpawnFactory
    {
        [SerializeField] private GameObject lightPrefab;

        public override GameObject GetSpawnable()
        {
            GameObject mainLight = Instantiate(lightPrefab);
            return mainLight;
        }
    }
}