using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1, point2, startingPoint;
    public float platformSpeed = 1f;

    Vector2 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
       float timeSteps = platformSpeed * Time.deltaTime;
       if(transform.position == point1.position)
       {
           nextPosition = point2.position;
       }
       else if(transform.position == point2.position)
       {
           nextPosition = point1.position;
       }

       transform.position = Vector2.MoveTowards(transform.position, nextPosition, timeSteps);
    }
}