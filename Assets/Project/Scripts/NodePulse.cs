using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePulse : MonoBehaviour
{
    public float initialNodeScale;
    public float pulseNodeScale;
    public float pulseSpeed;
    float nodeScale;

    void Start()
    {
        gameObject.transform.localScale = new Vector3(initialNodeScale, initialNodeScale, initialNodeScale);
    }

    void Update()
    {
        nodeScale = Mathf.PingPong(Time.time * pulseSpeed, initialNodeScale - pulseNodeScale);
        gameObject.transform.localScale = new Vector3(nodeScale + initialNodeScale, nodeScale + initialNodeScale, nodeScale + initialNodeScale);
    }
}
