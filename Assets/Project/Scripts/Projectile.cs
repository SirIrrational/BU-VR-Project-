using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Sphere")
        {
            GameController.sphereScore += 10;
            GameController.spheresPresent -= 1;
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

        if (collider.tag == "Square")
        {
            GameController.sphereScore -= 10;
            Destroy(gameObject);
        }

        if (collider.tag == "Scene")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
