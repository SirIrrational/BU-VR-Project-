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
    public GameObject counter;
    public GameObject square;
    public GameObject[] rectangles;
    public int squaresToSpawn;
    public int minSpawnDelay;
    public int maxSpawnDelay;
    public float colourLerpTime;
    public int[] levelSpeed;
    public int[] levelRequiredScore;
    public Color[] levelColours;
    public AudioClip squareAudioClip;
    public AudioClip rectangleAudioClip;
    public static int squareScore = 0;
    public static int squaresPresent = 0;
    public static int objectSpeed = 0;
    public static int levelState = 0;
    int squaresSpawned;
    GameObject[] rectangleSpawns;
    GameObject[] squareSpawns;
    GameObject[] lamps;
    GameObject[] platforms;
    GameObject[] lights;
    MeshRenderer[] lampMeshRenderers;
    MeshRenderer[] platformsMeshRenderers;
    Light[] lampLights;
    Light[] lightLights;
    Transform[] rectangleSpawnTransforms;
    Transform[] squareSpawnTransforms;
    bool endActivated = false;
    XRRayInteractor[] xrRayInteractor;
    AudioSource audioSource;
    Text[] counterTexts;
    float[] lerpTimer = { 0, 0, 0 };
    Color tempColour;
    
    void Start()
    {
        // Instantiates arrays
        squareSpawns = GameObject.FindGameObjectsWithTag("SquareSpawn");
        rectangleSpawns = GameObject.FindGameObjectsWithTag("RectangleSpawn");
        lamps = GameObject.FindGameObjectsWithTag("Lamp");
        platforms = GameObject.FindGameObjectsWithTag("Emissive");
        lights = GameObject.FindGameObjectsWithTag("Light");
        squareSpawnTransforms = new Transform[squareSpawns.Length];
        rectangleSpawnTransforms = new Transform[rectangleSpawns.Length];
        lampMeshRenderers = new MeshRenderer[lamps.Length];
        platformsMeshRenderers = new MeshRenderer[platforms.Length];
        lampLights = new Light[lamps.Length];
        lightLights = new Light[lights.Length];
        xrRayInteractor = vrRigPrefab.GetComponentsInChildren<XRRayInteractor>();
        audioSource = gameObject.GetComponent<AudioSource>();
        counterTexts = counter.GetComponentsInChildren<Text>();
        counterTexts[0].text = "Score";
        tempColour = RenderSettings.fogColor;
        for (int index = 0; index < squareSpawns.Length; index++)
        {
            squareSpawnTransforms[index] = squareSpawns[index].transform;
        }
        for (int index = 0; index < rectangleSpawns.Length; index++)
        {
            rectangleSpawnTransforms[index] = rectangleSpawns[index].transform;
        }
        for (int index = 0; index < lamps.Length; index++)
        {
            lampMeshRenderers[index] = lamps[index].GetComponent<MeshRenderer>();
            lampMeshRenderers[index].material.EnableKeyword("_EMISSION");
            lampLights[index] = lamps[index].GetComponentInChildren<Light>();
        }
        for (int index = 0; index < platforms.Length; index++)
        {
            platformsMeshRenderers[index] = platforms[index].GetComponent<MeshRenderer>();
            platformsMeshRenderers[index].material.EnableKeyword("_EMISSION");
        }
        for (int index = 0; index < lights.Length; index++)
        {
            lightLights[index] = lights[index].GetComponent<Light>();
        }
        Restart();
    }

    void Update()
    {
        End();
        Levels();
        LerpTimer();
    }

    // Starts the game
    public void Game()
    {
        for (int index = 0; index < xrRayInteractor.Length; index++)
        {
            xrRayInteractor[index].enabled = false;
        }
        playButton.SetActive(false);
        squareScore = 0;
        StartCoroutine(SpawnDelay());
        audioSource.Play();
    }

    void SpawnObjects()
    {
        switch (Random.Range(0 , 4))
        {
            case 0:
                SquareSpawn();
                break;
            case 1:
                SquareSpawn();
                break;
            case 2:
                SquareSpawn();
                break;
            case 3:
                RectangleSpawn();
                break;
        }
        if (squaresSpawned < squaresToSpawn)
        {
            StartCoroutine(SpawnDelay());
        }
        if (squaresSpawned >= squaresToSpawn)
        {
            endActivated = true;
        }
    }

    void SquareSpawn()
    {
        GameObject squareTarget = Instantiate(square, squareSpawnTransforms[Random.Range(0, squareSpawnTransforms.Length)]);
        squaresSpawned += 1;
        squaresPresent += 1;
    }

    void RectangleSpawn()
    {
        GameObject rectangleTarget = Instantiate(rectangles[Random.Range(0, rectangles.Length)], rectangleSpawnTransforms[Random.Range(0, rectangleSpawnTransforms.Length)]);

        switch (Random.Range(0, 2))
        {
            case 0:
                rectangleTarget.transform.Rotate(0f, 0f, 0f);
                break;
            case 1:
                rectangleTarget.transform.Rotate(90f, 0f, 0f);
                break;
        }
    }

    public void Home()
    {
        // Loads the main menu scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //Resets all static variables
    void Restart()
    {
        squaresSpawned = 0;
        squaresPresent = 0;
        levelState = 0;
        objectSpeed = 0;
    }

    void End()
    {
        if (endActivated)
        {
            if (squaresPresent <= 0)
            {
                for (int index = 0; index < xrRayInteractor.Length; index++)
                {
                    xrRayInteractor[index].enabled = true;
                }
                homeButton.SetActive(true);
                audioSource.Stop();
            }
        }
    }

    void Levels()
    {
        counterTexts[1].text = squareScore.ToString();

        if (squareScore >= levelRequiredScore[0] && squareScore <= levelRequiredScore[1])
        {       
            objectSpeed = levelSpeed[0];
            levelState = 0;         
        }
        else if (squareScore >= levelRequiredScore[1] && squareScore < levelRequiredScore[2])
        {          
            objectSpeed = levelSpeed[1];
            levelState = 1;        
        }
        else if (squareScore >= levelRequiredScore[2])
        {           
            objectSpeed = levelSpeed[2];
            levelState = 2;      
        }

        Lighting();
    }

    void Lighting()
    {
        tempColour = RenderSettings.fogColor;

        void LightingLoop(Color newColour)
        {
            for (int index = 0; index < lampMeshRenderers.Length; index++)
            {
                lampMeshRenderers[index].materials[1].SetColor("_EmissionColor", newColour * 2.25f);
                lampLights[index].color = newColour;
            }
            for (int index = 0; index < platformsMeshRenderers.Length; index++)
            {
                platformsMeshRenderers[index].materials[0].SetColor("_EmissionColor", newColour * 2.25f);
            }
            for (int index = 0; index < lightLights.Length; index++)
            {
                lightLights[index].color = newColour;
            }

            RenderSettings.fogColor = newColour;
        }

        Color ColourLerp(Color originalColour, Color newColour, float lerpTimer)
        {
            return Color.Lerp(originalColour, newColour, lerpTimer);
        }

        switch (levelState)
        {
            case 0:
                LightingLoop(ColourLerp(tempColour, levelColours[0], lerpTimer[0]));
                lerpTimer[1] = 0;
                lerpTimer[2] = 0;
                break;
            case 1:              
                LightingLoop(ColourLerp(tempColour, levelColours[1], lerpTimer[1]));
                lerpTimer[0] = 0;
                lerpTimer[2] = 0;
                break;
            case 2: 
                LightingLoop(ColourLerp(tempColour, levelColours[2], lerpTimer[2]));
                lerpTimer[0] = 0;
                lerpTimer[1] = 0;
                break;
        }
    }

    void LerpTimer()
    {
        switch (levelState)
        {
            case 0:
                lerpTimer[0] += Time.deltaTime / colourLerpTime;
                break;
            case 1:
                lerpTimer[1] += Time.deltaTime / colourLerpTime;
                break;
            case 2:
                lerpTimer[2] += Time.deltaTime / colourLerpTime;
                break;
        }

  
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnObjects();
    }
}
