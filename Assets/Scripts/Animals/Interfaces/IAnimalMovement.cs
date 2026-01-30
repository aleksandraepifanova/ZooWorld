using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.Animals
{
    public interface IAnimalMovement
    {
        void Tick(float dt);
        void FixedTick(float fixedDt);
    }
}

