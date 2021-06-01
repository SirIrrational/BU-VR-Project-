﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeDisplay : MonoBehaviour
{
    NodeData nodeData;
    public Text title;
    public Text information;
    public GameObject canvas;
    AudioSource audioSource;
    AudioClip audioClip;
    GameObject sceneNodeSpawnPoint;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneNodeSpawnPoint = GameObject.FindGameObjectWithTag("NodeSpawnPoint");
        gameObject.transform.LookAt(sceneNodeSpawnPoint.transform.position);
    }

    void Update()
    {

    }

    public void NodeLoad(NodeData newNodeData)
    {
        nodeData = newNodeData;
        title.text = nodeData.title;
        information.text = nodeData.information;
        audioClip = nodeData.nodeAudio;
    }

    public void ActivateNode()
    {
        if (audioClip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(nodeData.nodeAudio);
            }
        }
        canvas.SetActive(true);
    }

    public void DeactiveNode()
    {
        canvas.SetActive(false);
        audioSource.Stop();
    }
}