using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.World;


namespace ZooWorld.Animals
{
    public sealed class WanderMovement : IAnimalMovement
    {
        private readonly Rigidbody rb;
        private readonly WorldBounds bounds;
        private readonly float speed;
        private readonly float decisionInterval;

        private Vector3 direction;
        private float timer;

        public WanderMovement(
            Rigidbody rb,
            WorldBounds bounds,
            float speed,
            float decisionInterval
        )
        {
            this.rb = rb;
            this.bounds = bounds;
            this.speed = speed;
            this.decisionInterval = decisionInterval;

            PickNewDirection();
        }

        public void Tick(float dt)
        {
            timer += dt;
            if (timer >= decisionInterval)
            {
                timer = 0f;
                PickNewDirection();
            }
        }

        public void FixedTick(float fixedDt)
        {
            var nextPos = rb.position + direction * speed * fixedDt;

            if (!bounds.ContainsXZ(nextPos))
            {
                direction = (bounds.ClampToXZ(rb.position) - rb.position).normalized;
            }

            rb.MovePosition(nextPos);
        }

        private void PickNewDirection()
        {
            direction = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;
        }
    }
}

