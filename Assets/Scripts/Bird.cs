using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public GameObject Parent;
    public Rigidbody2D birdRigidbody;
    public CircleCollider2D birdCollider;

    private BirdState state;
    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };

    public BirdState State { get => state; private set => state = value; }

    private void Start()
    {
        birdRigidbody.bodyType = RigidbodyType2D.Kinematic;
        birdCollider.enabled = false;
        state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        if(state == BirdState.Idle && birdRigidbody.velocity.sqrMagnitude >= minVelocity)
        {
            state = BirdState.Thrown;
        }

        if((state == BirdState.Thrown || state == BirdState.HitSomething) && birdRigidbody.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float timeInSecond)
    {
        yield return new WaitForSeconds(timeInSecond);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        birdCollider.enabled = true;
        birdRigidbody.bodyType = RigidbodyType2D.Dynamic;
        birdRigidbody.velocity = velocity * distance * speed;
        OnBirdShot(this);
    }

    public virtual void OnTap()
    {

    }

    private void OnDestroy()
    {
        if(state == BirdState.Thrown || state == BirdState.HitSomething)
            OnBirdDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = BirdState.HitSomething;
    }
}
