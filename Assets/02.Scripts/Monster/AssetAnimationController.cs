using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class AssetAnimationController : MonoBehaviour
{
    SpriteResolver spriteResolver;

    private Animator animator;
    public Animator Animator
    {
        get
        {
            if(animator == null)
                animator = GetComponent<Animator>();

            return animator;
        }
    }

    public MonsterState animState;

    public string idleKey = "Idle";
    public string walkKey = "Walk";
    public string runKey = "Run";
    public string jumpKey = "Jump";
    public string attackKey = "Attack";
    public string dieKey = "Die";
    public string hitKey = "Hit";
    public string healKey = "Heal";
    
    public string animKey;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteResolver = GetComponentInChildren<SpriteResolver>();
    }

    private void Update()
    {
        print(gameObject.name + " " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    public void Idle(bool active)
    {
        print("SetIdle : " + active);

        if (active)
        {
            animState = MonsterState.Idle;
            Walk(false);
        }

        Animator.SetBool(idleKey, active);
    }

    public void Walk(bool active)
    {
        if (active)
        {
            animState = MonsterState.Trace;
            Idle(false);
        }

        Animator.SetBool(walkKey, active);
    }

    public void Run(bool active)
    {
        Animator.SetBool(runKey, active);
    }

    public void Jump(bool active)
    {
        Animator.SetBool(jumpKey, active);
    }

    public void Attack()
    {
        animState = MonsterState.Attack;
        Animator.SetTrigger(attackKey);
        Idle(true);
    }

    public void Die()
    {
        animState = MonsterState.Die;
        Idle(false);
        Walk(false);

        Animator.SetTrigger(dieKey);
    }

    public void Hit()
    {
        Animator.SetTrigger(hitKey);
    }

    public void Heal()
    {
        Animator.SetTrigger(healKey);
    }

}
