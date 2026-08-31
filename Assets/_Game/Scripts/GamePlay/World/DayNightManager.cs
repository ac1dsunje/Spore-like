using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.GamePlay.World
{
public class DayNightManager: MonoBehaviour
{
    [SerializeField] private float _step = 60;
    [SerializeField] private Light2D _light;

    private float _value;

    private bool _isCountingUp;

    private void Update()
    {
        if (!_isCountingUp)
        {
            _value -= Time.deltaTime / _step;
        }
        else
        {
            _value += Time.deltaTime / _step;
        }

        if (_value >= 1f)
        {
            _isCountingUp = false;
        }
        else if (_value <= 0f)
        {
            _isCountingUp = true;
        }
        
        _light.color = new Color(_value, _value, _value, 1f);
    }
}
}