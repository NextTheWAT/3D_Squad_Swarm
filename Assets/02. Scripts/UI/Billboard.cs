using UnityEngine;
using UnityEngine.SceneManagement;

// ĳ���� �̸� Text�� ī�޶� �������� ���߰� �ϱ� ���� �ڵ�
public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void OnEnable()
    {
        if (Camera.main != null)
        {
            // ���� ī�޶��� Transform�� ã��
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        // Canvas�� �׻� ī�޶� �ٶ󺸵��� ȸ��
        // LookAt() �Լ��� ������ ���(ī�޶�)�� �ٶ󺸰� ��
        // Vector3.up�� ������Ʈ�� ���� ������ ������ ���ʰ� ��ġ�ϰ� ��
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }
}