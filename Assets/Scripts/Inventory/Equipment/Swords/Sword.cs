using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Sword : MonoBehaviour, IEquipable_Item
{

    [SerializeField]
    private Sprite Inventory_sprite_cranberry;
    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private AudioSource audioSource;
    private Collider2D HitBox;

    public int Damage;
    private Animator Sword_Animator;

    public LayerMask LayerMask;



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
        HitBox = GetComponent<Collider2D>();
        On_Item_Use(default);
    }
    public IEnumerator On_Item_Use(Player_State_Machine player)
    {
        

        audioSource.clip = audioClips[0];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
        player.State_Switch(player.State_Attacking);
        Sword_Animator.Play("Attack", 0, 0);





        yield return null;
        


    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        Idamagable Hit_Obj = collision.GetComponent<Idamagable>();
        if (Hit_Obj != null)
        {
            Hit_Obj.On_Damaged(Damage);
        }
    }











}
