using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "360 Image", menuName = "360 Image")]
public class SceneData : ScriptableObject
{
    public Material panoramaMaterial;
    public AudioClip sceneNarration;
}
