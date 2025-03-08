using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile_Data_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Tile_Data_Script> _tileData;
    public Dictionary<TileBase, Tile_Data_Script> _DataFromTiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _DataFromTiles = new Dictionary<TileBase, Tile_Data_Script>();
        foreach (var tileData in _tileData)
        {
            foreach (var tile in tileData.tiles)
            {
                _DataFromTiles.Add(tile, tileData);
            }

        }
    }


}
