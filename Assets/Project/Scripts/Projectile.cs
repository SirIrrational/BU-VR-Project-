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
        if (collider.tag == "Square")
        {
            GameController.squareScore += 10;
            GameController.squaresPresent -= 1;
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        if (collider.tag == "Rectangle")
        {
            GameController.squareScore -= 5;
            Destroy(gameObject);
        }
        if (collider.tag == "Scene")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
