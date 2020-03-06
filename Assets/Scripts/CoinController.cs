using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float rotationSpeed = 100f;   

    void Update()
    {
        // dist in degrees to rotate each frame
        float angle = rotationSpeed * Time.deltaTime;

        // rotate on the y axis
        transform.Rotate(Vector3.up * angle, Space.World);
    }
}
