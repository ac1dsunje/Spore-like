using _Game.Scripts.World.Food;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.World.Chunk
{
public class Chunk: MonoBehaviour
{
    [SerializeField] private ChunkConfig _config;
    [SerializeField] private GameObject _plantPrefab;
    [SerializeField] private Transform[] _plantPositions;

    private void Awake()
    {
        SpawnPlants();
    }

    private void SpawnPlants()
    {
        foreach (var pos in  _plantPositions)
        {
            var rand = Random.Range(0, 100);
            if (!(rand < _config.PlantChance)) continue;
            
            var plant = Instantiate(_plantPrefab, pos.position, Quaternion.identity, transform).GetComponent<FoodItem>();
            var randIndex = Random.Range(0, _config.Foods.Length);
            plant.SetConfig(_config.Foods[randIndex]);
        }
    }
}
}