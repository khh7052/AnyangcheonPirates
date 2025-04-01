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
        // �÷��̾��� TextMeshProUGUI ������Ʈ�� ã��
        playerTextComponent = player.GetComponentInChildren<TextMeshProUGUI>();

        monsters = GameObject.FindGameObjectsWithTag("Enemy");

        // �� ������ �ڽ� ������Ʈ���� TextMeshProUGUI ������Ʈ�� ã��
        foreach (var monster in monsters)
        {
            monsterTextComponents.AddRange(monster.GetComponentsInChildren<TextMeshProUGUI>());
        }
    }

    void Update()
    {
        // �÷��̾� ��ġ �ؽ�Ʈ ������Ʈ
        Vector2 playerPos = player.transform.position;
        UpdatePositionText(playerTextComponent, playerPos);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorTextComponent.rectTransform.position = mousePos + mouseTextOffset;
        UpdatePositionText(cursorTextComponent, mousePos);

        // �� ���� ��ġ �ؽ�Ʈ ������Ʈ
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
