using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Collider : MonoBehaviour
{
    public string[] Attack_names;  // �±� �̸��� ������ �迭
    public GameObject particle;   // ������ ������

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (gameObject.CompareTag("Magic"))
            {
                if (GameManager.Instance)
                    GameManager.Instance.MagicCountDown(1);
            }
            else if (gameObject.CompareTag("Abs_Magic"))
            {
                if (GameManager.Instance)
                    GameManager.Instance.MagicCountDown(0.5f);
            }

            // �������� ����
            GameObject particleInstance = Instantiate(particle, transform.position, transform.rotation);

            // 3�� �� �������� ����
            Destroy(particleInstance, 3f);

            // ���� ������Ʈ ����
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            // �������� ����
            GameObject particleInstance = Instantiate(particle, transform.position, transform.rotation);

            // 3�� �� �������� ����
            Destroy(particleInstance, 3f);
        }
    }
}
