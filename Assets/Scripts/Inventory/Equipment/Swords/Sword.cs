using UnityEngine;

public class Sword : MonoBehaviour, IEquipable_Item
{

    [SerializeField]
    private Sprite Inventory_sprite_cranberry;

    public int Damage;
    private Animator Sword_Animator;


    public string Name()
    {
        return "Steel_Sword";
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
        
        Sword_Animator = GetComponent<Animator>();
    }
    public void On_Item_Use(Player_State_Machine player)
    {
            
        player.State_Switch(player.State_Attacking);
        Sword_Animator.Play("Attack",0,0);
        
        
    }



}
