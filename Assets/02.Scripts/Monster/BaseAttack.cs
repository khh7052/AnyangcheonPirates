using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseAttack : MonoBehaviour
{
    public UnityEvent OnAttack = new();

    public Transform target;
    public bool onAttack = false;
    public float attackRate = 2f;
    public int attackDamage = 10;
    protected float nextAttackTime = 0.0f;

    public abstract void Attack();
}
