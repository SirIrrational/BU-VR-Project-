using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationNodeDisplay : MonoBehaviour
{
    public InformationNode informationNode;
    public Text title;
    public Text information;
    public GameObject vrRig;
    public MeshRenderer meshRenderer;
    [SerializeField] [Range(0f, 1f)] float lerpTime = 0;
    float t = 0f;
    public Color[] colours;
    int colourIndex = 0;
    AudioSource audioSource;
    public GameObject canvas;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        title.text = informationNode.title;
        information.text = informationNode.information;
        gameObject.transform.LookAt(vrRig.transform.position);
    }

    void Update()
    {
        ColourLerp();
    }

    void ColourLerp()
    {
        meshRenderer.materials[0].color = Color.Lerp(meshRenderer.material.color, colours[colourIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t> 0.9f)
        {
            t = 0f;
            colourIndex++;
            colourIndex = (colourIndex >= colours.Length) ? 0 : colourIndex;
        }
    }

    public void ActivateNode()
    {
        if (informationNode.narration != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(informationNode.narration);
            }
        }
        else
        {
            Debug.Log(gameObject.name + " needs node narration");
        }

        canvas.SetActive(true);
    }

    public void DeactiveNode()
    {
        canvas.SetActive(false);
    }

}
