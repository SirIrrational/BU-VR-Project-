using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraTrack : MonoBehaviour
{
    public GameObject vrCamera;
    public float smoothFactor;
    Quaternion smoothedRotation;


    void Start()
    {

    }

    void Update()
    {
        smoothedRotation = Quaternion.Slerp(new Quaternion (0f, gameObject.transform.rotation.y, 0f, gameObject.transform.rotation.w), new Quaternion (0f, vrCamera.transform.rotation.y, 0f, vrCamera.transform.rotation.w), smoothFactor * Time.deltaTime);
        gameObject.transform.rotation = smoothedRotation;
    }
}
