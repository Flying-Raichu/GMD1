using UnityEngine;

namespace Spawn
{
    public class LightFactory : SpawnFactory
    {
        [SerializeField] private GameObject lightPrefab;

        public override ISpawnable GetSpawnable()
        {
            if (FindFirstObjectByType<Light>() == null)
            {
                GameObject mainLight = Instantiate(lightPrefab);
                return mainLight.GetComponent<ISpawnable>();
            }

            return null;
        }
    }
}