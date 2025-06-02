using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour, Idamagable
{
    int Health;
    public bool Invincible = false;
    Animator animator;

    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private GameObject Hit_Effect;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void On_Damaged(int Health_Sub)
    {
        if (Invincible == false)
        {
            Instantiate(Hit_Effect).transform.position = this.transform.position;
            AudioSource.pitch = Random.Range(0.8f,1.2f);
            AudioSource.Play();
            animator.Play("Hurt", 0, 0);
            StartCoroutine(Invincible_Timer());
        }
    }

    private IEnumerator Invincible_Timer()
    {
        Invincible = true;
        yield return new WaitForSecondsRealtime(0.2f);
        Invincible = false;
    }
}
