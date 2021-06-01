﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public SceneData[] sceneData;
    public GameObject sceneNodePrefab;
    public GameObject informationNodePrefab;
    public GameObject vrRigPrefab;
    public string initialScene;
    GameObject vrCamera;
    GameObject nodeSpawnPoint;
    GameObject[] sceneNodeSpawned;
    GameObject[] nodeSpawned;
    AudioSource[] sceneNodeSpawnedAudioSource;
    Skybox skybox;

    void Start()
    {
        // Instantiation of variables
        skybox = vrRigPrefab.GetComponentInChildren<Skybox>();
        vrCamera = GameObject.FindGameObjectWithTag("MainCamera");
        sceneNodeSpawned = new GameObject[sceneData.Length];
        sceneNodeSpawnedAudioSource = new AudioSource[sceneData.Length];

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
        // Creates a gameobject based on the position of the VR headset
        nodeSpawnPoint = new GameObject("Node Spawn Point");
        nodeSpawnPoint.tag = "NodeSpawnPoint";

        // Creates scene based on the related scene data
        for (int index = 0; index < sceneData.Length; index++)
        {
            nodeSpawned = new GameObject[sceneData[index].nodeData.Length];
            sceneNodeSpawned[index] = Instantiate(sceneNodePrefab, nodeSpawnPoint.transform.position, Quaternion.identity, nodeSpawnPoint.transform);
            sceneNodeSpawnedAudioSource[index] = sceneNodeSpawned[index].GetComponent<AudioSource>();
            sceneNodeSpawned[index].GetComponent<SceneDisplay>().SceneLoad(sceneData[index]);
            sceneNodeSpawned[index].name = sceneData[index].sceneName;

            // Created the information nodes related to the scene nodes
            for (int secondIndex = 0; secondIndex <sceneData[index].nodeData.Length; secondIndex++)
            {
                nodeSpawned[secondIndex] = Instantiate(informationNodePrefab, NodePositionPlacement(sceneData[index].nodeData[secondIndex]), Quaternion.identity, sceneNodeSpawned[index].transform);
                NodeRotationPlacement(sceneData[index].nodeData[secondIndex], nodeSpawned[secondIndex]);
                nodeSpawned[secondIndex].GetComponent<NodeDisplay>().NodeLoad(sceneData[index].nodeData[secondIndex]);
            }
            nodeSpawned = GameObject.FindGameObjectsWithTag("InformationNode");
        }
    }

    public void LoadScene(string newButtonName)
    {
        // Loads the scene and its data from a button press
        for (int index = 0; index < sceneNodeSpawned.Length; index++)
        {
            if (newButtonName == sceneNodeSpawned[index].name)
            {
                sceneNodeSpawned[index].SetActive(true);
                SkyboxSwap(sceneData[index]);
                PlayNarration(sceneData[index], sceneNodeSpawnedAudioSource[index]);
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

    void PlayNarration(SceneData newSceneData, AudioSource newSceneAudioSource)
    {
        // Plays audio if a clip exists and the audiosource is not currently playing 
        if (newSceneData.sceneAudio != null)
        {
            if (!newSceneAudioSource.isPlaying)
            {
                newSceneAudioSource.PlayOneShot(newSceneData.sceneAudio);
            }
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