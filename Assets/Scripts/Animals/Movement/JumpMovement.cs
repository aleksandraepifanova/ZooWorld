using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.World;


namespace ZooWorld.Animals
{
    public sealed class JumpMovement : IAnimalMovement
    {
        private readonly Rigidbody rb;
        private readonly WorldBounds bounds;
        private readonly float jumpDistance;
        private readonly float jumpInterval;

        private float timer;
        private Vector3 direction;

        public JumpMovement(
            Rigidbody rb,
            WorldBounds bounds,
            float jumpDistance,
            float jumpInterval
        )
        {
            this.rb = rb;
            this.bounds = bounds;
            this.jumpDistance = jumpDistance;
            this.jumpInterval = jumpInterval;

            PickNewDirection();
        }

        public void Tick(float dt)
        {
            timer += dt;
            if (timer >= jumpInterval)
            {
                timer = 0f;
                DoJump();
            }
        }

        public void FixedTick(float fixedDt)
        {
        }

        private void DoJump()
        {
            var targetPos = rb.position + direction * jumpDistance;

            if (!bounds.ContainsXZ(targetPos))
            {
                direction = (bounds.ClampToXZ(rb.position) - rb.position).normalized;
                targetPos = rb.position + direction * jumpDistance;
            }

            rb.MovePosition(targetPos);
            PickNewDirection();
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

