using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Change : MonoBehaviour
{
    public GameObject aura1;  // 1�� ������Ʈ
    public GameObject aura2;  // 2�� ������Ʈ
    public GameObject aura3;  // 3�� ������Ʈ
    public GameObject aura4;  // 4�� ������Ʈ

    void Start()
    {
        // �ʱ�ȭ �� ��� ������Ʈ ��Ȱ��ȭ
        DeactivateAllAuras();
    }

    void Update()
    {
        // 1�� Ű �Է� �� 1�� ������Ʈ Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAura(1);
        }
        // 2�� Ű �Է� �� 2�� ������Ʈ Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateAura(2);
        }
        // 3�� Ű �Է� �� 3�� ������Ʈ Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateAura(3);
        }
        // 4�� Ű �Է� �� 4�� ������Ʈ Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateAura(4);
        }
    }

    // ��� ������Ʈ ��Ȱ��ȭ
    void DeactivateAllAuras()
    {
        aura1.SetActive(false);
        aura2.SetActive(false);
        aura3.SetActive(false);
        aura4.SetActive(false);
    }

    // Ư�� ������Ʈ Ȱ��ȭ
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
