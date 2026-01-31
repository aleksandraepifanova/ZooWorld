using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.Infrastructure;
using ZooWorld.World;
using ZooWorld.Spawning;
using ZooWorld.Animals;


namespace ZooWorld.Game
{
    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public ServiceContainer Services { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Services = new ServiceContainer();

            var bounds = FindObjectOfType<WorldBounds>();
            Services.Register(bounds);

            var stats = new GameStats();
            Services.Register(stats);

            var factory = new AnimalMovementFactory(Services);
            Services.Register(factory);
        }
    }
}

