using System;
using Spawn;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour, ISpawnable
    {
        public static PlayerManager Instance { get; private set; }
        [SerializeField] private GameObject playerPrefab;
        public string Name { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Initialize()
        {
            gameObject.name = "PlayerManager";
            playerPrefab = Instantiate(playerPrefab);
        }

        public Transform GetPosition()
        {
            return playerPrefab.transform;
        }

        public GameObject GetPlayer()
        {
            return playerPrefab;
        }
    }
}