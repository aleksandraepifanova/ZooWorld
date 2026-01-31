using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.Infrastructure;
using ZooWorld.World;

namespace ZooWorld.Animals
{
    public sealed class AnimalMovementFactory
    {
        private readonly ServiceContainer services;

        public AnimalMovementFactory(ServiceContainer services)
        {
            this.services = services;
        }

        public IAnimalMovement CreateFor(Animal animal)
        {
            var cfg = animal.Config;
            var rb = animal.Rb;

            var bounds = services.Resolve<WorldBounds>();
            if (bounds == null) return null;

            return cfg.movementKind switch
            {
                AnimalMovementKind.Wander =>
                    new WanderMovement(rb, bounds, cfg.moveSpeed, cfg.decisionInterval),

                AnimalMovementKind.Linear =>
                    new LinearMovement(rb, bounds, cfg.moveSpeed),

                AnimalMovementKind.Jump =>
                    new JumpMovement(rb, bounds, cfg.jumpDistance, cfg.jumpInterval),

                _ => null
            };
        }
    }
}

