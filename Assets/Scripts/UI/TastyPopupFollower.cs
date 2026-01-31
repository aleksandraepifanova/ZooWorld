using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.UI
{
    public sealed class TastyPopupFollower : MonoBehaviour
    {
        private Transform target;
        private Vector3 offset;
        private float lifetime;
        private float timer;
        private Camera cam;

        public void Init(Transform target, Vector3 worldOffset, float lifetime)
        {
            this.target = target;
            this.offset = worldOffset;
            this.lifetime = lifetime;

            cam = Camera.main;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = target.position + offset;

            if (cam != null)
            {
                transform.rotation = Quaternion.LookRotation(cam.transform.forward);
            }

            timer += Time.deltaTime;
            if (timer >= lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}

