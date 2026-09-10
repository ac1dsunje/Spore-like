using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
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

            switch (barConfig.BarType)
            {
                case BarType.Health:
                    bar.Construct(player.Model.Health, barConfig);
                    break;
                
                case BarType.Experience:
                    bar.Construct(player.Experience, barConfig);
                    break;
                
                case BarType.Endurance:
                    bar.Construct(player.Model.Endurance, barConfig);
                    break;
                
                case BarType.Hunger:
                    bar.Construct(player.Model.Stomach, barConfig);
                    break;
            }
        }
    }
}
}