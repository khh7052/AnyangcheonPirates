using UnityEngine;

public class AbsWaveAttack : MonoBehaviour
{
    public float amplitude = 1.0f;  // A: ����
    public float speed = 5.0f;      // ���� �̵� �ӵ�
    public bool moveLeft = false;   // �������� �̵��ϴ��� ����
    private Vector3 startPosition;
    private float startTime;

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // �ð��� ���� x ��ġ ���
        float direction = moveLeft ? -1 : 1;
        float x = startPosition.x + direction * speed * (Time.time - startTime);

        // y ��ġ�� ���� �Լ��� �̿��� ���
        float y = amplitude * Mathf.Abs(x - startPosition.x);

        // ���ο� ��ġ ����
        Vector3 newPosition = new Vector3(x, startPosition.y + y, transform.position.z);
        transform.position = newPosition;

        // ���� �ð� �� ������Ʈ ����
        if (Time.time - startTime > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}