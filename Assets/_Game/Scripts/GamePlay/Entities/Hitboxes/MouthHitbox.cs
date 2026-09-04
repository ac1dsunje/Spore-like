using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
public class MouthHitbox: MonoBehaviour
{
    [Inject] private StomachModule _stomach;

    public void GetExperience()
    {
        _stomach.GetExperienceFromFood(1);
    }
}
}