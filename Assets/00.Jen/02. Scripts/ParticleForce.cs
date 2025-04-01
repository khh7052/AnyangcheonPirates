using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{
    public float explosionForce = 1f; // ������ ���� ũ��
    public float explosionRadius = 1f; // ���� �ݰ�

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D�� ������ �߰��ϱ�
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        Explode();
    }

    void Explode()
    {
        Vector2 explosionPosition = transform.position;

        // �ֺ��� ��� Rigidbody2D�� ���߷� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D nearbyRb = nearbyObject.GetComponent<Rigidbody2D>();
            if (nearbyRb != null)
            {
                Vector2 direction = nearbyRb.position - explosionPosition;
                float distance = direction.magnitude;
                float force = Mathf.Lerp(explosionForce, 0, distance / explosionRadius);
                nearbyRb.AddForce(direction.normalized * force);
            }
        }
    }
}
