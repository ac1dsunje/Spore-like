using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class SocialModule : StatModule
{
    public float Influence { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.Influence, UpdateInfluence);
    }

    private void UpdateInfluence(float value) => Influence = value;
}
}