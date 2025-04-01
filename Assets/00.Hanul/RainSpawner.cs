using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject[] rainPrefabs;  // ������ ������Ʈ�� ������ �迭
    public float spawnRate = 1.0f;  // ������Ʈ ���� ����
    public float spawnAreaWidth = 10.0f;  // ���� ������ �ʺ�
    public float spawnHeight = 10.0f;  // ���� ����

    private float nextSpawnTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRain();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnRain()
    {
        float spawnX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0);

        // �������� �ϳ��� ������ ����
        int randomIndex = Random.Range(0, rainPrefabs.Length);
        GameObject selectedPrefab = rainPrefabs[randomIndex];

        // ������Ʈ ����
        GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);

        // x�� �������� -1 �Ǵ� 1�� ���� ����
        float randomScaleX = Random.value < 0.5f ? -1f : 1f;
        spawnedObject.transform.localScale = new Vector3(randomScaleX, spawnedObject.transform.localScale.y, spawnedObject.transform.localScale.z);
    }

    void OnDisable()
    {
        // �ڽ� ������Ʈ���� ��� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
