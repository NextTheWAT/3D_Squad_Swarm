using UnityEngine;

// ĳ���� �̸� Text�� ī�޶� �������� ���߰� �ϱ� ���� �ڵ�
public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // ���� ī�޶��� Transform�� ã��
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Canvas�� �׻� ī�޶� �ٶ󺸵��� ȸ��
        // LookAt() �Լ��� ������ ���(ī�޶�)�� �ٶ󺸰� ��
        // Vector3.up�� ������Ʈ�� ���� ������ ������ ���ʰ� ��ġ�ϰ� ��
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }
}