using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
    public Player parentPlayer;

    private void Awake()
    {
        if (parentPlayer == null)
        {
            parentPlayer = GetComponentInParent<Player>();
        }
    }

    // This is the function called by the animation event
    public void OnAttackAnimationComplete()
    {
        parentPlayer.OnAttackAnimationComplete();
    }

    public void OnAttackHit()
    {
        parentPlayer.OnAttackHit();
    }
}
