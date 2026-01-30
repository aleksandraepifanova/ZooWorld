using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.Animals
{
    public interface IAnimalCollisionHandler
    {
        void OnAnimalCollision(Animal self, Collision collision);
    }
}

