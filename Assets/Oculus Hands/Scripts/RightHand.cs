using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHand : MonoBehaviour
{
    public GameObject rightHand;
    GameObject spawnedRightHand;

    Animator rightHandAnimator;

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
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        targetDevice = devices[0];
        spawnedRightHand = Instantiate(rightHand, transform);
        rightHandAnimator = spawnedRightHand.GetComponent<Animator>();
    }

    void HandFunction()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            rightHandAnimator.SetFloat("trigger", triggerValue);
        }
        else
        {
            rightHandAnimator.SetFloat("trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            rightHandAnimator.SetFloat("grip", gripValue);
        }
        else
        {
            rightHandAnimator.SetFloat("grip", 0);
        }
    }
}
