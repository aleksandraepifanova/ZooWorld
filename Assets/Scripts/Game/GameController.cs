using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.Game
{
    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public GameStats Stats { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Stats = new GameStats();
        }
    }
}

