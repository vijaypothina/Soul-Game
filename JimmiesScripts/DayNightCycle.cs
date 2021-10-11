using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Rotate(Speed * Time.deltaTime, 0, 0);
    }
}
