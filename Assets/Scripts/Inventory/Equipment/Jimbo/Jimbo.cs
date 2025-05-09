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
    public string Pullout_Item_Animation()
    {
        return "Use_Item";
    }
    void Start()
    {
       
    }
    public void On_Item_Use(Player_State_Machine player)
    {
        
        Debug.Log("Helo Wolrd!!!1!11! ! 1   ");
           
        
        
    }




}
