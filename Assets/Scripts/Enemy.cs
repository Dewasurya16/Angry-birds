using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private bool isHit = false;

    private void OnDestroy()
    {
        if (isHit) OnEnemyDestroyed(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if(collision.gameObject.tag == "Bird")
        {
            isHit = true;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            //hitung damage dari kecepatan obstacle menghantam enemy
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;
            health -= damage;

            if(health <= 0)
            {
                isHit = true;
                Destroy(gameObject);
            }
        }
    }
}
