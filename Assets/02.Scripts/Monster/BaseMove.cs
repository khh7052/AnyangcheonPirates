using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMove : MonoBehaviour
{
    public UnityEvent OnMove = new();
    public UnityEvent OnArrive = new();

    public Transform target;
    public float moveSpeed = 1.0f;
    public bool onMove = false;
    protected bool onArrive = false;


    public abstract void Move();
}
