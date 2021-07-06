using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float speedX;
    public float speedY;
    float offsetX;
    float offsetY;
    float originX;
    float originY;

    void Start()
    {
        originX = gameObject.GetComponent<MeshRenderer>().materials[2].mainTextureOffset.x;
        originY = gameObject.GetComponent<MeshRenderer>().materials[2].mainTextureOffset.y;
    }

    void FixedUpdate()
    {
        offsetX = Time.time * speedX + originX;
        offsetY = Time.time * speedY + originY;
        gameObject.GetComponent<MeshRenderer>().materials[2].mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
