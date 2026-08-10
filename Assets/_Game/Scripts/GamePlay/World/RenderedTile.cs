using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World
{
public readonly struct RenderedTile
{
    public readonly Biome Biome;
    public readonly TileBase Tile;

    public RenderedTile(Biome biome, TileBase tile)
    {
        Biome = biome;
        Tile = tile;
    }
}
}