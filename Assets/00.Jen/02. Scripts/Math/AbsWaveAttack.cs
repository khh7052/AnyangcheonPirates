using UnityEngine;

public class AbsWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // A: 진폭
    public float speed = 5.0f;      // 공격 이동 속도
    public bool moveLeft = false;   // 왼쪽으로 이동하는지 여부
    private Vector3 startPosition;
    private float startTime;

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // 시간에 따른 x 위치 계산
        float direction = moveLeft ? -1 : 1;
        float x = startPosition.x + direction * speed * (Time.time - startTime);

        // y 위치는 절댓값 함수를 이용해 계산
        float y = amplitude * Mathf.Abs(x - startPosition.x);

        // 새로운 위치 설정
        Vector3 newPosition = new Vector3(x, startPosition.y + y, transform.position.z);
        transform.position = newPosition;

        // 일정 시간 후 오브젝트 삭제
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}