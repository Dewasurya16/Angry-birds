using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject Trail;
    public Bird targetBird;
    private List<GameObject> trails;

    private void Start()
    {
        trails = new List<GameObject>();
    }

    public void SetBird(Bird selectedBird)
    {
        targetBird = selectedBird;

        for (int i = 0; i < trails.Count; i++)
        {
            Destroy(trails[i].gameObject);
        }

        trails.Clear();
    }

    public IEnumerator SpawnTrails()
    {
        GameObject newTrailGO = Instantiate(Trail, targetBird.transform.position, Quaternion.identity);
        trails.Add(newTrailGO);

        yield return new WaitForSeconds(0.1f);

        if(targetBird != null && targetBird.State != Bird.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrails());
        }
    }
}
