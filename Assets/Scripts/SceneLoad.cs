using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    GameObject vrRig;
    public SceneData sceneData;

    Skybox skybox;

    public void LoadScene()
    {
        vrRig = GameObject.FindGameObjectWithTag("VRRig");
        skybox = vrRig.GetComponentInChildren<Skybox>();
        skybox.material = sceneData.panoramaMaterial;
    }

}
