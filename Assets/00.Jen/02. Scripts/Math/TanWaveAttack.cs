using UnityEngine;

public class TanWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // ����
    public float frequency = 1.0f;  // ��
    public float speed = 5.0f;      // ���� �̵� �ӵ�
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction = Vector3.right;  // �̵� ����, �⺻���� ������

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        direction.Normalize();  // ���� ���͸� ����ȭ
    }

    void Update()
    {
        // �ð��� ���� �̵� �Ÿ� ���
        float distance = speed * (Time.time - startTime);

        // �̵��� x�� y ���
        float x = distance;
        float y = amplitude * Mathf.Tan(frequency * distance);

        // Ư������ ����� ���ο� ��ġ ����
        if (Mathf.Abs(y) < 3)  // y ���� �ʹ� Ŀ���� �ʵ��� ����
        {
            Vector3 offset = direction * x + new Vector3(0, y, 0);
            Vector3 newPosition = startPosition + offset;
            transform.position = newPosition;
        }

        // ���� �ð� �� ������Ʈ ����
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}
