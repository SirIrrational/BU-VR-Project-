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
    public GameObject navigationUI;
    public Text sceneDescriptionText;
    public SceneData initialScene;
    GameObject[] Buttons;
    GameObject vrCamera;
    GameObject nodeSpawnPoint;
    GameObject[] sceneNodeSpawned;
    GameObject[] nodeSpawned;
    AudioSource[] sceneNodeSpawnedAudioSource;
    Skybox skybox;
    int activeScene;
    bool navigationUIActive = false;

    void Start()
    {
        // Instantiates arrays linked to present scene data
        Buttons = GameObject.FindGameObjectsWithTag("Button");
        sceneData = new SceneData[Buttons.Length];
        for(int index = 0; index < Buttons.Length; index++)
        {
            sceneData[index] = Buttons[index].GetComponent<ThumbnailButton>().sceneData;
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

            // Creates the information nodes related to the scene data
            for (int secondIndex = 0; secondIndex <sceneData[index].nodeData.Length; secondIndex++)
            {
                nodeSpawned[secondIndex] = Instantiate(informationNodePrefab, NodePositionPlacement(sceneData[index].nodeData[secondIndex]), Quaternion.identity, sceneNodeSpawned[index].transform);
                NodeRotationPlacement(sceneData[index].nodeData[secondIndex], nodeSpawned[secondIndex]);
                nodeSpawned[secondIndex].GetComponent<NodeDisplay>().NodeLoad(sceneData[index].nodeData[secondIndex]);
            }
            nodeSpawned = GameObject.FindGameObjectsWithTag("InformationNode");
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
                sceneNodeSpawned[index].SetActive(true);
                SkyboxSwap(sceneData[index]);
                PlayAudio();
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

    void PlayAudio()
    {
        // Plays audio if a clip exists and the audiosource is not currently playing 
        if (sceneData[activeScene].sceneAudio != null)
        {
            if (!sceneNodeSpawnedAudioSource[activeScene].isPlaying)
            {
                sceneNodeSpawnedAudioSource[activeScene].PlayOneShot(sceneData[activeScene].sceneAudio);
            }
        }
    }

    public void MuteAudio()
    {
        // Mutes audio based on the audio button pressed
        sceneNodeSpawnedAudioSource[activeScene].Stop();
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Navigation()
    {
        if (navigationUIActive)
        {
            navigationUI.SetActive(false);
        }
        else if (!navigationUIActive)
        {
            navigationUI.SetActive(true);
        }
    }

    public void SceneDescription()
    {
        sceneDescriptionText.text = sceneData[activeScene].sceneDescription;
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
