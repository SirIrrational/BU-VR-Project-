using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material[] panoramaMaterials;
    public GameObject vrRig;

    Skybox skybox;

    void Awake()
    {
        skybox = vrRig.GetComponentInChildren<Skybox>();
    }

    public void SkyboxSwitch(int _skyboxState)
    {
        skybox.material = panoramaMaterials[_skyboxState];
    }

}
