using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    SceneData[] sceneData;
    public GameObject sceneNodePrefab;
    public GameObject informationNodePrefab;
    public GameObject vrRigPrefab;
    public GameObject tourUI;
    public GameObject textUI;
    public Text sceneDescriptionText;
    public Text sceneTitleText;
    public SceneData initialScene;
    GameObject[] buttons;
    GameObject vrCamera;
    GameObject nodeSpawnPoint;
    GameObject[] sceneNodeSpawned;
    GameObject[] nodeSpawned;
    AudioSource[] sceneNodeSpawnedAudioSource;
    Skybox skybox;
    public static int activeScene;
    public static bool tourUIActive = true;
    public static bool infoUIActive = false;
    public static bool audioActive = true;

    void Start()
    {
        // Instantiates arrays linked to present scene data
        buttons = GameObject.FindGameObjectsWithTag("Button");
        sceneData = new SceneData[buttons.Length];
        for(int index = 0; index < buttons.Length; index++)
        {
            sceneData[index] = buttons[index].GetComponent<TourButton>().sceneData;
            buttons[index].GetComponent<TourButtonConfiguration>().buttonIndex = index;
        }
        sceneNodeSpawned = new GameObject[sceneData.Length];
        sceneNodeSpawnedAudioSource = new AudioSource[sceneData.Length];
        skybox = vrRigPrefab.GetComponentInChildren<Skybox>();
        vrCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Starts system
        SpawnNodes();
        LoadScene(initialScene);
    }

    void LateUpdate()
    {
        // Updates the VR headset position so its actual position can be accounted for
        nodeSpawnPoint.transform.position = vrCamera.transform.position / 2;
    }

    void SpawnNodes()
    {
        // Creates a gameObject based on the position of the VR headset
        nodeSpawnPoint = new GameObject("Node Spawn Point");
        nodeSpawnPoint.tag = "NodeSpawnPoint";

        // Creates scenes based on the related scene data
        for (int index = 0; index < sceneData.Length; index++)
        {
            nodeSpawned = new GameObject[sceneData[index].nodeData.Length];
            sceneNodeSpawned[index] = Instantiate(sceneNodePrefab, nodeSpawnPoint.transform.position, Quaternion.identity, nodeSpawnPoint.transform);
            sceneNodeSpawnedAudioSource[index] = sceneNodeSpawned[index].GetComponent<AudioSource>();
            sceneNodeSpawned[index].GetComponent<SceneDisplay>().SceneLoad(sceneData[index]);
            sceneNodeSpawned[index].name = sceneData[index].sceneName;

            // Checks if any node data i being used based on array length
            if (sceneData[index].nodeData.Length != 0)
            {
                // Creates the information nodes related to the scene data
                for (int secondIndex = 0; secondIndex < sceneData[index].nodeData.Length; secondIndex++)
                {
                    nodeSpawned[secondIndex] = Instantiate(informationNodePrefab, NodePositionPlacement(sceneData[index].nodeData[secondIndex]), Quaternion.identity, sceneNodeSpawned[index].transform);
                    NodeRotationPlacement(sceneData[index].nodeData[secondIndex], nodeSpawned[secondIndex]);
                    nodeSpawned[secondIndex].GetComponent<NodeDisplay>().NodeLoad(sceneData[index].nodeData[secondIndex]);
                }
            }
            //nodeSpawned = GameObject.FindGameObjectsWithTag("InformationNode");
        }
    }

    public void LoadScene(SceneData newSceneName)
    {
        // Loads the scene and its data from a button press
        for (int index = 0; index < sceneNodeSpawned.Length; index++)
        {
            if (newSceneName.sceneName == sceneNodeSpawned[index].name)
            {
                activeScene = index;
                sceneNodeSpawned[activeScene].SetActive(true);
                sceneTitleText.text = sceneData[activeScene].sceneName;
                sceneDescriptionText.text = sceneData[activeScene].sceneDescription;
                SkyboxSwap(sceneData[activeScene]);
                Audio();
                Tour();
            }
            else
            {
                sceneNodeSpawned[index].SetActive(false);
            }
        }
    }

    void SkyboxSwap (SceneData newSceneData)
    {
        // Swaps 360 image to the stored scene data
        skybox.material.SetTexture("_MainTex", newSceneData.panoramaImage);
    }

    void Audio()
    {
        // Plays audio if a clip exists and the audiosource is not currently playing 
        if (sceneData[activeScene].sceneAudio != null)
        {
            if (audioActive)
            {
                if (!sceneNodeSpawnedAudioSource[activeScene].isPlaying)
                {
                    sceneNodeSpawnedAudioSource[activeScene].PlayOneShot(sceneData[activeScene].sceneAudio);
                }
            }
            else
            {
                // Mutes audio based on the audio button pressed
                sceneNodeSpawnedAudioSource[activeScene].Stop();
            }
        }
    }

    public void PlayAudio()
    {
        // Audio play toggle
        if (audioActive)
        {
            audioActive = false;
        }
        else
        {
            audioActive = true;
        }

        Audio();
    }

    public void Home()
    {
        // Loads the main menu scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Tour()
    {
        // Tour button toggle
        if (tourUIActive)
        {
            tourUI.SetActive(false);
            tourUIActive = false;
        }
        else if (!tourUIActive)
        {
            tourUI.SetActive(true);
            textUI.SetActive(false);
            tourUIActive = true;
            infoUIActive = false;
        }
    }

    public void Info()
    {
        // Text button toggle
        if (infoUIActive)
        {
            textUI.SetActive(false);
            infoUIActive = false;
        }
        else
        {
            textUI.SetActive(true);
            tourUI.SetActive(false);
            infoUIActive = true;
            tourUIActive = false;
        }
    }

    Vector3 NodePositionPlacement(NodeData newNodeData)
    {
        // Places the information nodes to their specified distance from the VR headset
        Vector3 positionPlacement;
        positionPlacement = nodeSpawnPoint.transform.position + new Vector3(0f, 0f, newNodeData.nodeDistance);
        return positionPlacement;
    }

    void NodeRotationPlacement(NodeData newNodeData, GameObject newNodeSpawned)
    {
        // Rotates the information node at an orbit specified by their angle around the VR headset
        newNodeSpawned.transform.RotateAround(nodeSpawnPoint.transform.position, Vector3.up, newNodeData.nodeOrbitLocation);
    }
}
