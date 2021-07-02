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
            switch (gameObject.tag)
            {
                case "Sphere":
                    GameController.spheresPresent -= 1;
                    Destroy(gameObject);
                    break;
                case "Square":
                    Destroy(gameObject);
                    break;
            }
        }

        if (collider.tag == "Projectile")
        {
            switch (gameObject.tag)
            {
                case "Sphere":
                    GameController.sphereScore += 10;
                    Destroy(gameObject);
                    break;
                case "Square":
                    GameController.sphereScore -= 10;
                    break;
            }
        }

        if (collider.tag == "Hands" || collider.tag == "MainCamera")
        {
            switch (gameObject.tag)
            {
                case "Square":
                    GameController.sphereScore -= 50;
                    break;
            }
        }
    }
}
