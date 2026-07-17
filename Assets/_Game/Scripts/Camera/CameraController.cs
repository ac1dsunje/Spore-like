using UnityEngine;

namespace _Game.Scripts.Camera
{
public class CameraController: MonoBehaviour
{
    [SerializeField] private CameraConfig _config;
    
    private Transform _target;

    public void Construct(Transform target)
    {
        _target = target;
    }
    
    private void LateUpdate()
    {
        if (!_target) return;
        
        var targetPos = new Vector3(_target.position.x + _config.Offset.x, _target.position.y + _config.Offset.y, _target.position.z + _config.Offset.z);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _config.FollowSpeed * Time.deltaTime);
    }
}
}