using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LeftHand : MonoBehaviour
{
    public GameObject leftHand;
    GameObject spawnedLeftHand;

    Animator leftHandAnimator;

    InputDevice targetDevice;

    void Start()
    {
        HandSpawn();
    }


    void Update()
    {
        HandFunction();
    }

    void HandSpawn()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        targetDevice = devices[0];
        spawnedLeftHand = Instantiate(leftHand, transform);
        leftHandAnimator = spawnedLeftHand.GetComponent<Animator>();
    }

    void HandFunction()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            leftHandAnimator.SetFloat("trigger", triggerValue);
        }
        else
        {
            leftHandAnimator.SetFloat("trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            leftHandAnimator.SetFloat("grip", gripValue);
        }
        else
        {
            leftHandAnimator.SetFloat("grip", 0);
        }
    }
}
