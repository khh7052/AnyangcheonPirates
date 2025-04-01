using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : BaseAttack
{

    private void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        if (target == null || !onAttack) return;
        if(Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackRate;
        target.GetComponent<UnitHP>().TakeDamage(attackDamage);
        OnAttack.Invoke();
    }
}
