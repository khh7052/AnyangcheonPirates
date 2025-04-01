using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PositionUI : MonoBehaviour
{
    public GameObject player;
    public Vector2 mouseTextOffset;
    public GameObject[] monsters;

    private TextMeshProUGUI playerTextComponent;
    public TextMeshProUGUI cursorTextComponent;

    private List<TextMeshProUGUI> monsterTextComponents = new List<TextMeshProUGUI>();

    void Start()
    {
        // 플레이어의 TextMeshProUGUI 컴포넌트를 찾음
        playerTextComponent = player.GetComponentInChildren<TextMeshProUGUI>();

        monsters = GameObject.FindGameObjectsWithTag("Enemy");

        // 각 몬스터의 자식 오브젝트에서 TextMeshProUGUI 컴포넌트를 찾음
        foreach (var monster in monsters)
        {
            monsterTextComponents.AddRange(monster.GetComponentsInChildren<TextMeshProUGUI>());
        }
    }

    void Update()
    {
        // 플레이어 위치 텍스트 업데이트
        Vector2 playerPos = player.transform.position;
        UpdatePositionText(playerTextComponent, playerPos);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorTextComponent.rectTransform.position = mousePos + mouseTextOffset;
        UpdatePositionText(cursorTextComponent, mousePos);

        // 각 몬스터 위치 텍스트 업데이트
        for (int i = 0; i < monsters.Length; i++)
        {
            Vector2 monsterPos = monsters[i].transform.position;
            UpdatePositionText(monsterTextComponents[i], monsterPos);
        }
    }

    void UpdatePositionText(TextMeshProUGUI textComponent, Vector2 position)
    {
        if (textComponent != null)
        {
            textComponent.text = $"X : {position.x:F2}\nY : {position.y:F2}";
        }
    }
}
