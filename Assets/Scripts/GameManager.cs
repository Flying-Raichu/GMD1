using System;
using System.Collections.Generic;
using Player;
using Spawn;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private SpawnFactory[] factories;
    private List<GameObject> createdPrefabs = new List<GameObject>();

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
            
            Debug.Log("added component " + spawnable.name);
        }
        
        if (PlayerManager.Instance != null)
        {
            PauseManager.Instance.Initialize(PlayerManager.Instance.GetPlayer());
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
}
