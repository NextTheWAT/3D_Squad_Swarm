using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        //�÷��̾ �ѿ�����
        if (player != null && player.Stats.isAlive)
        {
            player.Stats.isAlive = false;
        }
        Destroy(gameObject);
    }
}
