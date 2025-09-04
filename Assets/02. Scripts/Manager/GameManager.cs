using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // ���� �ð� ����
            Time.timeScale = 0f;
        }
        else
        {
            // ���� �ð� �簳
            Time.timeScale = 1f;
        }
    }
}
