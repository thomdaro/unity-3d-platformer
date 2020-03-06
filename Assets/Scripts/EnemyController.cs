using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // range for the enemy to move back and forth on
    public float rangeY = 1f;
    // speed at which the enemy moves
    public float speed = 3f;
    // used to switch from moving down to up and vice versa
    public float direction = 1f;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // initialize initial object position
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // consider the direction and speed for movement
        float movementY = direction * speed * Time.deltaTime;
        float newY = transform.position.y + movementY;

        // if we're too far away from the initial position
        if (Mathf.Abs(newY - initialPosition.y) > rangeY)
        {
            // reverse direction
            direction *= -1;
        }
        else
        {
            // otherwise, move along the Y axis
            transform.Translate(new Vector3(0, movementY, 0));
        }
    }
}
