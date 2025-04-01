using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceLine : MonoBehaviour
{
    public Transform player; 
    Transform object2;

    public LineRenderer lineRenderer; // �� ������Ʈ�� �����ϴ� ����
    public LineRenderer xLineRenderer; // x�� �Ÿ� ����
    public LineRenderer yLineRenderer; // y�� �Ÿ� ����

    void Update()
    {
        // ���콺 Ŭ�� ���� �� object2 �Ҵ�
        if (Input.GetMouseButtonDown(1)) // ���� ���콺 ��ư Ŭ�� ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                object2 = hit.transform;
            }
        }

        // �� ������Ʈ�� �Ҵ�� ��쿡�� �Ÿ� ��� �� ���� ������Ʈ
        if (player != null && object2 != null)
        {
            // �� ������Ʈ�� ��ġ�� ������
            Vector2 position1 = player.position;
            Vector2 position2 = object2.position;

            // �� ������Ʈ ������ �Ÿ� ���
            float distance = Vector2.Distance(position1, position2);
            float xDistance = Mathf.Abs(position1.x - position2.x);
            float yDistance = Mathf.Abs(position1.y - position2.y);

            // ��ü �Ÿ� ���� ������Ʈ
            lineRenderer.SetPosition(0, position1);
            lineRenderer.SetPosition(1, position2);

            // x�� �Ÿ� ���� ������Ʈ
            xLineRenderer.SetPosition(0, position1);
            xLineRenderer.SetPosition(1, new Vector2(position2.x, position1.y));

            // y�� �Ÿ� ���� ������Ʈ
            yLineRenderer.SetPosition(0, new Vector2(position2.x, position1.y));
            yLineRenderer.SetPosition(1, position2);
        }
    }
}
