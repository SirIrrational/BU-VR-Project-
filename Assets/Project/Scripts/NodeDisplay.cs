using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeDisplay : MonoBehaviour
{
    NodeData nodeData;
    public Text title;
    public Text information;
    public GameObject canvas;
    public MeshRenderer meshRenderer;
    [Range(0f, 1f)] public float lerpTime = 0;
    public Color[] colours;
    AudioSource audioSource;
    AudioClip audioClip;
    float t = 0f;
    int colourIndex = 0;
    GameObject sceneNodeSpawnPoint;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sceneNodeSpawnPoint = GameObject.FindGameObjectWithTag("NodeSpawnPoint");
        gameObject.transform.LookAt(sceneNodeSpawnPoint.transform.position);
    }

    void Update()
    {
        ColourLerp();
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

    void ColourLerp()
    {
        meshRenderer.materials[0].color = Color.Lerp(meshRenderer.material.color, colours[colourIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > 0.9f)
        {
            t = 0f;
            colourIndex++;
            colourIndex = (colourIndex >= colours.Length) ? 0 : colourIndex;
        }
    }
}
