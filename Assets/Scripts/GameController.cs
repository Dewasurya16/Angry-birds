using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public List<Bird> birds;
    public List<Enemy> enemies;
    public TrailController trailController;
    public BoxCollider2D tapCollider;

    private Bird shotBird;
    private bool isGameEnded = false;

    private void Start()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdShot += AssignTrail;
            birds[i].OnBirdDestroyed += ChangeBird;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShooter.InitiateBird(birds[0]);
        shotBird = birds[0];
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;

        if (isGameEnded) return;

        birds.RemoveAt(0);

        if(birds.Count > 0)
        {
            slingShooter.InitiateBird(birds[0]);
            shotBird = birds[0];
        }
    }

    public void AssignTrail(Bird selectedBird)
    {
        trailController.SetBird(selectedBird);
        StartCoroutine(trailController.SpawnTrails());
        tapCollider.enabled = true;
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if(enemies.Count == 0)
        {
            Debug.Log("SELAMAT KAMU MENANG!");
            isGameEnded = true;
        }
    }

    private void OnMouseUp()
    {
        if(shotBird != null)
        {
            shotBird.OnTap();
        }
    }
}
