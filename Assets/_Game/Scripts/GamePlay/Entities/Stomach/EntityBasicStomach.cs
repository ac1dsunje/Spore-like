using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Stomach
{
public class EntityBasicStomach: IStartable, ITickable
{
    [Inject] private StomachModule _stomach;
    [Inject] private BuffsModule _buffs;

    private const float LoseHungerTime = 5f;
    private float _loseHungerTimer;

    public void Start()
    {
        _loseHungerTimer = LoseHungerTime;
    }
    
    public void Tick()
    {
        if (_loseHungerTimer > 0f)
        {
            _loseHungerTimer -= Time.deltaTime;

            if (_loseHungerTimer <= 0f)
            {
                _loseHungerTimer = LoseHungerTime;
                _stomach.LoseHunger(1);
            }
        }
        
        _buffs.Set(BuffType.Overeating, _stomach.Hunger > _stomach.MaxHunger);
        _buffs.Set(BuffType.Starvation, _stomach.Hunger <= 0f);
    }
}
}