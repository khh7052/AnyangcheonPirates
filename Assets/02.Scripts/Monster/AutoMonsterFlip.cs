using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMonsterFlip : MonoBehaviour
{
    public bool onFlip = true;
    private Transform target;

    private void OnEnable()
    {
        onFlip = true;
        if(target == null)
        {

            // target = Player.Instance.transform;
        }
    }

    void LateUpdate()
    {
        Flip();
    }

    public void SetOnFlip(bool flip)
    {
        onFlip = flip;
    }

    void Flip()
    {
        if (!onFlip) return;
        if (target.position == transform.position) return;

        Vector2 scale = transform.localScale;
        scale.x = target.position.x < transform.position.x ? -1 : 1;
        transform.localScale = scale;
    }
}
