using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TourButton : MonoBehaviour
{
    SceneController sceneController;
    public SceneData sceneData;
    Text text;
    Image[] images;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        text = gameObject.GetComponentInChildren<Text>();
        text.text = sceneData.sceneName;
        images = gameObject.GetComponentsInChildren<Image>();
        images[1].sprite = sceneData.sceneButtonImage;
        gameObject.name = sceneData.sceneName;
    }

    public void LoadScene()
    {
        sceneController.LoadScene(gameObject.GetComponent<TourButton>().sceneData);
    }
}
