using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public int squareScore;
    public int rectangleShotScore;

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
            GameController.squareScore += squareScore;
            GameController.squaresPresent -= 1;
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        if (collider.tag == "Rectangle")
        {
            GameController.squareScore -= rectangleShotScore;
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
