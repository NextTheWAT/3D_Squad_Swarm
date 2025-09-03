using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationEventForwarder : MonoBehaviour
{
    public NPC parentNpc; // 부모 참조 (Inspector에서 드래그해서 연결)
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
