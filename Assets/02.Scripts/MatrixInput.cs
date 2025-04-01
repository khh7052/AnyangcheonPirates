using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MatrixInput : MonoBehaviour
{
    public TMP_InputField[] inputFields; // 16개의 입력 필드를 배열로 선언
    public Transform targetTransform; // 행렬을 적용할 트랜스폼

    // 버튼 클릭 시 호출되는 함수
    [ContextMenu("ApplyMatrix")]
    public void ApplyMatrix()
    {
        if (inputFields.Length != 16)
        {
            Debug.LogError("16개의 입력 필드가 필요합니다.");
            return;
        }

        // 4x4 행렬을 생성합니다.
        Matrix4x4 matrix = new();

        // 입력 필드에서 값을 읽어와서 행렬을 채웁니다.
        for (int i = 0; i < 16; i++)
        {
            if (float.TryParse(inputFields[i].text, out float value))
            {
                matrix[i] = value;
            }
            else
            {
                Debug.LogError("잘못된 입력값입니다.");
                return;
            }
        }

        // 트랜스폼에 생성한 행렬을 적용합니다.
        targetTransform.position = new Vector3(matrix.m03, matrix.m13, matrix.m23); // 위치

        // 스케일을 추출합니다.
        Vector3 scale = new Vector3(
            new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude,
            new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude,
            new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude
        );

        targetTransform.localScale = scale;

        // 회전을 추출합니다. 회전 행렬을 정규화하여 추출합니다.
        Matrix4x4 rotationMatrix = matrix;
        rotationMatrix.SetColumn(0, matrix.GetColumn(0) / scale.x);
        rotationMatrix.SetColumn(1, matrix.GetColumn(1) / scale.y);
        rotationMatrix.SetColumn(2, matrix.GetColumn(2) / scale.z);

        targetTransform.rotation = Quaternion.LookRotation(rotationMatrix.GetColumn(2), rotationMatrix.GetColumn(1)); // 회전
    }
}
