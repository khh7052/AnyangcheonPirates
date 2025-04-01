using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        anim.SetBool("Idle", true);
    }

    public void SetState(playerState state)
    {
        foreach (var variable in new[] { "Idle", "Ready", "Walk", "Run", "Crouch", "Crawl", "Jump", "Fall", "Land", "Block", "Climb", "Die" })
        {
            anim.SetBool(variable, false);
        }

        switch (state)
        {
            case playerState.Idle: anim.SetBool("Idle", true); break;
            case playerState.Ready: anim.SetBool("Ready", true); break;
            case playerState.Walk: anim.SetBool("Walk", true); break;
            case playerState.Run: anim.SetBool("Run", true); break;
            case playerState.Crouch: anim.SetBool("Crouch", true); break;
            case playerState.Crawl: anim.SetBool("Crawl", true); break;
            case playerState.Jump: anim.SetBool("Jump", true); break;
            case playerState.Fall: anim.SetBool("Fall", true); break;
            case playerState.Land: anim.SetBool("Land", true); break;
            case playerState.Block: anim.SetBool("Block", true); break;
            case playerState.Climb: anim.SetBool("Climb", true); break;
            case playerState.Die: anim.SetBool("Die", true); break;
        }
    }
}
