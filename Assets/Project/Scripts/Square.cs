using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    GameController gameController;
    AudioSource audioSource;
    Color tempColour;
    MeshRenderer meshRenderer;
    public MeshRenderer iconMeshRenderer;
    float[] lerpTimer = { 0, 0, 0 };
    public Color[] levelColours;
    public Texture2D[] levelOneImages;
    public Texture2D[] levelTwoImages;
    public Texture2D[] levelThreeImages;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        audioSource = gameController.GetComponent<AudioSource>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        IconSet();
    }

    void Update()
    {
        Movement();
        ObjectColour();
        LerpTimer();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bounds")
        {
            GameController.squaresPresent -= 1;
            Destroy(gameObject);
        }
        if (collider.tag == "Projectile")
        {
            audioSource.PlayOneShot(gameController.squareAudioClip);
        }
    }

    void Movement()
    {
        gameObject.transform.Translate(Vector3.back * GameController.objectSpeed * Time.deltaTime, Space.World);
    }

    void ObjectColour()
    {  
        tempColour = meshRenderer.materials[0].color;

        void Emissive(Color newColour)
        {
            meshRenderer.materials[0].SetColor("_EmissionColor", newColour * 2.1f);
        }
        Color ColourLerp(Color originalColour, Color newColour, float lerpTimer)
        {
            return Color.Lerp(originalColour, newColour, lerpTimer);
        }
        switch (GameController.levelState)
        {
            case 0:
                Emissive(ColourLerp(tempColour, levelColours[0], lerpTimer[0]));
                lerpTimer[1] = 0;
                lerpTimer[2] = 0;
                break;
            case 1:
                Emissive(ColourLerp(tempColour, levelColours[1], lerpTimer[1]));
                lerpTimer[0] = 0;
                lerpTimer[2] = 0;
                break;
            case 2:
                Emissive(ColourLerp(tempColour, levelColours[2], lerpTimer[2]));
                lerpTimer[0] = 0;
                lerpTimer[1] = 0;
                break;
        }      
    }

    void LerpTimer()
    {
        switch (GameController.levelState)
        {
            case 0:
                lerpTimer[0] += Time.deltaTime;
                break;
            case 1:
                lerpTimer[1] += Time.deltaTime;
                break;
            case 2:
                lerpTimer[2] += Time.deltaTime;
                break;
        }
    }

    void IconSet()
    {
        void TextureLoop(Texture2D[] newTextures)
        {
            //iconMeshRenderer.material.SetTexture("_MainTex", TextureSet(newTextures));
            iconMeshRenderer.materials[0].mainTexture = TextureSet(newTextures);
        }
        Texture2D TextureSet(Texture2D[] newTextures)
        {
            int index = Random.Range(0, newTextures.Length);
            return newTextures[index];
        }
        switch (GameController.levelState)
        {
            case 0:
                TextureLoop(levelOneImages);
                break;
            case 1:
                TextureLoop(levelTwoImages);
                break;
            case 2:
                TextureLoop(levelThreeImages);
                break;   
        }
    }
}
