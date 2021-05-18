using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Information Node", menuName = "Information Node")]
public class InformationNode : ScriptableObject
{
    public string title;
    public string information;
    public AudioClip narration;
}
