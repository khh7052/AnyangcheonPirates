using UnityEngine;

public class TanWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // 진폭
    public float frequency = 1.0f;  // 빈도
    public float speed = 5.0f;      // 공격 이동 속도
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction = Vector3.right;  // 이동 방향, 기본값은 오른쪽

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        direction.Normalize();  // 방향 벡터를 정규화
    }

    void Update()
    {
        // 시간에 따른 이동 거리 계산
        float distance = speed * (Time.time - startTime);

        // 이동할 x와 y 계산
        float x = distance;
        float y = amplitude * Mathf.Tan(frequency * distance);

        // 특이점을 고려한 새로운 위치 설정
        if (Mathf.Abs(y) < 3)  // y 값이 너무 커지지 않도록 제한
        {
            Vector3 offset = direction * x + new Vector3(0, y, 0);
            Vector3 newPosition = startPosition + offset;
            transform.position = newPosition;
        }

        // 일정 시간 후 오브젝트 삭제
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}
