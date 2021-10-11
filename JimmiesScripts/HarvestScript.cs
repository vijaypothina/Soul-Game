using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestScript : MonoBehaviour
{
    private bool inRange;

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Fire1"))
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inRange = false;
    }
}
