using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Data", menuName = "Scene Data")]
public class SceneData : ScriptableObject
{
    public string sceneName;
    public string sceneDescription;
    public Sprite sceneButtonImage;
    public Texture2D scenePanoramaImage;
    public AudioClip sceneAudio;
    public NodeData[] nodeData;
}
