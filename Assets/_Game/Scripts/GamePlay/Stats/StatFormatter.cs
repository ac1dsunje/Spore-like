using _Game.Scripts.GamePlay;

public static class StatFormatter
{
    public static string Format(SourceStat stat)
    {
        return stat.Operation switch
        {
            StatOperation.Add => $"{stat.Type}{(stat.CurrentValue > 0 ? " +" : " ")}{stat.CurrentValue}",
            StatOperation.Multiply => $"{stat.Type} *{stat.CurrentValue}",
            StatOperation.Percent => $"{stat.Type} {stat.CurrentValue}%",
            _ => $"{stat.Type} {stat.CurrentValue}"
        };
    }
}