using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationNodeDisplay : MonoBehaviour
{
    public InformationNode informationNode;

    public Text title;
    public Text information;

    void Start()
    {
        title.text = informationNode.title;
        information.text = informationNode.information;
    }

}
