using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        if(UIManager.Instance != null) UIManager.Instance.SetPlayGame();
    }
}
