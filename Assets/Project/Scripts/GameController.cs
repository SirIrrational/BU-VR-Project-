using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : MonoBehaviour
{
    public GameObject vrRigPrefab;
    public GameObject playButton;
    public GameObject homeButton;
    public GameObject sphere;
    public GameObject[] squares;
    public int spheresToSpawn;
    public int minSpawnDelay;
    public int maxSpawnDelay;
    public AudioClip music;
    public static int sphereScore = 0;
    public static int shotsFired = 0;
    public static int spheresPresent;
    int spheresSpawned;
    GameObject[] counters;
    GameObject[] sphereSpawns;
    GameObject[] squareSpawns;
    Transform[] sphereSpawnTransforms;
    Transform[] squareSpawnTransforms;
    Counter[] counter;
    bool endActivated = false;
    XRRayInteractor[] xrRayInteractor;
    AudioSource audioSource;
    
    void Start()
    {
        // Instantiates arrays linked to both counters
        counters = GameObject.FindGameObjectsWithTag("Counter");
        sphereSpawns = GameObject.FindGameObjectsWithTag("SphereSpawn");
        squareSpawns = GameObject.FindGameObjectsWithTag("SquareSpawn");
        counter = new Counter[counters.Length];
        sphereSpawnTransforms = new Transform[sphereSpawns.Length];
        squareSpawnTransforms = new Transform[squareSpawns.Length];
        xrRayInteractor = vrRigPrefab.GetComponentsInChildren<XRRayInteractor>();
        for (int index = 0; index < counters.Length; index++)
        {
            counter[index] = counters[index].GetComponent<Counter>();
        }
        for (int index = 0; index < sphereSpawns.Length; index++)
        {
            sphereSpawnTransforms[index] = sphereSpawns[index].transform;
        }
        for (int index = 0; index < squareSpawns.Length; index++)
        {
            squareSpawnTransforms[index] = squareSpawns[index].transform;
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        Restart();
    }

    void Update()
    {
        // Send both scores to UI
        counter[0].value.text = sphereScore.ToString();
        counter[1].value.text = shotsFired.ToString();

        if (endActivated)
        {
            End();
        }
    }

    // Starts the game
    public void Game()
    {
        for (int index = 0; index < xrRayInteractor.Length; index++)
        {
            xrRayInteractor[index].enabled = false;
        }
        playButton.SetActive(false);
        sphereScore = 0;
        shotsFired = 0;
        counter[0].Title.text = "Score";
        counter[1].Title.text = "Shots Fired";
        counter[0].image.enabled = false;
        counter[1].image.enabled = false;
        StartCoroutine(SpawnDelay());
        audioSource.PlayOneShot(music);
    }

    void SpawnObjects()
    {
        switch (Random.Range(0 , 4))
        {
            case 0:
                SphereSpawn();
                break;

            case 1:
                SphereSpawn();
                break;

            case 2:
                SphereSpawn();
                break;

            case 3:
                SquareSpawn();
                break;
        }

        if (spheresSpawned < spheresToSpawn)
        {
            StartCoroutine(SpawnDelay());
        }

        if (spheresSpawned >= spheresToSpawn)
        {
            endActivated = true;
        }
    }

    void SphereSpawn()
    {
        GameObject sphereTarget = Instantiate(sphere, sphereSpawnTransforms[Random.Range(0, sphereSpawnTransforms.Length)]);
        spheresSpawned += 1;
        spheresPresent += 1;
    }

    void SquareSpawn()
    {
        GameObject squareTarget = Instantiate(squares[Random.Range(0, squares.Length)], squareSpawnTransforms[Random.Range(0, squareSpawnTransforms.Length)]);

        switch (Random.Range(0, 2))
        {
            case 0:
                squareTarget.transform.Rotate(0f, 0f, 0f);
                break;
            case 1:
                squareTarget.transform.Rotate(90f, 0f, 0f);
                break;
        }
    }

    public void Home()
    {
        // Loads the main menu scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //Resets all game variables
    void Restart()
    {
        playButton.SetActive(true);
        homeButton.SetActive(false);
        counter[0].Title.text = "Shoot";
        counter[1].Title.text = "Dodge";
        counter[0].image.enabled = true;
        counter[1].image.enabled = true;
        spheresSpawned = 0;
        spheresPresent = 0;
        endActivated = false;
    }

    void End()
    {
        if (spheresPresent <= 0)
        {
            for (int index = 0; index < xrRayInteractor.Length; index++)
            {
                xrRayInteractor[index].enabled = true;
            }
            homeButton.SetActive(true);
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnObjects();
    }
}
