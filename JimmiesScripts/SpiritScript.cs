using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : MonoBehaviour
{
    [SerializeField] GameObject[] PatrolPoint;
    private int CurPoint;
    public float Speed;

    public bool isSoul;

    public float ManaCost;

    private PlayerMovement PM;

    [SerializeField] GameObject Plushie;

    private void Start()
    {
        if (!isSoul)
        {
            GameObject go_Player = GameObject.Find("Player");
            PM = go_Player.GetComponent<PlayerMovement>();
        }

        if (isSoul)
            Destroy(gameObject, 4);
    }

    private void Update()
    {
        if (!isSoul)
            transform.position = Vector3.Lerp(transform.position, PatrolPoint[CurPoint].transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Patrol")
        {
            CurPoint++;
            if (CurPoint - 1 > PatrolPoint.Length)
                CurPoint = 0;
        }

        if (other.gameObject.tag == "Attack")
        {
            Instantiate(Plushie, transform.position, transform.rotation);
            PM.DepleteMana(ManaCost);
            Destroy(gameObject);
        }
    }
}
