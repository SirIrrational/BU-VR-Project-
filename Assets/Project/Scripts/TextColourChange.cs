using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColourChange : MonoBehaviour
{
    public bool thumbnailText = false;

    Color highlightedColour;
    Color pressedColour;
    Color normalColour;
    Color textColour;
    Text text;

    void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
        highlightedColour = gameObject.GetComponent<Button>().colors.highlightedColor;
        pressedColour = gameObject.GetComponent<Button>().colors.pressedColor;
        normalColour = gameObject.GetComponent<Button>().colors.normalColor;
        textColour = gameObject.GetComponentInChildren<Text>().color;
    }

    public void HighlightedColour()
    {
        text.color = highlightedColour;
    }

    public void PressedColour()
    {
        text.color = pressedColour;
    }

    public void NormalColour()
    {
        if (thumbnailText)
        {
            text.color = textColour;
        }
        else
        {
            text.color = normalColour;
        }
    }
}
