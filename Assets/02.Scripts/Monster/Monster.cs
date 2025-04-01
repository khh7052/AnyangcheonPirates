using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterState
{
    Idle,
    Trace,
    Attack,
    Die
}

public class Monster : MonoBehaviour
{
    public UnityEvent<bool> OnFindPlayer = new();

    public string monsterName = "Monster";
    public MonsterState monsterState = MonsterState.Idle;
    public float actionRate = 0.2f;
    public float traceDistance = 10.0f;
    public float attackDistance = 2.0f;
    public float backCheckDistance = 1.0f;

    public BaseMove baseMove;
    public BaseAttack baseAttack;

    private AssetAnimationController animController;
    private Rigidbody2D rigd;
    private Collider2D coll;

    public Transform checkRayOrigin; // 레이캐스트 시작 위치
    public LayerMask playerLayer; // 플레이어 레이어를 지정
    public int detectionRayCount = 4; // 감지 레이 개수
    public float detectionRayHeight = 2.0f; // 감지 레이 높이

    void OnEnable()
    {

        if(animController == null)
            animController = GetComponent<AssetAnimationController>();

        if(rigd == null)
            rigd = GetComponent<Rigidbody2D>();
        if (coll == null)
            coll = GetComponent<Collider2D>();

        if (baseMove == null)
            baseMove = GetComponent<BaseMove>();
        if (baseAttack == null)
            baseAttack = GetComponent<BaseAttack>();

        rigd.isKinematic = false;
        coll.enabled = true;

        StateAction(MonsterState.Idle);
        StartCoroutine(StateUpdate());
    }

    public void StateAction(string state)
    {
        StateAction((MonsterState)System.Enum.Parse(typeof(MonsterState), state));
    }

    public void StateAction(MonsterState state)
    {
        if (state == monsterState) return;

        print("SetState : " + state);
        monsterState = state;

        if (monsterState == MonsterState.Die)
        {
            StopTrace();
            rigd.isKinematic = true;
            coll.enabled = false;
            animController.Die();
            return;
        }

        OnFindPlayer.Invoke(state == MonsterState.Trace || state == MonsterState.Attack);
        switch (monsterState)
        {
            case MonsterState.Idle:

                if (baseMove.onMove)
                {
                    StopTrace();
                    animController.Walk(false);
                }
                StopAttack();
                animController.Idle(true);
                break;
            case MonsterState.Trace:

                StartTrace();
                StopAttack();
                animController.Idle(false);
                animController.Walk(true);
                break;
            case MonsterState.Attack:
                if (baseMove.onMove)
                {
                    StopTrace();
                    animController.Walk(false);
                }
                StartAttack();
                break;
        }
    }

    void StartTrace()
    {
        baseMove.onMove = true;
        // baseMove.target = Player.Instance.transform;
    }


    void StopTrace()
    {
        baseMove.onMove = false;
    }

    void StartAttack()
    {
        baseAttack.onAttack = true;
        // baseAttack.target = Player.Instance.transform;
    }

    void StopAttack()
    {
        baseAttack.onAttack = false;
    }

    IEnumerator StateUpdate()
    {
        while (monsterState != MonsterState.Die)
        {
            if (IsPlayerAhead(out float distanceToPlayer))
            {
                if (distanceToPlayer <= attackDistance)
                {
                    StateAction(MonsterState.Attack);
                }
                else if (distanceToPlayer <= traceDistance)
                {
                    StateAction(MonsterState.Trace);
                }
                else
                {
                    StateAction(MonsterState.Idle);
                }
            }
            else
            {
                StateAction(MonsterState.Idle);
            }

            yield return new WaitForSeconds(actionRate);
        }
    }

    private bool IsPlayerAhead(out float distanceToPlayer)
    {
        distanceToPlayer = float.MaxValue;
        Vector2 direction = Vector2.right * (transform.localScale.x > 0 ? 1 : -1); // 몬스터가 향하고 있는 방향
        float interval = detectionRayHeight / detectionRayCount;
        Vector2 originPosition = checkRayOrigin.position;
        originPosition.x -= backCheckDistance * transform.localScale.x;
        float rayDistance = traceDistance + backCheckDistance;

        for (int i = 0; i < detectionRayCount; i++)
        {
            Vector2 position = originPosition;
            position.y += interval * i;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, rayDistance, playerLayer);
            if (hit.collider != null)
            {
                distanceToPlayer = Mathf.Abs(transform.position.x - hit.point.x);
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (checkRayOrigin == null) return;

        Vector2 position = checkRayOrigin.position;
        float interval = detectionRayHeight / detectionRayCount;

        // 디버깅을 위한 기즈모스 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, traceDistance);

        Gizmos.color = Color.yellow;

        position.x -= backCheckDistance * transform.localScale.x;
        float rayDistance = traceDistance + backCheckDistance;

        for (int i = 0; i < detectionRayCount; i++)
        {
            position.y = checkRayOrigin.position.y + interval * i;
            Gizmos.DrawLine(position, position + Vector2.right * (transform.localScale.x > 0 ? rayDistance : -rayDistance));
        }
    }
}
