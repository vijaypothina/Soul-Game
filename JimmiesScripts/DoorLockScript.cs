using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockScript : MonoBehaviour
{
    [SerializeField] private bool isLocked, isAutomatic;

    private bool inRange;
    private float DoorOpen, DoorTimer = 5f;

    private Animator anim;
    private AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        DoorOpen = DoorTimer;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    private void Update()
    {
        if (inRange && !isLocked)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                AS.Play();
                if (anim.GetBool("IsOpen") == false)
                    anim.SetBool("IsOpen", true);
                else
                    anim.SetBool("IsOpen", false);
            }

            if (isAutomatic)
            {
                if (anim.GetBool("IsOpen") == false)
                {
                    AS.Play();
                    anim.SetBool("IsOpen", true);
                }
            }
        }
        if (anim.GetBool("IsOpen") && !inRange)
        {
            DoorOpen -= Time.deltaTime;
            if (DoorOpen < 0)
            {
                AS.Play();
                anim.SetBool("IsOpen", false);
                DoorOpen = DoorTimer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            inRange = false;
    }
}
