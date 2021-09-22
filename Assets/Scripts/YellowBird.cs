using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField] private float boostForce = 100f;
    private bool boostUsed = false;

    public void Boost()
    {
        if(State == BirdState.Thrown && !boostUsed)
        {
            birdRigidbody.AddForce(birdRigidbody.velocity * boostForce);
            boostUsed = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}
