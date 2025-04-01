using UnityEngine;

public class CosWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // 진폭
    public float frequency = 1.0f;  // 빈도
    public float speed = 5.0f;      // 공격 이동 속도
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction;       // 이동 방향

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;

        // direction 벡터는 외부에서 설정됩니다.
    }

    void Update()
    {
        // 시간에 따른 이동 거리 계산
        float distance = speed * (Time.time - startTime);
        Vector3 offset = direction * distance;

        // y 위치는 cos 함수를 이용해 계산
        float y = amplitude * Mathf.Cos(frequency * distance);

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
