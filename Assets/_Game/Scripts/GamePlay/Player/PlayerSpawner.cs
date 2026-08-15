using UnityEngine;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private bool _spawnOnStart;

    private void Start()
    {
        if (_spawnOnStart) Spawn();
    }
    
    public void Spawn()
    {
        var spawnPos = new Vector3(
            transform.position.x + Random.Range(-5, 5),
            transform.position.y + Random.Range(-5, 5), 
            transform.position.z);
        
        var player = Instantiate(
            _playerPrefab,
            spawnPos,
            Quaternion.identity,
            transform);

        player.SetPlayer();
    }
}
}