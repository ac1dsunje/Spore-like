using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.World
{
public class DayNightManager: MonoBehaviour
{
    [SerializeField] private float _step = 60;
    
    [Inject] private Light2D _globalLight;

    private float _value;

    private bool _isCountingUp;

    private void Awake()
    {
        _value = _globalLight.color.r;
    }

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
        
        _globalLight.color = new Color(_value, _value, _value, 1f);
    }
}
}