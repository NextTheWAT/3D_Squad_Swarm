using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationEventForwarder : MonoBehaviour
{
    public NPC parentNpc; // �θ� ���� (Inspector���� �巡���ؼ� ����)
    private void Awake()
    {
        // Try to automatically get the correct parent if not set manually
        if (parentNpc == null)
        {
            parentNpc = GetComponentInParent<NPC>();
            
        }
    }
    public void OnShootEvent()
    {
        parentNpc.OnShootEvenet();
    }

    public void OnDeathAnimationEnd()
    {
        //npc.OnDeathAnimationEnd();
    }
}
