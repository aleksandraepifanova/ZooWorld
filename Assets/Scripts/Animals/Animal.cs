using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.World;
using ZooWorld.Game;


namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalConfig config;

        public AnimalConfig Config => config;
        public AnimalFaction Faction => config.faction;
        public event Action<Animal> Died;
        public event Action Ate;

        private bool isDead;

        public Rigidbody Rb { get; private set; }

        protected virtual void Awake()
        {
            if (config == null)
            {
                Debug.LogError($"{name}: AnimalConfig is not assigned.", this);
            }

            Rb = GetComponent<Rigidbody>();
            Rb.mass = config != null ? config.mass : 1f;

            Rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        protected virtual void Start()
        {
            if (config == null) { enabled = false; return; }

            var controller = GetComponent<AnimalMovementController>();
            if (controller == null) return;

            var factory = Game.GameController.Instance?.Services?.Resolve<AnimalMovementFactory>();
            if (factory == null)
            {
                Debug.LogError("AnimalMovementFactory not found");
                return;
            }

            var movement = factory.CreateFor(this);
            controller.SetMovement(movement);
        }

        public virtual void Die()
        {
            if (isDead) return;
            isDead = true;

            Died?.Invoke(this);
            var stats = GameController.Instance?.Services?.Resolve<GameStats>();
            stats?.RegisterDeath(Faction);
            Destroy(gameObject);
        }

        public void NotifyAte()
        {
            Ate?.Invoke();
        }

    }
}

