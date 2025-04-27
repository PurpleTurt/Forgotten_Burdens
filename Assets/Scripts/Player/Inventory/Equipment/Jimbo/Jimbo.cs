using UnityEngine;

public class Jimbo : MonoBehaviour, IEquipable_Item
{

    [SerializeField]
    private Sprite Inventory_sprite_cranberry;

    public string Name()
    {
        return "Jimbo";
    }
    public Sprite inventory_sprite()
    {
        return Inventory_sprite_cranberry;
    }

    void Start()
    {
       
    }
    public void On_Item_Use(float Holding_Input)
    {
        
        Debug.Log("Helo Wolrd!!!1!11! ! 1   ");
           
        
        
    }




}
