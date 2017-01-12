using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    // These values will appear in the editor, full properties will not.
    public float Speed = 50;
    private Transform _playerTransform;
    private Transform _myTransform;

    // Use this for initialization
    void Start()
    {

        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError(
              "Could not find the main player. Ensure it has the player tag set.");
        }
        else
        {
            // Grab a reference to its transform for use later (saves on managed
            // code to native code calls).
            _playerTransform = player.transform;
        }
        // Grab a reference to our transform for use later.
        _myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // I am setting how fast I should move toward the "player"
        // per second. In Unity, one unit is a meter.
        // Time.deltaTime gives the amount of time since the last frame.
        // If you're running 60 FPS (frames per second) this is 1/60 = 0.0167,
        // so w/Speed=2 and frame rate of 60 FPS (frame rate always varies
        // per second), I have a movement amount of 2*0.0167 = .033 units
        // per frame. This is 2 units.
        var moveAmount = Speed * Time.deltaTime;
        // Update the position, move toward the player's position by moveAmount.
        _myTransform.position = Vector3.MoveTowards(_myTransform.position,
          _playerTransform.position, moveAmount);

    }
}
