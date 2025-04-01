using UnityEngine;

public class CosWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // ����
    public float frequency = 1.0f;  // ��
    public float speed = 5.0f;      // ���� �̵� �ӵ�
    private Vector3 startPosition;
    private float startTime;
    public Vector3 direction;       // �̵� ����

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;

        // direction ���ʹ� �ܺο��� �����˴ϴ�.
    }

    void Update()
    {
        // �ð��� ���� �̵� �Ÿ� ���
        float distance = speed * (Time.time - startTime);
        Vector3 offset = direction * distance;

        // y ��ġ�� cos �Լ��� �̿��� ���
        float y = amplitude * Mathf.Cos(frequency * distance);

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
