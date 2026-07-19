using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class Evolution
{
    private readonly EvolutionConfig _config;
    private EvolutionRarityConfig _rarity;
    
    public string Name { get; private set; }
    public float Value { get; private set; }
    public string Description { get; private set; }
    public Sprite Sprite { get; private set; }
    public Sprite Frame { get; private set; }
    
    public Evolution(EvolutionConfig config)
    {
        _config = config;
        ParseConfig();
    }

    private void ParseConfig()
    {
        Name = $"{_config.Name}";
        Description = _config.Description;
        Sprite = _config.Sprite;
    }

    public void SetRarity(EvolutionRarityConfig rarity)
    {
        _rarity = rarity;
        Name = $"{_rarity.Name} {_config.Name}";
        Value = _config.BasicValue * _rarity.Scaler;
        Frame = _rarity.Sprite;
    }
}
}