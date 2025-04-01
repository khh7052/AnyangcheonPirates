using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{
    public float explosionForce = 1f; // 터지는 힘의 크기
    public float explosionRadius = 1f; // 폭발 반경

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D가 없으면 추가하기
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        Explode();
    }

    void Explode()
    {
        Vector2 explosionPosition = transform.position;

        // 주변의 모든 Rigidbody2D에 폭발력 전달
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
