using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class BarsPanelUI : MonoBehaviour
{
    [SerializeField] private BarUI _prefab;
    [SerializeField] private List<BarConfig> _barConfigs;

    public void Construct(EntityController player)
    {
        foreach (var barConfig in _barConfigs)
        {
            var bar = Instantiate(_prefab, transform);

            IStatWithLimit statWithLimit = barConfig.BarType switch
            {
                BarType.Health => player.Model.Health,
                BarType.Experience => player.Experience,
                BarType.Endurance => player.Model.Endurance,
                BarType.Hunger => player.Model.Stomach,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            bar.Construct(statWithLimit, barConfig);
        }
    }
}
}