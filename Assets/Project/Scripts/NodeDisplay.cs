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
        title.text = nodeData.nodeTitle;
        information.text = nodeData.nodeInformation;
    }

    public void ActivateNode()
    {
        node.SetActive(true);
    }

    public void DeactiveNode()
    {
        node.SetActive(false);
        audioSource.Stop();
    }
}
