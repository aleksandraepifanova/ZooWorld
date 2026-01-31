using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZooWorld.World;


namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Animal))]
    public sealed class AnimalMovementController : MonoBehaviour
    {
        private Animal animal;
        private IAnimalMovement movement;
        private WorldBounds bounds;

        [Header("Plane Lock")]
        [SerializeField] private bool lockY = true;
        private float fixedY;

        private void Awake()
        {
            animal = GetComponent<Animal>();
            fixedY = transform.position.y;
            bounds = FindObjectOfType<WorldBounds>();
        }

        public void SetMovement(IAnimalMovement newMovement)
        {
            movement = newMovement;
        }

        private void Update()
        {
            movement?.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            movement?.FixedTick(Time.fixedDeltaTime);
            if (lockY && animal != null && animal.Rb != null)
            {
                var p = animal.Rb.position;
                if (!Mathf.Approximately(p.y, fixedY))
                {
                    p.y = fixedY;
                    if (bounds != null)
                    {
                        p = bounds.ClampToXZ(p);
                    }
                    animal.Rb.position = p;
                }
            }
        }
    }
}

