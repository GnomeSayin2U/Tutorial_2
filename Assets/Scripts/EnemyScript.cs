using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1.0F;
    public Transform startMarker;
    public Transform endMarker;

    private float startTime;

    private float journeyLength;
    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector2.Distance(startMarker.position,endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));

       

    }
}