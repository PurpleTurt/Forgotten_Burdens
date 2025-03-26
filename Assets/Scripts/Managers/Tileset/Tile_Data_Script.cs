
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class Tile_Data_Script : ScriptableObject
{

    public Vector2 Angle_Dir;
    //0-dirt
    //1-grass
    public int Ground_Type;
    public Tile[] tiles;


}
