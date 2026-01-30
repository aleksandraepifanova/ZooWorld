using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ZooWorld.Game;


namespace ZooWorld.UI
{
    public sealed class StatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text preyText;
        [SerializeField] private TMP_Text predatorText;

        private void Start()
        {
            if (GameController.Instance?.Stats != null)
                GameController.Instance.Stats.Changed += Refresh;

            Refresh();
        }
        private void OnEnable()
        {
            if (GameController.Instance?.Stats != null)
                GameController.Instance.Stats.Changed += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            if (GameController.Instance?.Stats != null)
                GameController.Instance.Stats.Changed -= Refresh;
        }

        private void Refresh()
        {
            var stats = GameController.Instance?.Stats;
            if (stats == null) return;

            preyText.text = $"Prey dead: {stats.DeadPrey}";
            predatorText.text = $"Predators dead: {stats.DeadPredators}";
        }
    }
}

