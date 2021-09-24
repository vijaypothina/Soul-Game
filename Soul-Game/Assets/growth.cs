using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growth : MonoBehaviour
{
    private int stage=0;
    public int delta;
    public int max=3;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("growing", delta, delta);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void growing()
    {
        if (stage != max)
        {
            gameObject.transform.GetChild(stage).gameObject.SetActive(true);
        }
        if (stage>0 && stage<max)
        {
            gameObject.transform.GetChild(stage - 1).gameObject.SetActive(false);

        }
        if (stage < max)
        {
            stage++;
        }
    }
}
