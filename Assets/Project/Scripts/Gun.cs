using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public GameObject barrelPivot;
    public Transform projectileSpawnTransform;
    public AudioClip fireAudioClip;
    AudioSource audioSource;
    Animator animator;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        BarrelRotation();
    }

    public void Shoot()
    {
        GameController.shotsFired += 1;
        GameController.sphereScore -= 1;
        animator.Play("Gun Fire");
        audioSource.PlayOneShot(fireAudioClip);
        GameObject spawnedGameObject = Instantiate(projectile, projectileSpawnTransform);
        spawnedGameObject.transform.SetParent(null);
    }

    void BarrelRotation()
    {
        barrelPivot.transform.Rotate(Vector3.left * 300 * Time.deltaTime);
    }
}
