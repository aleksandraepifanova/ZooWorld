using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ZooWorld.Game;
using Unity.VisualScripting.Antlr3.Runtime.Misc;


namespace ZooWorld.UI
{
    public sealed class StatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text preyText;
        [SerializeField] private TMP_Text predatorText;

        private GameStats stats;

        private void Start()
        {
            stats = GameController.Instance?.Services?.Resolve<GameStats>();
            if (stats != null)
                stats.Changed += Refresh;

            Refresh();
        }
        private void OnEnable()
        {
            stats = GameController.Instance?.Services?.Resolve<GameStats>();
            if (stats != null)
                stats.Changed += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            if (stats != null)
                stats.Changed -= Refresh;
        }

        private void Refresh()
        {
            if (stats == null) return;

            preyText.text = $"Prey dead: {stats.DeadPrey}";
            predatorText.text = $"Predators dead: {stats.DeadPredators}";
        }
    }
}

