using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject[] rainPrefabs;  // 떨어질 오브젝트의 프리팹 배열
    public float spawnRate = 1.0f;  // 오브젝트 생성 간격
    public float spawnAreaWidth = 10.0f;  // 생성 영역의 너비
    public float spawnHeight = 10.0f;  // 생성 높이

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

        // 랜덤으로 하나의 프리팹 선택
        int randomIndex = Random.Range(0, rainPrefabs.Length);
        GameObject selectedPrefab = rainPrefabs[randomIndex];

        // 오브젝트 생성
        GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);

        // x축 스케일을 -1 또는 1로 랜덤 설정
        float randomScaleX = Random.value < 0.5f ? -1f : 1f;
        spawnedObject.transform.localScale = new Vector3(randomScaleX, spawnedObject.transform.localScale.y, spawnedObject.transform.localScale.z);
    }

    void OnDisable()
    {
        // 자식 오브젝트들을 모두 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
