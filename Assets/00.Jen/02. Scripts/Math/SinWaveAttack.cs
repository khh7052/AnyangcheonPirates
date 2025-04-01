using UnityEngine;

public class SinWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // A: ����
    public float frequency = 0.1f;  // B: ��
    public float speed = 5.0f;      // ���� �̵� �ӵ�
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction;       // ���� ����

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // �ð��� ���� x ��ġ ���
        float distance = speed * (Time.time - startTime);
        Vector3 offset = direction * distance;

        // y ��ġ�� sin �Լ��� �̿��� ���
        float y = amplitude * Mathf.Sin(frequency * distance);

        // ���ο� ��ġ ����
        Vector3 newPosition = startPosition + offset;
        newPosition.y += y;
        transform.position = newPosition;

        // ���� �ð� �� ������Ʈ ����
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}