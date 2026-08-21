using System.Text;
using _Game.Scripts.GamePlay.Types;
using VContainer;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public class EvolutionFormatter
{
    [Inject] private StatTypeConfig _statsConfig;
    
    private string FormatStats(SourceStat stat)
    {
        var statItem = _statsConfig.Get(stat.Type);
        var name = $"{statItem.Name} <sprite name=\"{statItem.Sprite.name}\">";
        
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
                text.AppendLine($"Grants ability to {ability.Type} <sprite name=\"{ability.Sprite.name}\">");
            }
        }

        return text.ToString();
    }
}
}