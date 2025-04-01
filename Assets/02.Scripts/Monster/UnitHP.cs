using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHP : BaseHP
{
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        currentHP = maxHP;
    }

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;
        
        OnDamage.Invoke(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("Die");
        OnDie.Invoke();
    }
}
