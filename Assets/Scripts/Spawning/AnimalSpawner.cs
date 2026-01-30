using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZooWorld.World;


namespace ZooWorld.Spawning
{
    public sealed class AnimalSpawner : MonoBehaviour
    {
        [SerializeField] private WorldBounds bounds;

        [Header("Spawn timing")]
        [SerializeField] private float minInterval = 1f;
        [SerializeField] private float maxInterval = 2f;

        [Header("Prefabs")]
        [SerializeField] private GameObject[] animalPrefabs;

        private float timer;
        private float nextSpawnIn;

        private void Awake()
        {
            if (bounds == null)
                bounds = FindObjectOfType<WorldBounds>();

            ScheduleNext();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= nextSpawnIn)
            {
                timer = 0f;
                SpawnOne();
                ScheduleNext();
            }
        }

        private void ScheduleNext()
        {
            nextSpawnIn = Random.Range(minInterval, maxInterval);
        }

        private void SpawnOne()
        {
            if (bounds == null || animalPrefabs == null || animalPrefabs.Length == 0)
                return;

            var prefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

            var pos = GetRandomPointInBounds();
            var rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            Instantiate(prefab, pos, rot);
        }

        private Vector3 GetRandomPointInBounds()
        {
            var min = bounds.Min;
            var max = bounds.Max;

            var x = Random.Range(min.x, max.x);
            var z = Random.Range(min.z, max.z);

            return new Vector3(x, 0.5f, z);
        }
    }
}

