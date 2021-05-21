﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Node Data", menuName = "Node Data")]
public class NodeData : ScriptableObject
{
    public string title;
    public string information;
    public AudioClip nodeAudio;
    [Range (1f, 6f)] public float nodeDistance;
    [Range(0f, 359f)] public float nodeOrbitLocation;
}



