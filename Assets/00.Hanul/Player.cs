using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    AnimController animController;

    //public float speed;
    //public float jumpPower;
    //bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<AnimController>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        //Move();
        Attack();
    }

    void Move()
    {
        /*
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            transform.localScale = new Vector2(-1f, 1f);
            if (!animController.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                animController.SetState(playerState.Run);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector2(1f, 1f);
            if (!animController.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                animController.SetState(playerState.Run);
            }
        }
        else if (!animController.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animController.SetState(playerState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animController.SetState(playerState.Jump);
            }
        }
        

        var velocity = rb.velocity;

        if (!isGrounded && velocity.y < 0)
        {
            animController.SetState(playerState.Fall);
        }
        */
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animController.anim.SetTrigger("Jab");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animController.anim.SetTrigger("Slash");
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */
}