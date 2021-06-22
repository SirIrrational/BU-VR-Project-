using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonConfiguration : MonoBehaviour
{
    Sprite audioOriginalSprite;
    public Color normalColour;
    public Color highlightColour;
    public Sprite audioMuteSprite;
    Image[] images;
    Text text;
    bool colourActive = false;

    void Start()
    {
        gameObject.name = GetComponentInChildren<Text>().text;
        text = gameObject.GetComponentInChildren<Text>();
        images = gameObject.GetComponentsInChildren<Image>();
        audioOriginalSprite = images[1].sprite;
    }

    void Update()
    {
        switch (gameObject.tag)
        {
            case "Audio":
                ToggleAudio(SceneController.audioActive);
                break;
            case "Tour":
                Toggle(SceneController.tourUIActive);
                break;
            case "Info":
                Toggle(SceneController.infoUIActive);
                break;
        }
    }

    public void HighlightedColour()
    {
        images[0].color = highlightColour;
        images[1].color = normalColour;
        text.color = normalColour;
    }

    public void NormalColour()
    {
        images[0].color = normalColour;
        images[1].color = highlightColour;
        text.color = highlightColour;
    }

    void Toggle(bool newBool)
    {
        if (newBool)
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

    void ToggleAudio(bool newBool)
    {
        if (!newBool)
        {
            images[1].sprite = audioMuteSprite;
        }
        else
        {
            images[1].sprite = audioOriginalSprite;
        }
    }
}
