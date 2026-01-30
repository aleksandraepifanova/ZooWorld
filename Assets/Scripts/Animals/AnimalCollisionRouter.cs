using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Animal))]
    public sealed class AnimalCollisionRouter : MonoBehaviour
    {
        private Animal self;

        private void Awake()
        {
            self = GetComponent<Animal>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var other = collision.collider.GetComponentInParent<Animal>();
            if (other == null) return;
            if (other == self) return;

            ResolveFoodChain(self, other);
        }

        private static void ResolveFoodChain(Animal a, Animal b)
        {
            if (a.Faction == AnimalFaction.Prey && b.Faction == AnimalFaction.Prey)
                return;

            if (a.Faction == AnimalFaction.Predator && b.Faction == AnimalFaction.Prey)
            {
                b.Die();
                a.NotifyAte();
                return;
            }

            if (a.Faction == AnimalFaction.Prey && b.Faction == AnimalFaction.Predator)
            {
                a.Die();
                b.NotifyAte();
                return;
            }

            if (a.Faction == AnimalFaction.Predator && b.Faction == AnimalFaction.Predator)
            {
                var aWins = a.GetInstanceID() < b.GetInstanceID();
                if (aWins)
                {
                    b.Die();
                    a.NotifyAte();
                }
                else
                {
                    a.Die();
                    b.NotifyAte();
                }
            }
        }
    }
}

