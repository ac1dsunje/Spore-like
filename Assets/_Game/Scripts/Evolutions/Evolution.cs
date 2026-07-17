using _Game.Scripts.Evolutions.Rarities;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class Evolution
{
    private readonly EvolutionConfig _config;
    private readonly EvolutionRarityConfig _rarity;
    
    public string Name { get; private set; }
    public float Value { get; private set; }
    public string Description { get; private set; }
    public Sprite Sprite { get; private set; }
    public Sprite Frame { get; private set; }
    
    public Evolution(EvolutionConfig config, EvolutionRarityConfig rarity)
    {
        _config = config;
        _rarity = rarity;
        
        ParseConfig();
        UseRarity();
    }

    private void ParseConfig()
    {
        Name = $"{_rarity.Name}  {_config.Name}";
        Description = _config.Description;
        Sprite = _config.Sprite;
    }

    private void UseRarity()
    {
        Value = _config.BasicValue * _rarity.Scaler;
        Frame = _rarity.Sprite;
    }
}
}