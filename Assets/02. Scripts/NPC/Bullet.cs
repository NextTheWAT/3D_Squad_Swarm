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
        //�÷��̾ �ѿ�����
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Zombie"))
        {
            var target = collision.gameObject.GetComponent<StatHandler>();
            target.isAlive = false;
        }
        Destroy(gameObject);
    }
}
