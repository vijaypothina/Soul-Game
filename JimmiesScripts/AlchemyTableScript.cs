using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTableScript : MonoBehaviour
{
    private PlayerMovement PM;

    private int Potion01, Potion02, Apple, Book, Leaf, Flower, Seeds;
    private int al_Apple, al_Leaf, al_Flower;
    [SerializeField] Text t_Apple, t_Leaf, t_Flower, t_Seeds;
    [SerializeField] Text alt_Apple, alt_Leaf, alt_Flower, alt_Seeds, alt_Potion1, alt_Potion2;
    [SerializeField] GameObject go_Potion01, go_Potion02, go_Apple, go_Leaf, go_Flower, go_Seeds;

    [SerializeField] GameObject altgo_Potion01, altgo_Potion02, altgo_Apple, altgo_Leaf, altgo_Flower, altgo_Seeds;

    [SerializeField] GameObject AlchemyButtonActive, AlchemyButtonInactive;

    [SerializeField] Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
            Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
            Apple = PlayerPrefs.GetInt("Apple", Apple);
            Book = PlayerPrefs.GetInt("Book", Book);
            Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
            Flower = PlayerPrefs.GetInt("Flower", Flower);
            Seeds = PlayerPrefs.GetInt("Seeds", Seeds);

            t_Apple.text = Apple.ToString();
            t_Leaf.text = Leaf.ToString();
            t_Flower.text = Flower.ToString();
            t_Seeds.text = Seeds.ToString();
            alt_Leaf.text = "";
            alt_Flower.text = "";
            alt_Apple.text = "";
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";

            if (Leaf == 0)
                go_Leaf.SetActive(false);
            else
                go_Leaf.SetActive(true);
            if (Apple == 0)
                go_Apple.SetActive(false);
            else
                go_Apple.SetActive(true);
            if (Flower == 0)
                go_Flower.SetActive(false);
            else
                go_Flower.SetActive(true);
            if (Seeds == 0)
                go_Seeds.SetActive(false);
            else
                go_Seeds.SetActive(true);
        }
    }

    private void Start()
    {
        GameObject go_Player = GameObject.Find("Player");
        PM = go_Player.GetComponent<PlayerMovement>();

        altgo_Apple.SetActive(false);
        altgo_Flower.SetActive(false);
        altgo_Leaf.SetActive(false);
        altgo_Potion01.SetActive(false);
        altgo_Potion02.SetActive(false);
        altgo_Seeds.SetActive(false);

        anim = anim.GetComponent<Animator>();

        Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
        Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
        Apple = PlayerPrefs.GetInt("Apple", Apple);
        Book = PlayerPrefs.GetInt("Book", Book);
        Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
        Flower = PlayerPrefs.GetInt("Flower", Flower);
        Seeds = PlayerPrefs.GetInt("Seeds", Seeds);

        t_Apple = t_Apple.GetComponent<Text>();
        t_Leaf = t_Leaf.GetComponent<Text>();
        t_Flower = t_Flower.GetComponent<Text>();
        t_Seeds = t_Seeds.GetComponent<Text>();
        alt_Leaf = alt_Leaf.GetComponent<Text>();
        alt_Flower = alt_Flower.GetComponent<Text>();
        alt_Apple = alt_Apple.GetComponent<Text>();

        alt_Seeds = alt_Seeds.GetComponent<Text>();
        alt_Potion1 = alt_Potion1.GetComponent<Text>();
        alt_Potion2 = alt_Potion2.GetComponent<Text>();

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        alt_Leaf.text = "";
        alt_Flower.text = "";
        alt_Apple.text = "";
        alt_Seeds.text = "";
        alt_Potion1.text = "";
        alt_Potion2.text = "";
    }

    public void useApple()
    {
        if (Apple > 0)
        {
            Apple--;
            al_Apple++;

            al_Leaf = 0;
            Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
        }
        altgo_Apple.SetActive(true);

        if (altgo_Leaf.activeInHierarchy)
            altgo_Leaf.SetActive(false);

        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple==1) 
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    public void useLeaf()
    {
        if (Leaf > 0)
        {
            Leaf--;
            al_Leaf++;
            altgo_Leaf.SetActive(true);

            al_Apple = 0;
            Apple = PlayerPrefs.GetInt("Apple", Apple);
        }
        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (altgo_Apple.activeInHierarchy)
            altgo_Apple.SetActive(false);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple == 1)
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    public void useFlower()
    {
        if (Flower > 0)
        {
            Debug.Log(Flower);
            Flower--;
            al_Flower++;
        }
        Debug.Log(Flower);
        altgo_Flower.SetActive(true);
        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple == 1)
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    public void UnuseApple()
    {
            Apple++;
            al_Apple--;
        
        if (al_Apple == 0)
            altgo_Apple.SetActive(false);

        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple == 1)
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    public void UnuseLeaf()
    {
            Leaf++;
            al_Leaf--;
        
        if (al_Leaf == 0)
            altgo_Leaf.SetActive(false);
        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple == 1)
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    public void UnuseFlower()
    {
            Flower++;
            al_Flower--;
        
        if (al_Flower == 0)
            altgo_Flower.SetActive(false);
        if (Leaf == 0)
            go_Leaf.SetActive(false);
        else
            go_Leaf.SetActive(true);
        if (Apple == 0)
            go_Apple.SetActive(false);
        else
            go_Apple.SetActive(true);
        if (Flower == 0)
            go_Flower.SetActive(false);
        else
            go_Flower.SetActive(true);

        if (al_Flower == 0 && al_Leaf == 0 && al_Apple == 1)
        {
            altgo_Seeds.SetActive(true);
            altgo_Potion01.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "5";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 2 && al_Leaf == 1 && al_Apple == 0)
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(true);
            alt_Seeds.text = "";
            alt_Potion1.text = "1";
            alt_Potion2.text = "";
        }
        else if (al_Flower == 1 && al_Leaf == 0 && al_Apple == 2)
        {
            altgo_Potion01.SetActive(true);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "1";
        }
        else
        {
            altgo_Potion01.SetActive(false);
            altgo_Seeds.SetActive(false);
            altgo_Potion02.SetActive(false);
            alt_Seeds.text = "";
            alt_Potion1.text = "";
            alt_Potion2.text = "";
        }

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        if (al_Leaf > 0)
            alt_Leaf.text = al_Leaf.ToString();
        else
            alt_Leaf.text = "";
        if (al_Flower > 0)
            alt_Flower.text = al_Flower.ToString();
        else
            alt_Flower.text = "";
        if (al_Apple > 0)
            alt_Apple.text = al_Apple.ToString();
        else
            alt_Apple.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (al_Apple == 0 && al_Leaf == 0 & al_Flower == 0)
            {
                Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
                Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
                Apple = PlayerPrefs.GetInt("Apple", Apple);
                Book = PlayerPrefs.GetInt("Book", Book);
                Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
                Flower = PlayerPrefs.GetInt("Flower", Flower);
                Seeds = PlayerPrefs.GetInt("Seeds", Seeds);
            }
        }
        if (Book > 0)
        {
            if ((al_Flower == 2 && al_Leaf == 1) || (al_Apple == 1) || (al_Apple == 2 && al_Flower == 1))
            {
                AlchemyButtonActive.SetActive(true);
                AlchemyButtonInactive.SetActive(false);
            }
            else
            {
                AlchemyButtonInactive.SetActive(true);
                AlchemyButtonActive.SetActive(false);
            }
        }
    }

    public void OpenTable()
    {
        al_Flower = 0;
        al_Leaf = 0;
        al_Apple = 0;

        Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
        Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
        Apple = PlayerPrefs.GetInt("Apple", Apple);
        Book = PlayerPrefs.GetInt("Book", Book);
        Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
        Flower = PlayerPrefs.GetInt("Flower", Flower);
        Seeds = PlayerPrefs.GetInt("Seeds", Seeds);

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        alt_Leaf.text = "";
        alt_Flower.text = "";
        alt_Apple.text = "";
    }

    public void CheckAlchemy()
    {
        if (al_Flower==2 && al_Leaf == 1)
        {
            Potion02++;

            PlayerPrefs.SetInt("Potion01", Potion01);
            PlayerPrefs.SetInt("Potion02", Potion02);
            PlayerPrefs.SetInt("Apple", Apple);
            PlayerPrefs.SetInt("Book", Book);
            PlayerPrefs.SetInt("Leaf", Leaf);
            PlayerPrefs.SetInt("Flower", Flower);
            PlayerPrefs.SetInt("Seeds", Seeds);

            anim.SetBool("LightOn", true);
            Invoke("ResetAnim", .5f);
        }
        else if (al_Apple == 1)
        {
            Seeds += 5;

            PlayerPrefs.SetInt("Potion01", Potion01);
            PlayerPrefs.SetInt("Potion02", Potion02);
            PlayerPrefs.SetInt("Apple", Apple);
            PlayerPrefs.SetInt("Book", Book);
            PlayerPrefs.SetInt("Leaf", Leaf);
            PlayerPrefs.SetInt("Flower", Flower);
            PlayerPrefs.SetInt("Seeds", Seeds);

            anim.SetBool("LightOn", true);
            Invoke("ResetAnim", .5f);
        }
        else if (al_Apple == 2 && al_Flower == 1)
        {
            Potion01++;

            PlayerPrefs.SetInt("Potion01", Potion01);
            PlayerPrefs.SetInt("Potion02", Potion02);
            PlayerPrefs.SetInt("Apple", Apple);
            PlayerPrefs.SetInt("Book", Book);
            PlayerPrefs.SetInt("Leaf", Leaf);
            PlayerPrefs.SetInt("Flower", Flower);
            PlayerPrefs.SetInt("Seeds", Seeds);

            anim.SetBool("LightOn", true);
            Invoke("ResetAnim", .5f);
        }

        al_Flower = 0;
        al_Leaf = 0;
        al_Apple = 0;

        t_Apple.text = Apple.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        alt_Leaf.text = "";
        alt_Flower.text = "";
        alt_Apple.text = "";

        alt_Seeds.text = "";
        alt_Potion1.text = "";
        alt_Potion2.text = "";

        altgo_Apple.SetActive(false);
        altgo_Flower.SetActive(false);
        altgo_Leaf.SetActive(false);
        altgo_Potion01.SetActive(false);
        altgo_Potion02.SetActive(false);
        altgo_Seeds.SetActive(false);

        PM.CheckInventory();
    }

    private void ResetAnim()
    {
        anim.SetBool("LightOn", false);
    }

    public void CloseAlchemy()
    {
        PM.CloseAlchemy();
    }
}
