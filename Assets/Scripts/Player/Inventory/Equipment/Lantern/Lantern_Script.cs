using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern_Script : MonoBehaviour, IEquipable_Item
{
    [SerializeField]
    private Light2D Light_Source;
    [SerializeField]
    private Sprite Inventory_sprite_cranberry;


    public string Name()
    {
        return "Lantern";
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
        Light_Source.enabled = true;

    }
    public void On_Item_Use(Player_State_Machine player)
    {
            

            Light_Source.enabled = !Light_Source.enabled;
        
        
    }




}
