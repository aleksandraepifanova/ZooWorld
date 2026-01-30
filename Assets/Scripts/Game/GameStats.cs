using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using ZooWorld.Animals;


namespace ZooWorld.Game
{
    public sealed class GameStats
    {
        public int DeadPrey { get; private set; }
        public int DeadPredators { get; private set; }

        public event Action Changed;

        public void RegisterDeath(AnimalFaction faction)
        {
            if (faction == AnimalFaction.Prey) DeadPrey++;
            else DeadPredators++;

            Changed?.Invoke();
        }
    }
}

