using UnityEngine;

namespace Spawn
{
    public class PlayerFactory : SpawnFactory
    {
        [SerializeField] private GameObject playerPrefab;

        public override ISpawnable GetSpawnable()
        {
            GameObject playerInstance = Instantiate(playerPrefab);
            playerInstance.layer = LayerMask.NameToLayer("Player");
            
            return playerInstance.GetComponent<ISpawnable>();
        }
    }
}