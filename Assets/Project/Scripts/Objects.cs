using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    GameController gameController;
    AudioSource audioSource;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSource = gameController.GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bounds")
        {
            if (gameObject.tag == "Square")
            {
                GameController.squaresPresent -= 1;
            }
            Destroy(gameObject);
        }
        if (collider.tag == "Hands" || collider.tag == "MainCamera" || collider.tag == "Gun")
        {
            if (gameObject.tag == "Rectangle")
            {
                GameController.squareScore -= 10;
                audioSource.PlayOneShot(gameController.rectangleAudioClip);
            }
        }
        if (collider.tag == "Projectile")
        {
            switch (gameObject.tag)
            {
                case "Square":
                    audioSource.PlayOneShot(gameController.squareAudioClip);
                    break;
                case "Rectangle":
                    audioSource.PlayOneShot(gameController.rectangleAudioClip);
                    break;
            }
        }
    }

    void Movement()
    {
        switch (gameObject.tag)
        {
            case "Square":
                gameObject.transform.Translate(Vector3.back * GameController.objectSpeed * Time.deltaTime, Space.World);
                break;
            case "Rectangle":
                gameObject.transform.Translate(Vector3.right * GameController.objectSpeed * Time.deltaTime);
                break;
        }
    }

    void ObjectColour()
    {

    }
}
