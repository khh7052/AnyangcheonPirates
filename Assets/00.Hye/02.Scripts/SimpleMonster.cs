using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleMonster : MonoBehaviour
{
    public UnityEvent OnInit = new();
    public UnityEvent OnDie = new();

    public float limitForce = 10f; // 해당 힘 이상으로 받으면 죽음

    private Animator anim;
    private Rigidbody2D rigd;
    private Collider2D coll;
    public string dieSfxName;


    private bool isDie = false;
    

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        if (rigd == null)
            rigd = GetComponent<Rigidbody2D>();
        if(coll == null)
            coll = GetComponent<Collider2D>();

        if(dieSfxName == "")
        {
            string[] ss = gameObject.name.Split();
            dieSfxName = ss[0];
        }

        anim.SetBool("Idle", true);
        rigd.isKinematic = false;
        coll.enabled = true;

        isDie = false;

        OnInit.Invoke();
    }

    public void Die()
    {
        if (isDie) return;

        isDie = true;
        anim.SetBool("Idle", false);
        anim.SetTrigger("Die");
        //rigd.isKinematic = true;
        //coll.enabled = false;
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySFX(dieSfxName);

        OnDie.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magic") || collision.CompareTag("Abs_Magic"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= limitForce)
        {
            Die();
        }
    }

}
