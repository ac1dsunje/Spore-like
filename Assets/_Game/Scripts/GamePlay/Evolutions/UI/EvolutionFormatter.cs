using System.Text;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public static class EvolutionFormatter
{
    private static string FormatStats(SourceStat stat)
    {
        return stat.Operation switch
        {
            StatOperation.Add => $"{stat.Type}{(stat.CurrentValue > 0 ? " +" : " ")}{stat.CurrentValue}",
            StatOperation.Multiply => $"{stat.Type} *{stat.CurrentValue}",
            StatOperation.Percent => $"{stat.Type} {stat.CurrentValue}%",
            _ => $"{stat.Type} {stat.CurrentValue}"
        };
    }

    public static string FormatDescription(Evolution evolution)
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