using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D collider;
    private Vector2 startPos;
    [SerializeField] private float radius = 0.75f;
    [SerializeField] private float throwSpeed = 30f;
    private Bird birdInSling;
    public LineRenderer trajectoryRenderer;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mousePosition - startPos;
        if (dir.sqrMagnitude > radius)
        {
            dir = dir.normalized * radius;
        }

        transform.position = startPos + dir;

        float distance = Vector2.Distance(startPos, transform.position);

        if (!trajectoryRenderer.enabled)
        {
            trajectoryRenderer.enabled = true;
        }

        DisplayTrajectory(distance);
    }

    private void DisplayTrajectory(float distance)
    {
        if (birdInSling == null) return;

        Vector2 velocity = startPos - (Vector2)transform.position;
        int segmentCount = 5;
        Vector2[] segmentsPosition = new Vector2[segmentCount];

        segmentsPosition[0] = transform.position;
        Vector2 segmentVelocity = velocity * throwSpeed * distance;

        for (int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segmentsPosition[i] = segmentsPosition[0] + segmentVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }

        trajectoryRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            trajectoryRenderer.SetPosition(i, segmentsPosition[i]);
        }
    }

    private void OnMouseUp()
    {
        collider.enabled = false;
        Vector2 velocity = startPos - (Vector2)transform.position;
        float distance = Vector2.Distance(startPos, transform.position);

        birdInSling.Shoot(velocity, distance, throwSpeed);

        gameObject.transform.position = startPos;

        trajectoryRenderer.enabled = false;
    }

    public void InitiateBird(Bird bird)
    {
        birdInSling = bird;
        bird.MoveTo(transform.position, gameObject);
        collider.enabled = true;
    }
}
