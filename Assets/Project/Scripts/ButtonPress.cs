using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    SceneController sceneController;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    public void LoadScene()
    {
        sceneController.LoadScene(gameObject.GetComponentInChildren<Text>().text);
    }

    public void MuteAudio()
    {
        sceneController.MuteAudio();
    }
}
