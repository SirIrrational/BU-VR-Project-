using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position += -gameObject.transform.forward * 2 * Time.deltaTime;
    }
}
