using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(fileName = "AnimalConfig", menuName = "ZooWorld/Animal Config")]
    public sealed class AnimalConfig : ScriptableObject
    {
        [Header("Identity")]
        public string animalName;
        public AnimalFaction faction;

        [Header("Movement")]
        [Min(0f)] public float moveSpeed = 2f;
        [Min(0f)] public float decisionInterval = 1f;

        [Header("Physics")]
        [Min(0.01f)] public float mass = 1f;

        [Header("Prefab")]
        public GameObject prefab;
    }
}

