using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    GameController gameController;
    public int objectSpeed;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        switch (gameObject.tag)
        {
            case "Sphere":
                gameObject.transform.Translate(Vector3.back * objectSpeed * Time.deltaTime, Space.World);
                gameObject.transform.Rotate(Vector3.left * 120 * Time.deltaTime);
                break;
            case "Square":
                gameObject.transform.Translate(Vector3.right * objectSpeed * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bounds")
        {
            if (gameObject.tag == "Sphere")
            {
                GameController.spheresPresent -= 1;
            }

            Destroy(gameObject);
        }

        if (collider.tag == "Hands" || collider.tag == "MainCamera" || collider.tag == "Gun")
        {
            if (gameObject.tag == "Square")
            {
                GameController.sphereScore -= 20;
            }
        }
    }
}
