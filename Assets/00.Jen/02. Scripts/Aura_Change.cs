using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Change : MonoBehaviour
{
    public GameObject aura1;  // 1번 오브젝트
    public GameObject aura2;  // 2번 오브젝트
    public GameObject aura3;  // 3번 오브젝트
    public GameObject aura4;  // 4번 오브젝트

    void Start()
    {
        // 초기화 시 모든 오브젝트 비활성화
        DeactivateAllAuras();
    }

    void Update()
    {
        // 1번 키 입력 시 1번 오브젝트 활성화
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAura(1);
        }
        // 2번 키 입력 시 2번 오브젝트 활성화
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateAura(2);
        }
        // 3번 키 입력 시 3번 오브젝트 활성화
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateAura(3);
        }
        // 4번 키 입력 시 4번 오브젝트 활성화
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateAura(4);
        }
    }

    // 모든 오브젝트 비활성화
    void DeactivateAllAuras()
    {
        aura1.SetActive(false);
        aura2.SetActive(false);
        aura3.SetActive(false);
        aura4.SetActive(false);
    }

    // 특정 오브젝트 활성화
    void ActivateAura(int auraNumber)
    {
        DeactivateAllAuras();

        switch (auraNumber)
        {
            case 1:
                aura1.SetActive(true);
                break;
            case 2:
                aura2.SetActive(true);
                break;
            case 3:
                aura3.SetActive(true);
                break;
            case 4:
                aura4.SetActive(true);
                break;
        }
    }
}
