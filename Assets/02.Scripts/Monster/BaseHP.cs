using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseHP : MonoBehaviour
{
    public UnityEvent<int> OnDamage = new();
    public UnityEvent OnDie = new();

    public int maxHP = 100;
    public int currentHP;

    public abstract void TakeDamage(int damage);
    public abstract void Die();
}
