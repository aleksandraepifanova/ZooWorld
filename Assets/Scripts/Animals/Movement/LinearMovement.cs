using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.World;


namespace ZooWorld.Animals
{
    public sealed class LinearMovement : IAnimalMovement
    {
        private readonly Rigidbody rb;
        private readonly WorldBounds bounds;
        private readonly float speed;

        private Vector3 direction;

        public LinearMovement(Rigidbody rb, WorldBounds bounds, float speed)
        {
            this.rb = rb;
            this.bounds = bounds;
            this.speed = Mathf.Max(0f, speed);

            PickInitialDirection();
        }

        public void Tick(float dt)
        {
        }

        public void FixedTick(float fixedDt)
        {
            var nextPos = rb.position + direction * speed * fixedDt;

            if (!bounds.ContainsXZ(nextPos))
            {
                var inward = (bounds.ClampToXZ(rb.position) - rb.position);
                direction = inward.sqrMagnitude > 0.0001f ? inward.normalized : -direction;

                nextPos = rb.position + direction * speed * fixedDt;
            }

            rb.MovePosition(nextPos);
        }

        private void PickInitialDirection()
        {
            direction = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;

            if (direction.sqrMagnitude < 0.0001f)
                direction = Vector3.forward;
        }
    }
}

