using UnityEngine;

public class SinWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // A: 진폭
    public float frequency = 0.1f;  // B: 빈도
    public float speed = 5.0f;      // 공격 이동 속도
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction;       // 방향 벡터

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // 시간에 따른 x 위치 계산
        float distance = speed * (Time.time - startTime);
        Vector3 offset = direction * distance;

        // y 위치는 sin 함수를 이용해 계산
        float y = amplitude * Mathf.Sin(frequency * distance);

        // 새로운 위치 설정
        Vector3 newPosition = startPosition + offset;
        newPosition.y += y;
        transform.position = newPosition;

        // 일정 시간 후 오브젝트 삭제
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}