using UnityEngine;

namespace _Game.Scripts.Camera
{
[CreateAssetMenu(fileName = "NewCameraConfig", menuName = "Configs/Game/CameraConfig")]
public class CameraConfig: ScriptableObject
{
    [field: SerializeField, Range(0, 1)] public float FollowSpeed { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
}
}