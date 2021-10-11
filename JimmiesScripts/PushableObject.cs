using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    bool isHolding;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            isHolding = true;
        else if (Input.GetButtonUp("Fire1"))
        {
            transform.parent = null;
            isHolding = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isHolding)
                transform.parent = other.transform;
            else
                transform.parent = null;
        }
    }
}
