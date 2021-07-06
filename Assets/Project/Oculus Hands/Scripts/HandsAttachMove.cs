using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAttachMove : MonoBehaviour
{
    GameObject leftAttach;
    GameObject rightAttach;
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    void Start()
    {
        StartCoroutine(MoveDelay());
    }

    void MoveAttach()
    {
        leftAttach = GameObject.Find("[Left Hand] Attach");
        rightAttach = GameObject.Find("[Right Hand] Attach");

        leftAttach.transform.position = leftAttachTransform.position;
        rightAttach.transform.position = rightAttachTransform.position;
    }


    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1f);
        MoveAttach();
    }
}
