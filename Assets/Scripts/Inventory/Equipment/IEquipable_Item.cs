using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IEquipable_Item
{
    public Sprite inventory_sprite();

    public string Name();

    public IEnumerator On_Item_Use(Player_State_Machine player);
    
    public string Pullout_Item_Animation();
}
