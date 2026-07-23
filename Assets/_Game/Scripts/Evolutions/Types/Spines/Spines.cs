namespace _Game.Scripts.Evolutions.Types.Spines
{
public class Spines: Evolution
{
    public Spines(EvolutionConfig config) : base(config) {}

    public override void Apply()
    {
        base.Apply();
        Player.Attack.OnDamageReflected += OnDamageReflected;
    }

    private void OnDamageReflected(int value)
    {
        UpdateExperience(value);
    }

    public override void Dispose()
    {
        if (Player == null) return;
            
        Player.Attack.OnDamageReflected -= OnDamageReflected;
    }
}
}