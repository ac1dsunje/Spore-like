using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(CircleCollider2D))]
public class PickerHitbox: MonoBehaviour
{
    private CircleCollider2D _collider;
    
    [Inject] private StomachModule _stomach;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    
    public void SetSize(float size)
    {
        _collider.radius = size;
    }

    public void GetFood()
    {
        _stomach.GetExperienceFromFood(1);
    }
}
}