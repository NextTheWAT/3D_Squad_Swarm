using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        base.Awake();
    }

    // 게임 일시정지 및 재개 메서드
    public void OnPause(bool pause)
    {
        if (pause)
        {
            // 게임 시간 정지
            Time.timeScale = 0f;
        }
        else
        {
            // 게임 시간 재개
            Time.timeScale = 1f;
        }
    }
}
