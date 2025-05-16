using UnityEngine;

namespace Spawn
{
    public class CameraFactory : SpawnFactory
    {
        [SerializeField] private GameObject cameraPrefab;
        public override GameObject GetSpawnable()
        {
            GameObject mainCamera = Instantiate(cameraPrefab);
            return mainCamera;
        }
    }
}