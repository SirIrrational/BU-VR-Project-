using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float speedX;
    public float speedY;
    float offsetX;
    float offsetY;

    void FixedUpdate()
    {
        float offsetX = Time.time * speedX;
        float offsetY = Time.time * speedY;
        gameObject.GetComponent<MeshRenderer>().materials[2].mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
