using _Game.Scripts.Rarities;
using _Game.Scripts.World.Food;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.World.Chunk
{
public class Chunk: MonoBehaviour
{
    [SerializeField] private ChunkConfig _config;
    [SerializeField] private GameObject[] _plantPrefabs;
    [SerializeField] private Transform[] _plantPositions;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;

    private void Awake()
    {
        SpawnPlants();
    }

    private void SpawnPlants()
    {
        foreach (var pos in  _plantPositions)
        {
            if (!IsAbleToPlant()) continue;

            SetRandomPlant(pos.position);
        }
    }

    private bool IsAbleToPlant()
    {
        var rand = Random.Range(0, 100);
        return rand < _config.PlantChance;
    }

    private void SetRandomPlant(Vector2 pos)
    {
        var randPlant = Random.Range(0, _plantPrefabs.Length);
            
        var plant = Instantiate(_plantPrefabs[randPlant], pos, Quaternion.identity, transform).GetComponent<FoodItem>();
        plant.SetRarity(_raritiesDatabase);
    }
}
}