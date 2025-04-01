using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Collider : MonoBehaviour
{
    public string[] Attack_names;  // 태그 이름을 저장할 배열
    public GameObject particle;   // 실행할 프리팹

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

            // 프리팹을 실행
            GameObject particleInstance = Instantiate(particle, transform.position, transform.rotation);

            // 3초 후 프리팹을 삭제
            Destroy(particleInstance, 3f);

            // 원래 오브젝트 삭제
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 프리팹을 실행
            GameObject particleInstance = Instantiate(particle, transform.position, transform.rotation);

            // 3초 후 프리팹을 삭제
            Destroy(particleInstance, 3f);
        }
    }
}
