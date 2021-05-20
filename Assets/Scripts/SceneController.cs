using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static GameObject[] sceneNodes;
    public static AudioSource[] sceneAudioSources;

    private void Awake()
    {
        sceneNodes = GameObject.FindGameObjectsWithTag("SceneNode");

        for (int a = 0; a <= sceneNodes.Length - 1; a++)
        {
            sceneAudioSources[a] = sceneNodes[a].GetComponent<AudioSource>();
            sceneNodes[a].SetActive(false);
        }

        sceneNodes[0].SetActive(true);
    }
}
