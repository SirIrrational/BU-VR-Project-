using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    GameController gameController;
    AudioSource audioSource;
    public int rectangleContactScore;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSource = gameController.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        gameObject.transform.Translate(Vector3.right * GameController.objectSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hands" || collider.tag == "MainCamera" || collider.tag == "Gun")
        {
            GameController.squareScore -= rectangleContactScore;
            audioSource.PlayOneShot(gameController.rectangleAudioClip);
        }
        if (collider.tag == "Projectile")
        {
            audioSource.PlayOneShot(gameController.rectangleAudioClip);
        }
    }
}
