using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerXRay : MonoBehaviour
{
    private VisionModule _vision;
    private CircleCollider2D _collider;

    [Inject]
    private void Construct(VisionModule vision)
    {
        _collider = GetComponent<CircleCollider2D>();
        _vision = vision;
        _vision.OnXRayUpdated += UpdateXRay;
        UpdateXRay(_vision.XRayRadius, false);
    }

    private void UpdateXRay(float radius, bool state)
    {
        _collider.radius = radius;
        _collider.enabled = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDisguisable disguiseAble)) return;

        _vision.EnterXRay(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDisguisable disguiseAble)) return;

        _vision.ExitXRay(other.gameObject);
    }

    private void OnDestroy()
    {
        _vision.OnXRayUpdated -= UpdateXRay;
    }
}
}