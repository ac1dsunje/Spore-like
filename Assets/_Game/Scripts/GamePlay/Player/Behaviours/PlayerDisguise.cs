using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerDisguise : MonoBehaviour, IDisguisable
{
    [Inject] private ItemAnimation _animation;
    [Inject] private DisguiseModule _disguise;

    public bool IsDetected(float sensorics, bool xRay)
    {
        var show = _disguise.TryNotice(sensorics, xRay);
        
        Debug.Log($"{gameObject.name}: detected {show}");

        return show;
    }
}
}