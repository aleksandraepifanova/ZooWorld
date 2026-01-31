using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.Animals;


namespace ZooWorld.UI
{
    [RequireComponent(typeof(Animal))]
    public sealed class TastyPopupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject popupPrefab;
        [SerializeField] private Vector3 localOffset = new Vector3(0f, 0f, -0.7f);
        [SerializeField] private float lifetime = 0.8f;

        private Animal animal;

        private void Awake()
        {
            animal = GetComponent<Animal>();
        }

        private void OnEnable()
        {
            if (animal != null)
                animal.Ate += OnAte;
        }

        private void OnDisable()
        {
            if (animal != null)
                animal.Ate -= OnAte;
        }

        private void OnAte()
        {
            if (popupPrefab == null) return;

            var go = Instantiate(popupPrefab, transform);
            var follower = go.AddComponent<TastyPopupFollower>();

            follower.Init(transform, localOffset, lifetime);
        }
    }
}

