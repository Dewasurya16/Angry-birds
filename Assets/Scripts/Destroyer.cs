using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if(collisionTag == "Bird" || collisionTag == "Enemy" || collisionTag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }
    }
}
