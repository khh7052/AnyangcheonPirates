using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class TiTle_Scene : MonoBehaviour
{
    public GameObject attackPoint;

    public GameObject sinAttackPrefab;
    public GameObject cosAttackPrefab;
    public GameObject tanAttackPrefab;
    public GameObject absAttackPrefab;

    [Header("sinAttackAmplitude")]
    [Range(0f, 5f)]
    public float sinAttackAmplitude = 1.0f;
    [Header("sinAttackFrequency")]
    [Range(0f, 3f)]
    public float sinAttackFrequency = 1.0f;
    [Header("sinAttackSpeed")]
    [Range(5f, 10f)]
    public float sinAttackSpeed = 5.0f;

    [Header("cosAttackAmplitude")]
    [Range(0f, 5f)]
    public float cosAttackAmplitude = 1.0f;
    [Header("cosAttackFrequency")]
    [Range(0f, 3f)]
    public float cosAttackFrequency = 1.0f;
    [Header("cosAttackSpeed")]
    [Range(5f, 10f)]
    public float cosAttackSpeed = 5.0f;

    [Header("tanAttackAmplitude")]
    [Range(1f, 3f)]
    public float tanAttackAmplitude = 1.0f;
    [Header("tanAttackFrequency")]
    [Range(1f, 3f)]
    public float tanAttackFrequency = 1.0f;
    [Header("tanAttackSpeed")]
    [Range(2f, 5f)]
    public float tanAttackSpeed = 5.0f;

    [Header("absAttackAmplitude")]
    [Range(0f, 5f)]
    public float absAttackAmplitude = 1.0f;
    [Header("absAttackSpeed")]
    [Range(5f, 10f)]
    public float absAttackSpeed = 5.0f;

    private enum AttackType { None, Sin, Cos, Tan, Abs }
    private AttackType currentAttackType = AttackType.Sin;
    private Vector3 previewDirection;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            switch (currentAttackType)
            {
                case AttackType.Sin:
                    SpawnAttack(sinAttackPrefab, sinAttackAmplitude, sinAttackFrequency, sinAttackSpeed, previewDirection);
                    currentAttackType = AttackType.Cos;
                    break;
                case AttackType.Cos:
                    SpawnAttack(cosAttackPrefab, cosAttackAmplitude, cosAttackFrequency, cosAttackSpeed, previewDirection);
                    currentAttackType = AttackType.Tan;
                    break;
                case AttackType.Tan:
                    SpawnAttack(tanAttackPrefab, tanAttackAmplitude, tanAttackFrequency, tanAttackSpeed);
                    currentAttackType = AttackType.Abs;
                    break;
                case AttackType.Abs:
                    SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, false);
                    SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, true);
                    currentAttackType = AttackType.Sin;
                    break;
            }
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        previewDirection = (mousePosition - attackPoint.transform.position).normalized;
    }


    void SpawnAttack(GameObject attackPrefab, float amplitude, float frequency, float speed, Vector3 direction)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            SinWaveAttack sinWaveAttack = attackInstance.GetComponent<SinWaveAttack>();
            CosWaveAttack cosWaveAttack = attackInstance.GetComponent<CosWaveAttack>();
            TanWaveAttack tanWaveAttack = attackInstance.GetComponent<TanWaveAttack>();

            if (sinWaveAttack != null)
            {
                sinWaveAttack.amplitude = amplitude;
                sinWaveAttack.frequency = frequency;
                sinWaveAttack.speed = speed;
                sinWaveAttack.direction = direction;
            }
            else if (cosWaveAttack != null)
            {
                cosWaveAttack.amplitude = amplitude;
                cosWaveAttack.frequency = frequency;
                cosWaveAttack.speed = speed;
                cosWaveAttack.direction = direction;
            }
        }
    }

    void SpawnAttack(GameObject attackPrefab, float amplitude, float frequency, float speed)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            TanWaveAttack tanWaveAttack = attackInstance.GetComponent<TanWaveAttack>();

            if (tanWaveAttack != null)
            {
                tanWaveAttack.amplitude = amplitude;
                tanWaveAttack.frequency = frequency;
                tanWaveAttack.speed = speed;
            }
        }
    }

    void SpawnAttackAbs(GameObject attackPrefab, float amplitude, float speed, bool moveLeft)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            AbsWaveAttack absWaveAttack = attackInstance.GetComponent<AbsWaveAttack>();

            if (absWaveAttack != null)
            {
                absWaveAttack.amplitude = amplitude;
                absWaveAttack.speed = speed;
                absWaveAttack.moveLeft = moveLeft;
            }
        }
    }
}
