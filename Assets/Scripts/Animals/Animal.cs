using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalConfig config;

        public AnimalConfig Config => config;
        public AnimalFaction Faction => config.faction;

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

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}

