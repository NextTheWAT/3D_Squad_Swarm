using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
    public Player parentPlayer;
    public FollowerZombie parentZombie;

    private void Awake()
    {
        // Try to automatically get the correct parent if not set manually
        if (parentPlayer == null && parentZombie == null)
        {
            parentPlayer = GetComponentInParent<Player>();
            parentZombie = GetComponentInParent<FollowerZombie>();
        }
    }

    // Called by the animation event
    public void OnAttackAnimationComplete()
    {
        if (parentPlayer != null)
        {
            parentPlayer.OnAttackAnimationComplete();
        }
        else if (parentZombie != null)
        {
            parentZombie.OnAttackAnimationComplete();
        }
    }

    // Called by the animation event
    public void OnAttackHit()
    {
        if (parentPlayer != null)
        {
            parentPlayer.OnAttackHit();
        }
        else if (parentZombie != null)
        {
            parentZombie.OnAttackHit();
        }
    }

    public void OnDeathAnimationComplete()
    {
        if (parentPlayer != null)
        {
            parentPlayer.OnDeathAnimationComplete();
        }
        else if (parentZombie != null)
        {
            parentZombie.OnDeathAnimationComplete();
        }
    }

    public void OnRiseComplete()
    {
        if (parentZombie != null)
        {
            parentZombie.OnRiseComplete();
        }
    }
}
