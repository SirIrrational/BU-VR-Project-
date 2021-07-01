using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject playButton;
    public static int sphereScore = 0;
    public static int shotsFired = 0;
    GameObject[] counters;
    Counter[] counter;
    
    void Start()
    {
        // Instantiates arrays linked to both counters
        counters = GameObject.FindGameObjectsWithTag("Counter");
        counter = new Counter[counters.Length];
        for (int index = 0; index < counters.Length; index++)
        {
            counter[index] = counters[index].GetComponent<Counter>();
        }
        Restart();
    }

    void Update()
    {
        // Send both scores to UI
        counter[0].value.text = sphereScore.ToString();
        counter[1].value.text = shotsFired.ToString();
    }

    // Starts the game
    public void Game()
    {
        playButton.SetActive(false);
        sphereScore = 0;
        shotsFired = 0;
        counter[0].Title.text = "Score";
        counter[1].Title.text = "Shots Fired";
        counter[0].image.enabled = false;
        counter[1].image.enabled = false;
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
        counter[0].Title.text = "Shoot";
        counter[1].Title.text = "Dodge";
        counter[0].image.enabled = true;
        counter[1].image.enabled = true;
    }
}
