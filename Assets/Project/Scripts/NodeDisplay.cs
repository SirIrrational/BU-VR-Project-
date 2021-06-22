using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeDisplay : MonoBehaviour
{
    NodeData nodeData;
    public Text title;
    public Text information;
    public GameObject node;
    AudioSource audioSource;
    AudioClip audioClip;
    GameObject sceneNodeSpawnPoint;
    Canvas canvas;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneNodeSpawnPoint = GameObject.FindGameObjectWithTag("NodeSpawnPoint");
        gameObject.transform.LookAt(sceneNodeSpawnPoint.transform.position);
        gameObject.GetComponentInChildren<Canvas>().worldCamera = Camera.FindObjectOfType<Camera>();
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
            if (SceneController.audioActive)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(nodeData.nodeAudio);
                }
            }
        }
        node.SetActive(true);
    }

    public void DeactiveNode()
    {
        node.SetActive(false);
        audioSource.Stop();
    }
}
