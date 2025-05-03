using System.Collections.Generic;
using Spawn;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] 
    private SpawnFactory[] factories;
    private List<GameObject> createdPrefabs = new List<GameObject>();
    public GameObject playerInstance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        foreach (SpawnFactory s in factories)
        {
            ISpawnable spawnable = s.GetSpawnable();
            if (spawnable is Component component)
            {
                createdPrefabs.Add(component.gameObject);
                
                if (instance.CompareTag("Player"))
                {
                    playerInstance = instance.gameObject;
                    PauseManager.instance.Initialize(playerInstance);
                }
            }
        }
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }

    void Update()
    {
        if (InputManager.instance.PausePressed()) PauseManager.instance.TogglePause();
    }
}
