using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
