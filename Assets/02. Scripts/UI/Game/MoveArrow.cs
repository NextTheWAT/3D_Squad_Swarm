using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    private Vector3 originalPosition;
    private float timer;

    private void OnEnable()
    {
        // ���� �� ���� ��ġ�� ���� ��ġ�� ����
        originalPosition = transform.position;
        timer = 0f;
    }

    void Update()
    {
        // Ÿ�ӽ����ϰ� ������� �ð��� �����Ϸ��� �Ʒ��� ���� ����ؾ� �մϴ�.
        timer += Time.unscaledDeltaTime;

        // ���� �������� �� ������ 0.1�� �̵�
        transform.position += Vector3.up * 0.03f;

        // Ÿ�̸Ӱ� 3�ʸ� �ʰ��ϸ�
        if (timer >= 1f)
        {
            // ���� ��ġ�� �ǵ�����
            transform.position = originalPosition;
            timer = 0f;
        }
    }
}
