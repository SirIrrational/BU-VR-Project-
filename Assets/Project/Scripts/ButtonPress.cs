using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{

    public void LoadScene()
    {
        FindObjectOfType<SceneController>().LoadScene(gameObject.GetComponentInChildren<Text>().text);
    }
}
