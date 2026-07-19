using UnityEngine;

namespace _Game.Scripts.Camera
{
[CreateAssetMenu(fileName = "NewCameraConfig", menuName = "Configs/Game/Camera")]
public class CameraConfig: ScriptableObject
{
    [field: SerializeField] public float FollowSpeed { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
}
}