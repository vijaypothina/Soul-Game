using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public bool isKey, isPanel;
    private bool inRange;
    [SerializeField] private Text text;
    [SerializeField] private DoorLockScript DLS;

    private void Start()
    {
        if (!isKey)
        {
            text = text.GetComponent<Text>();
            text.text = "Door is locked.";
        }

        DLS = DLS.GetComponent<DoorLockScript>();
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                text.text = "Door is now unlocked.";
                DLS.UnlockDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isKey)
            {
                DLS.UnlockDoor();
                gameObject.SetActive(false);
            }
            else if (isPanel)
                inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isPanel)
                inRange = false;
        }
    }
}
