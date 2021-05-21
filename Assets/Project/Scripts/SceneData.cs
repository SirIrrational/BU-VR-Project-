using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Node", menuName = "Scene Node")]
public class SceneData : ScriptableObject
{
    public string sceneName;
    public Texture2D panoramaImage;
    public AudioClip sceneAudio;
    public NodeData[] nodeData;
}
