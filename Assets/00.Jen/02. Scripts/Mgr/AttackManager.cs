using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackManager : MonoBehaviour
{
    public GameObject attackPoint;

    [Header("Preview")]
    public LineRenderer previewLineRenderer;
    public Color[] previewColors = new Color[5];
    
    public int previewSinCount = 1000;
    public int previewTanCount = 5000;
    public int previewAbsCount = 1000;
    public float previewDivide = 0.01f;
    
    public float previewPointDistance = 0.1f;
    public float previewLength = 10f;


    [Header("Sin Attack")]
    public GameObject sinAttackPrefab;
    [Range(-10f, 10f)]
    [SerializeField]
    public float sinAttackAmplitude = 1.0f;
    [Range(0f, 3f)]
    [SerializeField]
    public float sinAttackFrequency = 1.0f;
    [Range(5f, 10f)]
    [SerializeField]
    public float sinAttackSpeed = 5.0f;

    [Header("Cos Attack")]
    public GameObject cosAttackPrefab;
    [Range(-10f, 10f)]
    [SerializeField]
    public float cosAttackAmplitude = 1.0f;
    [Range(0f, 3f)]
    [SerializeField]
    public float cosAttackFrequency = 1.0f;
    [Range(5f, 10f)]
    [SerializeField]
    public float cosAttackSpeed = 5.0f;

    [Header("Tan Attack")]
    public GameObject tanAttackPrefab;
    [Range(0f, 3f)]
    [SerializeField]
    public float tanAttackAmplitude = 1.0f;
    [Range(1f, 3f)]
    [SerializeField]
    public float tanAttackFrequency = 1.0f;
    [Range(2f, 5f)]
    [SerializeField]
    public float tanAttackSpeed = 5.0f;

    [Header("Abs Attack")]
    public GameObject absAttackPrefab;
    [Range(0f, 5f)]
    [SerializeField]
    public float absAttackAmplitude = 1.0f;
    [Range(5f, 10f)]
    [SerializeField]
    public float absAttackSpeed = 5.0f;


    private enum AttackType { None, Sin, Cos, Tan, Abs }
    private AttackType selectedAttackType = AttackType.None;
    private Vector3 previewDirection;

    public GameObject sin_Panel;
    public GameObject cos_Panel;
    public GameObject tan_Panel;
    public GameObject abs_Panel;

    private void Start()
    {
        SetPanelsActive(AttackType.None);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAttackType = AttackType.Sin;
            SetPanelsActive(selectedAttackType);
            UpdatePreviewLineColor(selectedAttackType);
            Debug.Log("Sin attack selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedAttackType = AttackType.Cos;
            SetPanelsActive(selectedAttackType);
            UpdatePreviewLineColor(selectedAttackType);
            Debug.Log("Cos attack selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedAttackType = AttackType.Tan;
            SetPanelsActive(selectedAttackType);
            UpdatePreviewLineColor(selectedAttackType);
            Debug.Log("Tan attack selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedAttackType = AttackType.Abs;
            SetPanelsActive(selectedAttackType);
            UpdatePreviewLineColor(selectedAttackType);
            Debug.Log("Abs attack selected");
        }

        if (selectedAttackType != AttackType.None)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            previewDirection = (mousePosition - attackPoint.transform.position).normalized;
            ShowPreviewPath();

            if (Input.GetMouseButtonDown(0) && MagicCount.magicCountBool)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                MagicCount.currentMagicCount--;

                if (selectedAttackType == AttackType.Sin)
                {
                    SpawnAttack(sinAttackPrefab, sinAttackAmplitude, sinAttackFrequency, sinAttackSpeed, previewDirection);
                }
                else if (selectedAttackType == AttackType.Cos)
                {
                    SpawnAttack(cosAttackPrefab, cosAttackAmplitude, cosAttackFrequency, cosAttackSpeed, previewDirection);
                }
                else if (selectedAttackType == AttackType.Tan)
                {
                    SpawnAttack(tanAttackPrefab, tanAttackAmplitude, tanAttackFrequency, tanAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Abs)
                {
                    SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, false);
                    SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, true);
                }
            }
        }
    }

    void SetPanelsActive(AttackType type)
    {
        sin_Panel.SetActive(type == AttackType.Sin);
        cos_Panel.SetActive(type == AttackType.Cos);
        tan_Panel.SetActive(type == AttackType.Tan);
        abs_Panel.SetActive(type == AttackType.Abs);
    }

    void UpdatePreviewLineColor(AttackType type)
    {
        int idx = (int)type;

        previewLineRenderer.startColor = previewColors[idx];
        previewLineRenderer.endColor = previewColors[idx];
    }

    Vector2 CalculateCoordinates(float m, float L)
    {
        // 각도 계산
        float theta = Mathf.Atan(m);

        // x와 y 계산
        float x = L * Mathf.Cos(theta);
        float y = L * Mathf.Sin(theta);

        return new Vector2(x, y);
    }

    void ShowPreviewPath()
    {
        int pointCount = (selectedAttackType == AttackType.Abs) ? previewAbsCount : (selectedAttackType == AttackType.Tan) ? previewTanCount : previewSinCount; // Abs일 경우 200, Tan일 경우 500, 나머지는 100
        float t = 0f;

        Vector3 startPosition = attackPoint.transform.position;
        List<Vector3> points = new();

        if (selectedAttackType == AttackType.Abs)
        {
            
            Vector2 pos = CalculateCoordinates(absAttackAmplitude, previewLength);

            Vector2 point1 = startPosition + new Vector3(pos.x, pos.y);
            Vector2 point2 = startPosition;
            Vector2 point3 = startPosition + new Vector3(-pos.x, pos.y);

            Vector2 currentPoint = point1;
            points.Add(currentPoint);

            while (currentPoint != point2)
            {
                currentPoint = Vector2.MoveTowards(currentPoint, point2, previewPointDistance);
                points.Add(currentPoint);
            }

            points.Add(currentPoint);

            while (currentPoint != point3)
            {
                currentPoint = Vector2.MoveTowards(currentPoint, point3, previewPointDistance);
                points.Add(currentPoint);
            }


            /*
            
            float y = absAttackAmplitude * previewLength;
            float x = absAttackAmplitude / y;

            int half = previewAbsCount / 2;

            Vector2 point1 = startPosition + new Vector3(x, y);
            Vector2 point2 = startPosition;
            Vector2 point3 = startPosition + new Vector3(-x, y);

            Vector2 currentPoint = point1;
            Vector2 step = (point2 - point1) / half;

            int idx = 0;
            for(int i = 0; i < half; i++)
            {
                previewLineRenderer.SetPosition(idx++, currentPoint);
                currentPoint += step;
            }

            step = (point3 - point2) / half;
            for (int i = 0; i < half; i++)
            {
                previewLineRenderer.SetPosition(idx++, currentPoint);
                currentPoint += step;
            }
            */

            /*
            previewLineRenderer.SetPosition(0, startPosition + new Vector3(x, y));
            previewLineRenderer.SetPosition(1, startPosition);
            previewLineRenderer.SetPosition(2, startPosition + new Vector3(-x, y));
            */

            previewLineRenderer.positionCount = points.Count;
            previewLineRenderer.SetPositions(points.ToArray());

        }
        else
        {
            previewLineRenderer.positionCount = pointCount;

            for (int i = 0; i < pointCount; i++, t += previewDivide)
            {
                Vector3 offset = previewDirection * t * sinAttackSpeed; // 기본 속도 사용

                float y = 0;

                if (selectedAttackType == AttackType.Sin)
                {
                    y = sinAttackAmplitude * Mathf.Sin(sinAttackFrequency * t * sinAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Cos)
                {
                    y = cosAttackAmplitude * Mathf.Cos(cosAttackFrequency * t * cosAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Tan)
                {
                    offset = new Vector3(t * tanAttackSpeed, 0, 0); // x 방향 고정
                    y = tanAttackAmplitude * Mathf.Tan(tanAttackFrequency * t * tanAttackSpeed);
                    if (Mathf.Abs(y) >= 3)
                    {
                        y = Mathf.Sign(y) * 3; // y 값이 5를 넘지 않도록 제한
                    }
                }

                Vector3 newPosition = startPosition + offset;
                newPosition.y += y;
                previewLineRenderer.SetPosition(i, newPosition);
            }

            
            Vector3[] positions = new Vector3[previewLineRenderer.positionCount];
            previewLineRenderer.GetPositions(positions);

            // 전체 길이 계산
            float totalLength = 0f;
            for (int i = 1; i < positions.Length; i++)
            {
                totalLength += Vector2.Distance(positions[i - 1], positions[i]);
            }

            // 새로운 포지션 리스트
            List<Vector3> newPositions = new();

            // 현재 위치와 다음 위치 설정
            float currentLength = 0f;
            newPositions.Add(positions[0]);

            for (int i = 1; i < positions.Length; i++)
            {
                Vector3 start = positions[i - 1];
                Vector3 end = positions[i];
                float segmentLength = Vector2.Distance(start, end);

                while (currentLength + segmentLength >= previewPointDistance)
                {
                    float remainingLength = previewPointDistance - currentLength;
                    Vector3 newPosition = Vector2.Lerp(start, end, remainingLength / segmentLength);
                    newPositions.Add(newPosition);

                    // 새로운 시작점 설정
                    start = newPosition;
                    segmentLength -= remainingLength;
                    currentLength = 0f;
                }

                currentLength += segmentLength;
            }


            previewLineRenderer.positionCount = newPositions.Count;
            previewLineRenderer.SetPositions(newPositions.ToArray());

            

            /*
            previewLineRenderer.positionCount = pointCount;

            for (int i = 0; i < pointCount; i++, t += previewDivide)
            {
                Vector3 offset = previewDirection * t * sinAttackSpeed; // 기본 속도 사용

                float y = 0;

                if (selectedAttackType == AttackType.Sin)
                {
                    y = sinAttackAmplitude * Mathf.Sin(sinAttackFrequency * t * sinAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Cos)
                {
                    y = cosAttackAmplitude * Mathf.Cos(cosAttackFrequency * t * cosAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Tan)
                {
                    offset = new Vector3(t * tanAttackSpeed, 0, 0); // x 방향 고정
                    y = tanAttackAmplitude * Mathf.Tan(tanAttackFrequency * t * tanAttackSpeed);
                    if (Mathf.Abs(y) >= 3)
                    {
                        y = Mathf.Sign(y) * 3; // y 값이 5를 넘지 않도록 제한
                    }
                }

                Vector3 newPosition = startPosition + offset;
                newPosition.y += y;
                previewLineRenderer.SetPosition(i, newPosition);
            }
            */
        }

        //previewLineRenderer.positionCount = points.Count;
        //previewLineRenderer.SetPositions(points.ToArray());
    }
    /*
    Vector3 CalculateWavePosition(float t, float amplitude, float frequency, System.Func<float, float> waveFunction)
    {
        float omega = 2 * Mathf.PI * frequency;  // 각 주파수 계산
        float x = t;
        float y = amplitude * waveFunction(omega * t);  // 파형 함수 적용

        Vector3 localPosition = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.LookRotation(previewDirection, Vector3.up);
        Vector3 rotatedPosition = rotation * localPosition;

        return rotatedPosition;
    }

    Vector3 CalculateCosineWave(float t)
    {
        float omega = 2 * Mathf.PI * cosAttackFrequency;  // 각 주파수 계산
        float x = cosAttackAmplitude * Mathf.Cos(omega * t);  // 코사인파 값 계산

        // 기본 방향 벡터에서 코사인파 값을 적용한 위치 계산
        Vector3 localPosition = new(x, 0, 0);

        // 회전 적용
        Quaternion rotation = Quaternion.LookRotation(previewDirection, Vector3.up);
        Vector3 rotatedPosition = rotation * localPosition;

        return rotatedPosition;
    }
    */

    
    float CalculateCosineWave(float t)
    {
        float omega = 2 * Mathf.PI * cosAttackFrequency;  // 각 주파수 계산
        return cosAttackAmplitude * Mathf.Cos(omega * t);  // 사인파 값 계산
    }
    

    Vector3 CalculateNextSineWavePosition(Vector3 currentPosition, float currentTime, float amplitude, float frequency)
    {
        float nextTime = currentTime + previewPointDistance;
        float x = nextTime;
        float y = amplitude * Mathf.Sin(frequency * nextTime);

        return new Vector3(x, y, 0);
    }

    Vector3 CalculateNextCosineWavePosition(Vector3 currentPosition, float currentTime, float amplitude, float frequency)
    {
        float nextTime = currentTime + previewPointDistance;
        float x = nextTime;
        float y = amplitude * Mathf.Cos(frequency * nextTime);

        return new Vector3(x, y, 0);
    }

    Vector3 CalculateNextTangentWavePosition(Vector3 currentPosition, float currentTime, float amplitude, float frequency)
    {
        float nextTime = currentTime + previewPointDistance;
        float x = nextTime;
        float y = amplitude * Mathf.Tan(frequency * nextTime);

        if (Mathf.Abs(y) >= 3)
        {
            y = Mathf.Sign(y) * 3;
        }

        return new Vector3(x, y, 0);
    }


    void SpawnAttack(GameObject attackPrefab, float amplitude, float frequency, float speed, Vector3 direction)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            SinWaveAttack sinWaveAttack = attackInstance.GetComponent<SinWaveAttack>();
            CosWaveAttack cosWaveAttack = attackInstance.GetComponent<CosWaveAttack>();

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