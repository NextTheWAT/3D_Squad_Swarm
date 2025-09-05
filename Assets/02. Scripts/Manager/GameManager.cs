using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        base.Awake();
    }

    // ���� �Ͻ����� �� �簳 �޼���
    public void OnPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("�Ͻ�����");
            // ���� �ð� ����
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("�Ͻ���������");
            // ���� �ð� �簳
            Time.timeScale = 1f;
        }
    }
}
