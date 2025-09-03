using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Player player;


    private void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    public void PlayerSpeedUp()
    {
        player.Stats.BoostStatRound(StatType.Speed, 0.5f);
        Debug.Log("Player Speed Up! 0.5f");

        if (player == null) return;
    }

}
