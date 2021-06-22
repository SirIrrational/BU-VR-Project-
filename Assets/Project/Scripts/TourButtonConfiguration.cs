using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourButtonConfiguration : MonoBehaviour
{
    public Color normalColour;
    public Color highlightColour;
    Image[] images;
    Text text;
    bool colourActive = false;
    public int buttonIndex;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = GetComponentInChildren<Text>().text;
        text = gameObject.GetComponentInChildren<Text>();
        images = gameObject.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        TourToggle();
    }

    public void HighlightedColour()
    {
        images[0].color = highlightColour;
        images[1].color = highlightColour;
        images[2].color = highlightColour;
        text.color = normalColour;
    }

    public void NormalColour()
    {
        images[0].color = normalColour;
        images[1].color = normalColour;
        images[2].color = normalColour;
        text.color = highlightColour;
    }

    void TourToggle()
    {
        if (buttonIndex == SceneController.activeScene)
        {
            HighlightedColour();
            colourActive = false;
        }
        else
        {
            if (!colourActive)
            {
                NormalColour();
                colourActive = true;
            }
        }
    }
}
