using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDisplay : MonoBehaviour
{
    public GameObject vrRig;
    public SceneData sceneData;
    Skybox skybox;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void LoadScene()
    {
        for (int a = 0; a <= SceneController.sceneNodes.Length - 1; a++)
        {
            SceneController.sceneNodes[a].SetActive(false);
        }

        gameObject.SetActive(true);
        skybox = vrRig.GetComponentInChildren<Skybox>();
        skybox.material.SetTexture("_MainTex", sceneData.panoramaTexture);

        if (sceneData.sceneNarration != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sceneData.sceneNarration);
            }
            else
            {
                Debug.Log(gameObject.name + " needs scene narration");
            }
        }
    }

}
