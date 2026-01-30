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
            var controller = GetComponent<AnimalMovementController>();
            if (controller == null) return;

            var bounds = FindObjectOfType<WorldBounds>();
            if (bounds == null)
            {
                Debug.LogError("WorldBounds not found in scene");
                return;
            }

            if (Faction == AnimalFaction.Prey)
            {
                controller.SetMovement(
                    new JumpMovement(
                        Rb,
                        bounds,
                        Config.moveSpeed,       
                        Config.decisionInterval  
                    )
                );
            }
            else
            {
                controller.SetMovement(
                    new LinearMovement(
                        Rb, 
                        bounds, 
                        Config.moveSpeed)
                );
            }

        }
        public virtual void Die()
        {
            if (isDead) return;
            isDead = true;

            Died?.Invoke(this);
            GameController.Instance?.Stats?.RegisterDeath(Faction);
            Destroy(gameObject);
        }

        public void NotifyAte()
        {
            Ate?.Invoke();
        }

    }
}

