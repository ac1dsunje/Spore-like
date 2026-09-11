using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Health;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class BarsPanelUI : MonoBehaviour
{
    [SerializeField] private BarUI _prefab;
    [SerializeField] private List<BarConfig> _barConfigs;

    public void Construct(EntityScope player)
    {
        foreach (var barConfig in _barConfigs)
        {
            var bar = Instantiate(_prefab, transform);

            switch (barConfig.BarType)
            {
                case BarType.Health:
                    bar.Construct(player.Get<HealthModule>().Current, player.Get<HealthModule>().Max, barConfig);
                    break;
                case BarType.Experience:
                    bar.Construct(player.Get<ExperienceModule>().Current, player.Get<ExperienceModule>().Max, barConfig);
                    break;
                case BarType.Endurance:
                    bar.Construct(player.Get<EnduranceModule>().Current, player.Get<EnduranceModule>().Max, barConfig);
                    break;
                case BarType.Hunger:
                    bar.Construct(player.Get<StomachModule>().Current, player.Get<StomachModule>().Max, barConfig);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }
    }
}
}