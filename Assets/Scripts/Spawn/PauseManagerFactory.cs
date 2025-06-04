using UnityEngine;
using UnityEngine.EventSystems;

namespace Spawn
{
    public class PauseManagerFactory : SpawnFactory
    {
        [SerializeField] private GameObject pauseManagerPrefab;
        public override GameObject GetSpawnable()
        {
            GameObject obj = Instantiate(pauseManagerPrefab);
            return obj;
        }
    }
}