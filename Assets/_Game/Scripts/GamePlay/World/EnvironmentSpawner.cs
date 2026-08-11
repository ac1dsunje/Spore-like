using _Game.Scripts.GamePlay.World.Biomes;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{
public class EnvironmentSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
     
    private WorldGenerator _generator;
    
    [Inject]
    private void Construct(WorldGenerator generator)
    {
        _generator = generator;
        _generator.OnTilePlaced += TryPlaceEnvironment;
    }

    private bool CanPlaceObject(float chance) => Random.Range(0, 100) >= chance;

    private void TryPlaceEnvironment(Vector3Int position, Biome biome, Transform parent)
    {
        if (!CanPlaceObject(biome.Config.ChanceEnvironment)) return;
        
        var environment = biome.Config.GetRandomEnvironment();
        var foodItem = environment.FoodItems[Random.Range(0, environment.FoodItems.Length)];
        var setPos = new Vector3(position.x + 0.5f, position.y + 0.5f, position.z);
        var item = Instantiate(_foodPrefab, setPos, Quaternion.identity, parent).GetComponent<FoodItem>();
        item.Construct(foodItem);
    }

    private void OnDestroy()
    {
        _generator.OnTilePlaced -= TryPlaceEnvironment;
    }
}
}