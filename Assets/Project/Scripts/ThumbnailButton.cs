using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThumbnailButton : MonoBehaviour
{
    SceneController sceneController;
    public SceneData sceneData;
    Text text;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        text = gameObject.GetComponentInChildren<Text>();
        text.text = sceneData.sceneName;
        gameObject.GetComponent<Image>().sprite = sceneData.sceneThumbnailImage;
        gameObject.name = sceneData.sceneName;
    }

    public void LoadScene()
    {
        sceneController.LoadScene(gameObject.GetComponent<ThumbnailButton>().sceneData);
    }
}
