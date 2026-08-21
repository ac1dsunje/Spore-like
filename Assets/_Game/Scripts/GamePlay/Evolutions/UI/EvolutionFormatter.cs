using System.Text;
using _Game.Scripts.GamePlay.Types;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public class EvolutionFormatter
{
    private StatTypeConfig _statsConfig;

    [Inject] 
    private void Construct(StatTypeConfig statsConfig)
    {
        _statsConfig = statsConfig;
    }
    
    private string FormatStats(SourceStat stat)
    {
        var statItem = _statsConfig.Get(stat.Type);
        var statTypeName = statItem.Name;
        var name = statTypeName != "" ? statTypeName : stat.Type.ToString();

        name = $"{name} <sprite name=\"{statItem.Sprite.name}\">";
        
        return stat.Operation switch
        {
            StatOperation.Add => $"{name} {(stat.CurrentValue > 0 ? "+" : "")}{stat.CurrentValue}",
            StatOperation.Multiply => $"{name} *{stat.CurrentValue}",
            StatOperation.Percent => $"{name} {stat.CurrentValue}%",
            _ => $"{name} {stat.CurrentValue}"
        };
    }

    public string FormatDescription(Evolution evolution)
    {
        var text = new StringBuilder();

        foreach (var stat in evolution.Stats)
        {
            text.AppendLine(FormatStats(stat));
        }

        if (evolution.Config.Abilities != null)
        {
            foreach (var ability in evolution.Config.Abilities)
            {
                text.AppendLine($"Grants ability to {ability.Type}");
            }
        }

        return text.ToString();
    }
}
}