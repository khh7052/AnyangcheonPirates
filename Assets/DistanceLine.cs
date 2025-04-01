using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceLine : MonoBehaviour
{
    public Transform player; 
    Transform object2;

    public LineRenderer lineRenderer; // 두 오브젝트를 연결하는 라인
    public LineRenderer xLineRenderer; // x축 거리 라인
    public LineRenderer yLineRenderer; // y축 거리 라인

    void Update()
    {
        // 마우스 클릭 감지 및 object2 할당
        if (Input.GetMouseButtonDown(1)) // 왼쪽 마우스 버튼 클릭 감지
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                object2 = hit.transform;
            }
        }

        // 두 오브젝트가 할당된 경우에만 거리 계산 및 라인 업데이트
        if (player != null && object2 != null)
        {
            // 두 오브젝트의 위치를 가져옴
            Vector2 position1 = player.position;
            Vector2 position2 = object2.position;

            // 두 오브젝트 사이의 거리 계산
            float distance = Vector2.Distance(position1, position2);
            float xDistance = Mathf.Abs(position1.x - position2.x);
            float yDistance = Mathf.Abs(position1.y - position2.y);

            // 전체 거리 라인 업데이트
            lineRenderer.SetPosition(0, position1);
            lineRenderer.SetPosition(1, position2);

            // x축 거리 라인 업데이트
            xLineRenderer.SetPosition(0, position1);
            xLineRenderer.SetPosition(1, new Vector2(position2.x, position1.y));

            // y축 거리 라인 업데이트
            yLineRenderer.SetPosition(0, new Vector2(position2.x, position1.y));
            yLineRenderer.SetPosition(1, position2);
        }
    }
}
