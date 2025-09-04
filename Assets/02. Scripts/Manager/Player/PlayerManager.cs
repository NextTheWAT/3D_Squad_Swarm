using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] public Player player;


    public float playerSpeed = 0f;

    private void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();

    }
    private void OnEnable()
    {
        if (player != null)
        {
            playerSpeed = player.Stats.GetStat(StatType.Speed);
        }
    }

    public void PlayerSpeedUp()
    {
        player.Stats.BoostStatRound(StatType.Speed, 0.5f);
        Debug.Log("Player Speed Up! 0.5f");

        if (player == null) return;
    }
    public void PlayerSpeedReset()
    {
        player.Stats.ResetStat(StatType.Speed, playerSpeed);
        Debug.Log("Player Speed Reset!");

        if (player == null) return;
    }

}
