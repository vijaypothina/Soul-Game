using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneTreeScript : MonoBehaviour
{
    private Animator anim;
    public bool isComplete;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (isComplete)
            anim.SetBool("Complete", true);
    }

    public void OpenDoor()
    {
        anim.SetBool("IsOpen", true);
    }
}
