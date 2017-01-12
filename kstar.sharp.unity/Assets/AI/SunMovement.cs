using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{

    public float Speed;
    public float Radius;

    private Transform thisTransform;

    private float sunriseEastDelta = 3.1f;
    private float noonDelta = 1.7f;
    private float midnightDelta = 3.1f;
    private float sunsetWestDelta = 6.3f;

    private float x_offset;
    private float y_offset;


    private float test = 0f;
    // Use this for initialization
    void Start()
    {

        thisTransform = this.transform;

        x_offset = this.transform.position.x;
        y_offset = this.transform.position.y;

        Debug.Log("X: " + x_offset + " Y: " + y_offset);
    }

    // Update is called once per frame
    void Update()
    {

        test += Speed; //0.1f;// 001f;
        if (test > sunsetWestDelta)
            test = 0f;

        var Delta = test; //Time.realtimeSinceStartup * Speed;

        float newX = (Mathf.Sin(Delta) * Radius) + x_offset;
        float newY = (Mathf.Cos(Delta) * Radius) + y_offset;


        //var moveAmount = Speed * Time.deltaTime;
        thisTransform.position = new Vector3(newX, newY); //new Vector3(cos, sin);


        //  Vector3.MoveTowards(
        //  cos,
        //sin,
        //moveAmount);
    }
}
