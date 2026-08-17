using UnityEngine;

namespace _Game.Scripts.GamePlay.World
{
public class DayNightManager: MonoBehaviour
{
    [SerializeField] private float _step = 60;
    
    public float Value { get; private set; }

    private bool _isCountingUp;

    private void Update()
    {
        if (!_isCountingUp)
        {
            Value -= Time.deltaTime / _step;
        }
        else
        {
            Value += Time.deltaTime / _step;
        }

        if (Value >= 1f)
        {
            _isCountingUp = false;
        }
        else if (Value <= 0f)
        {
            _isCountingUp = true;
        }
    }
}
}