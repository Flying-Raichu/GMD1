using System;
using System.Collections.Generic;
using Spawn;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private SpawnFactory[] factories;
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
            GameObject spawnable = s.GetSpawnable();
            createdPrefabs.Add(spawnable);
            
            if (instance.CompareTag("Player"))
            {
                playerInstance = instance.gameObject;
                PauseManager.instance.Initialize(playerInstance);
            }
            
            Debug.Log("added component " + spawnable.name);
        }
    }
    
    public T GetComponentInPrefab<T>() where T : Component
    {
        foreach (var prefab in createdPrefabs)
        {
            T comp = prefab.GetComponentInChildren<T>(true); // true = include inactive
            if (comp != null) return comp;
        }
        return null;
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
