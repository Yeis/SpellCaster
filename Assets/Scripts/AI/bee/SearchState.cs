using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BeeState
{
    protected List<Collider2D> results;
    public SearchState(Bee bee) : base(bee)
    {
    }

    public override IEnumerator Start()
    {
        // Bee.print("SearchState");
        Bee.initialPosition = Bee.transform.position;

        Vector2 flowerPosition = ScanAreaForFlowers();
        if (flowerPosition != Vector2.zero)
        {
            Bee.destination = flowerPosition;
            Bee.hasFoundFlower = true;
            // Bee.print("Found Flower at: " + flowerPosition);
        }
        else if(flowerPosition == Bee.initialPosition){
            // Bee.print("Sitting on flower gotta move on");
            SetRandomDirection(Bee.maxDistance * 2);
        }
        else
        {
            SetRandomDirection(Bee.maxDistance);
        }

        FlipSprite();
        Bee.SetState(new GoState(Bee));
        yield break;
    }

    private void FlipSprite()
    {
        if ((Bee.destination.x - Bee.initialPosition.x) >= 0)
        {
            Bee.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            Bee.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override IEnumerator Searching()
    {
        return this.Start();
    }


    private void SetRandomDirection(float distance)
    {
        // If we are calculating a random distance we didn't found flowers
        Bee.hasFoundFlower = false;
        Bee.destination = new Vector2(Random.Range(-distance, distance), Random.Range(-distance, distance));
        Bee.destination += (Vector2)Bee.transform.position;
    }


    private Vector2 ScanAreaForFlowers()
    {

        results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        int numColliders = Physics2D.OverlapCircle(Bee.transform.position, Bee.maxDistance, filter.NoFilter(), results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.tag == "Flower")
            {
                return results[i].transform.position;
            }
        }
        return Vector2.zero;
    }

}
