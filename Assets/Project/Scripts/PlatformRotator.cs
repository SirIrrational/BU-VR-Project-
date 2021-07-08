using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        gameObject.transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }
}
