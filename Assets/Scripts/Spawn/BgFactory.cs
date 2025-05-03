using UnityEngine;

namespace Spawn
{
    public class BackgroundFactory : SpawnFactory
    {
        [SerializeField] private GameObject bgPrefab;

        public override ISpawnable GetSpawnable()
        {
            GameObject bg = Instantiate(bgPrefab);
            Canvas canvas = bg.GetComponent<Canvas>();
            Camera cam = Camera.main ?? FindFirstObjectByType<Camera>();
            if (canvas != null && cam != null)
                canvas.worldCamera = cam;

            return bg.GetComponent<ISpawnable>();
        }
    }
}