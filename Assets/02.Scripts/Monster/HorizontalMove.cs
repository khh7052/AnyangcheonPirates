using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HorizontalMove : BaseMove
{
    public Transform checkRayOrigin; // ����ĳ��Ʈ ���� ��ġ
    public LayerMask groundLayer;  // �� ���̾ ����
    public LayerMask obstacleLayer;  // ��ֹ� ���̾ ����
    public float groundCheckDistance = 1.0f;  // �� üũ �Ÿ�
    public float obstacleCheckDistance = 0.5f;  // ��ֹ� üũ �Ÿ�
    public float obstacleCheckHeight = 1.0f;  // ��ֹ� üũ ����
    public int obstacleRayCount = 4;  // ��ֹ� üũ ���� ����
    public ParticleSystem moveParticle;
    public AssetAnimationController animController;

    private void Awake()
    {
        animController = GetComponent<AssetAnimationController>();
    }

    private void Update()
    {
        Vector2 prevPos = transform.position;

        if (IsGroundAhead() && !IsObstacleAhead())
        {
            Move();
        }
        else
        {
            // �տ� ���� ���ų� ��ֹ��� ������ Idle ���·� ��ȯ
            SetOnArrive(true);
        }

        if((Vector2)transform.position == prevPos)
        {
            moveParticle.Stop();
            if (animController.animState == MonsterState.Trace)
            {
                animController.Idle(true);
            }
        }
        else
        {
            moveParticle.Play();
            if (animController.animState != MonsterState.Trace)
            {
                animController.Walk(true);
            }
        }
    }

    public override void Move()
    {
        if (!onMove) return;

        Vector2 targetPos = target.position;
        targetPos.y = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetPos)
        {
            SetOnArrive(true);
        }
        else
        {
            SetOnArrive(false);
        }
    }

    void SetOnArrive(bool arrive)
    {
        if(onArrive == arrive) return;
        onArrive = arrive;

        if (onArrive)
        {
            onArrive = true;
            print("Arrive");
            OnArrive.Invoke();
        }
        else
        {
            onArrive = false;
            print("Start Move");
            OnMove.Invoke();
        }

    }

    private bool IsGroundAhead()
    {
        Vector2 position = checkRayOrigin.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        // �� üũ ����ĳ��Ʈ
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        return hit.collider != null;
    }

    private bool IsObstacleAhead()
    {
        Vector2 position = checkRayOrigin.position;
        Vector2 direction = Vector2.right * (transform.localScale.x > 0 ? 1 : -1); // ���Ͱ� ���ϰ� �ִ� ����
        float distance = obstacleCheckDistance;
        float interval = obstacleCheckHeight / obstacleRayCount;

        for (int i = 0; i < obstacleRayCount; i++)
        {
            position.y = checkRayOrigin.position.y + interval * i;

            if (Physics2D.Raycast(position, direction, distance, obstacleLayer))
                return true;
        }

        // ��ֹ� üũ ����ĳ��Ʈ
        return false;
    }

    private void OnDrawGizmos()
    {
        if (checkRayOrigin == null) return;

        Vector2 position = checkRayOrigin.position;

        // ������� ���� ����� �׸���
        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, position + Vector2.down * groundCheckDistance);

        Gizmos.color = Color.red;
        float interval = obstacleCheckHeight / obstacleRayCount;

        for (int i = 0; i < obstacleRayCount; i++)
        {
            position.y = checkRayOrigin.position.y + interval * i;

            Gizmos.DrawLine(position, position + Vector2.right * (transform.localScale.x > 0 ? obstacleCheckDistance : -obstacleCheckDistance));
        }

    }
}
