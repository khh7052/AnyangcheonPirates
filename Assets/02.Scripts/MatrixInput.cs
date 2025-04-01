using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MatrixInput : MonoBehaviour
{
    public TMP_InputField[] inputFields; // 16���� �Է� �ʵ带 �迭�� ����
    public Transform targetTransform; // ����� ������ Ʈ������

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    [ContextMenu("ApplyMatrix")]
    public void ApplyMatrix()
    {
        if (inputFields.Length != 16)
        {
            Debug.LogError("16���� �Է� �ʵ尡 �ʿ��մϴ�.");
            return;
        }

        // 4x4 ����� �����մϴ�.
        Matrix4x4 matrix = new();

        // �Է� �ʵ忡�� ���� �о�ͼ� ����� ä��ϴ�.
        for (int i = 0; i < 16; i++)
        {
            if (float.TryParse(inputFields[i].text, out float value))
            {
                matrix[i] = value;
            }
            else
            {
                Debug.LogError("�߸��� �Է°��Դϴ�.");
                return;
            }
        }

        // Ʈ�������� ������ ����� �����մϴ�.
        targetTransform.position = new Vector3(matrix.m03, matrix.m13, matrix.m23); // ��ġ

        // �������� �����մϴ�.
        Vector3 scale = new Vector3(
            new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude,
            new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude,
            new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude
        );

        targetTransform.localScale = scale;

        // ȸ���� �����մϴ�. ȸ�� ����� ����ȭ�Ͽ� �����մϴ�.
        Matrix4x4 rotationMatrix = matrix;
        rotationMatrix.SetColumn(0, matrix.GetColumn(0) / scale.x);
        rotationMatrix.SetColumn(1, matrix.GetColumn(1) / scale.y);
        rotationMatrix.SetColumn(2, matrix.GetColumn(2) / scale.z);

        targetTransform.rotation = Quaternion.LookRotation(rotationMatrix.GetColumn(2), rotationMatrix.GetColumn(1)); // ȸ��
    }
}
