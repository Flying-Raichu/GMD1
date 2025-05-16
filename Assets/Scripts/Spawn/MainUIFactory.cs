using UnityEngine;

namespace Spawn
{
    public class MainMenuFactory : SpawnFactory
    {
        [SerializeField] private GameObject mainMenuPrefab;

        public override GameObject GetSpawnable()
        {
            GameObject ui = Instantiate(mainMenuPrefab);
            return ui;
        }
    }
}