using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IEquipable_Item
{
    public Sprite inventory_sprite();

    public string Name();

    public void On_Item_Use(Player_State_Machine player);
    
    public string Pullout_Item_Animation();
}
