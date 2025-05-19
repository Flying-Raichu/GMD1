using UnityEngine;

namespace Spawn
{
    public abstract class SpawnFactory : MonoBehaviour
    {
        public abstract GameObject GetSpawnable(); // instantiates and returns object

        public string GetLog(ISpawnable spawnable)
        {
            return "Spawned " + spawnable.Name;
        }
    }
}