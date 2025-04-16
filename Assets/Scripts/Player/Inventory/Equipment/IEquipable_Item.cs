using Unity.VisualScripting;
using UnityEngine;

public interface IEquipable_Item
{
    public Sprite inventory_sprite();
    public void On_Item_Use();
    
}
