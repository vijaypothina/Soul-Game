using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoundScripts : MonoBehaviour
{
    [SerializeField] GameObject Plant;
    private bool inRange, isGrowing;
    private int Seeds;
    private PlayerMovement PM;

    private void Start()
    {
        GameObject go_Player = GameObject.Find("Player");
        PM = go_Player.GetComponent<PlayerMovement>();
    }

    private void CheckWait()
    {
        PM.CheckInventory();
    }

    private void Update()
    {
        if (inRange && Input.GetButtonDown("Fire1"))
        {
            Seeds = PlayerPrefs.GetInt("Seeds", Seeds);
            if (Seeds > 0)
            {
                Seeds--;
                PlayerPrefs.SetInt("Seeds", Seeds);
                Invoke("CheckWait", .1f);
                GrowPlant();
            }
        }
    }

    private void GrowPlant()
    {
        inRange = false;
        Instantiate(Plant, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isGrowing)
                inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
