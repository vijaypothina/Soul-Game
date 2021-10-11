using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //
    // Debug Testing DO NOT USE.....please
    //
    public bool ResetPlayerData;

    [SerializeField] IntroSceneTreeScript ISTS;

    //
    // Player Movement
    //

    [SerializeField] CharacterController CharCont;
    [SerializeField] CinemachineVirtualCamera CVC;
    private AudioSource AS;
    [SerializeField] AudioClip ScytheSwing;

    private Vector2 MouseTurn, ControllerTurn;

    public float TurnSensitivity, TurnSensitivityY;
    public Transform groundCheck, PushCheck;
    public float groundDistance = 0.4f, PushDistance = 0.4f;
    public LayerMask groundMask, PushMask;

    private Vector3 playerVelocity, CharacterMovement, CharacterMovementX;
    private bool isGrounded, isAbleToPush, isJumping, isAttacking;

    private bool Invincible;
    private float InvinTimer;
    public float InvincibleTimer;

    public float JumpHeight;
    public float DamageSpeed;

    public float playerSpeed;
    public float Gravity;

    [SerializeField] private GameObject FootSteps;

    //
    // Plushies
    //

    [SerializeField] GameObject FoxGhost, FoxSoul;
    private bool inFoxGhostRange;
    [SerializeField] GameObject soulInstantiate;

    //
    // Player Animations
    //
    private string CurAnimation;
    [SerializeField] Animator anim;

    const string Idle = "Idle";
    const string Run = "Run";
    const string Attack2 = "Attack02";
    const string Attack1 = "Attack1";
    const string Landing = "Landing";
    const string InAir = "InAir";
    const string Pushing = "Pushing";
    const string PushingIdle = "PushingIdle";
    const string PushStart = "PushStart";
    const string PushEnd = "PushEnd";
    const string Hurt = "Hurt";
    const string Death = "Death";
    const string Dancing = "Dancing";
    const string Jump = "Jump";

    //
    // Pause Menu
    //

    private bool isPaused, isInventory, isAlchemy, isDead;
    [SerializeField] GameObject PauseMenu, Inventory;

    //
    // Inventory
    //

    [SerializeField] GameObject Potion01Active, Potion01Inactive, Potion02Active, Potion02Inactive, AppleActive, AppleInactive, BookActive, BookInactive, LeafActive, LeafInactive, FlowerActive, FlowerInactive, SeedsActive, SeedsInactive, FoxPlushieActive, FoxPlushieInactive;

    private float Mana;
    [SerializeField] Slider ManaSlider;

    private int Potion01, Potion02, Apple, Book, Leaf, Flower, Seeds, FoxPlushie;
    [SerializeField] Text t_Potion01, t_Potion02, t_Apple, t_Book, t_Leaf, t_Flower, t_Seeds, t_FoxPlushie;

    public void DepleteMana(float Cost)
    {
        Mana -= Cost;
        ManaSlider.value = Mana / 100;
        if (Mana <= 0)
        {
            isDead = true;
            ChangeAnimations(Death);
            isDead = true;
        }
        else
        {
            PlayerPrefs.SetFloat("Mana", Mana);
        }
    }

    private void Start()
    {
        //Debug Settings
        if (ResetPlayerData)
            PlayerPrefs.DeleteAll();

        if (ISTS != null)
            ISTS = ISTS.GetComponent<IntroSceneTreeScript>();

        ManaSlider = ManaSlider.GetComponent<Slider>();
        Mana = PlayerPrefs.GetFloat("Mana", Mana);
        ManaSlider.value = Mana / 100;
        if (Mana == 0)
        {
            Mana = 1;
            PlayerPrefs.SetFloat("Mana", Mana);
        }
        // Player Inventory
        Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
        Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
        Apple = PlayerPrefs.GetInt("Apple", Apple);
        Book = PlayerPrefs.GetInt("Book", Book);
        Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
        Flower = PlayerPrefs.GetInt("Flower", Flower);
        Seeds = PlayerPrefs.GetInt("Seeds", Seeds);
        FoxPlushie = PlayerPrefs.GetInt("FoxPlushie", FoxPlushie);

        t_Potion01 = t_Potion01.GetComponent<Text>();
        t_Potion02 = t_Potion02.GetComponent<Text>();
        t_Apple = t_Apple.GetComponent<Text>();
        t_Book = t_Book.GetComponent<Text>();
        t_Leaf = t_Leaf.GetComponent<Text>();
        t_Flower = t_Flower.GetComponent<Text>();
        t_Seeds = t_Seeds.GetComponent<Text>();
        t_FoxPlushie = t_FoxPlushie.GetComponent<Text>();

        t_Potion01.text = Potion01.ToString();
        t_Potion02.text = Potion02.ToString();
        t_Apple.text = Apple.ToString();
        t_Book.text = Book.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        t_FoxPlushie.text = FoxPlushie.ToString();

        if (FoxPlushie == 0)
        {
            FoxPlushieActive.SetActive(false);
            FoxPlushieInactive.SetActive(true);
        }
        else
        {
            FoxPlushieActive.SetActive(true);
            FoxPlushieInactive.SetActive(false);
        }
        if (Seeds == 0)
        {
            SeedsActive.SetActive(false);
            SeedsInactive.SetActive(true);
        }
        else
        {
            SeedsActive.SetActive(true);
            SeedsInactive.SetActive(false);
        }
        if (Potion01 == 0)
        {
            Potion01Active.SetActive(false);
            Potion01Inactive.SetActive(true);
        }
        else
        {
            Potion01Active.SetActive(true);
            Potion01Inactive.SetActive(false);
        }
        if (Potion02 == 0)
        {
            Potion02Active.SetActive(false);
            Potion02Inactive.SetActive(true);
        }
        else
        {
            Potion02Active.SetActive(true);
            Potion02Inactive.SetActive(false);
        }
        if (Apple == 0)
        {
            AppleActive.SetActive(false);
            AppleInactive.SetActive(true);
        }
        else
        {
            AppleActive.SetActive(true);
            AppleInactive.SetActive(false);
        }
        if (Book == 0)
        {
            BookActive.SetActive(false);
            BookInactive.SetActive(true);
        }
        else
        {
            BookActive.SetActive(true);
            BookInactive.SetActive(false);
        }
        if (Leaf == 0)
        {
            LeafActive.SetActive(false);
            LeafInactive.SetActive(true);
        }
        else
        {
            LeafActive.SetActive(true);
            LeafInactive.SetActive(false);
        }
        if (Flower == 0)
        {
            FlowerActive.SetActive(false);
            FlowerInactive.SetActive(true);
        }
        else
        {
            FlowerActive.SetActive(true);
            FlowerInactive.SetActive(false);
        }

        //Player Inventory


        AS = GetComponent<AudioSource>();
        CVC = CVC.GetComponent<CinemachineVirtualCamera>();
        var transposer = CVC.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.y = 3.5f;
        CharCont = GetComponent<CharacterController>();
        anim = anim.GetComponent<Animator>();

        if (transposer.m_FollowOffset.y < 1.0f || transposer.m_FollowOffset.y > 10.0f)
            transposer.m_FollowOffset.y = 3.5f;

        Invoke("CheckCam", .5f);
    }

    public void CloseAlchemy()
    {
        isAlchemy = false;
        go_TableCanvas.SetActive(false);
    }

    public void CheckInventory()
    {
        Potion01 = PlayerPrefs.GetInt("Potion01", Potion01);
        Potion02 = PlayerPrefs.GetInt("Potion02", Potion02);
        Apple = PlayerPrefs.GetInt("Apple", Apple);
        Book = PlayerPrefs.GetInt("Book", Book);
        Leaf = PlayerPrefs.GetInt("Leaf", Leaf);
        Flower = PlayerPrefs.GetInt("Flower", Flower);
        Seeds = PlayerPrefs.GetInt("Seeds", Seeds);
        FoxPlushie = PlayerPrefs.GetInt("FoxPlushie", FoxPlushie);

        t_Potion01.text = Potion01.ToString();
        t_Potion02.text = Potion02.ToString();
        t_Apple.text = Apple.ToString();
        t_Book.text = Book.ToString();
        t_Leaf.text = Leaf.ToString();
        t_Flower.text = Flower.ToString();
        t_Seeds.text = Seeds.ToString();
        t_FoxPlushie.text = FoxPlushie.ToString();

        if (FoxPlushie == 0)
        {
            FoxPlushieActive.SetActive(false);
            FoxPlushieInactive.SetActive(true);
        }
        else
        {
            FoxPlushieActive.SetActive(true);
            FoxPlushieInactive.SetActive(false);
        }
        if (Seeds == 0)
        {
            SeedsActive.SetActive(false);
            SeedsInactive.SetActive(true);
        }
        else
        {
            SeedsActive.SetActive(true);
            SeedsInactive.SetActive(false);
        }

        if (Potion01 == 0)
        {
            Potion01Active.SetActive(false);
            Potion01Inactive.SetActive(true);
        }
        else
        {
            Potion01Active.SetActive(true);
            Potion01Inactive.SetActive(false);
        }
        if (Potion02 == 0)
        {
            Potion02Active.SetActive(false);
            Potion02Inactive.SetActive(true);
        }
        else
        {
            Potion02Active.SetActive(true);
            Potion02Inactive.SetActive(false);
        }
        if (Apple == 0)
        {
            AppleActive.SetActive(false);
            AppleInactive.SetActive(true);
        }
        else
        {
            AppleActive.SetActive(true);
            AppleInactive.SetActive(false);
        }
        if (Book == 0)
        {
            BookActive.SetActive(false);
            BookInactive.SetActive(true);
        }
        else
        {
            BookActive.SetActive(true);
            BookInactive.SetActive(false);
        }
        if (Leaf == 0)
        {
            LeafActive.SetActive(false);
            LeafInactive.SetActive(true);
        }
        else
        {
            LeafActive.SetActive(true);
            LeafInactive.SetActive(false);
        }
        if (Flower == 0)
        {
            FlowerActive.SetActive(false);
            FlowerInactive.SetActive(true);
        }
        else
        {
            FlowerActive.SetActive(true);
            FlowerInactive.SetActive(false);
        }
    }

    private void CheckCam()
    {
        var transposer = CVC.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.y = 3.5f;
    }

    void ChangeAnimations(string newState)
    {
        if (CurAnimation == newState)
            return;

        anim.Play(newState);

        CurAnimation = newState;
    }

    private float MouseYFloat;
    private Vector2 PlayerTurn;
    public bool TableCanvas;

    [SerializeField] GameObject go_TableCanvas;

    public void GhostPlushie()
    {
        if (FoxPlushie > 0)
        {
            FoxPlushie--;
            PlayerPrefs.SetInt("FoxPlushie", FoxPlushie);
            CheckInventory();
            Instantiate(FoxGhost, soulInstantiate.transform.position, soulInstantiate.transform.rotation);
        }
    }
    public void usePotion01()
    {
        if (inFoxGhostRange)
        {
            Potion01--;
            PlayerPrefs.SetInt("FoxPlushie", FoxPlushie);
            CheckInventory();
            Destroy(NewGO);
            Instantiate(FoxSoul, soulInstantiate.transform.position, soulInstantiate.transform.rotation);
            inFoxGhostRange = false;
            if (ISTS != null)
                ISTS.OpenDoor();
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            if (isPaused)
                isInventory = false;
        }
        if (Input.GetButtonDown("Inventory") && !isPaused)
        {
            isInventory = !isInventory;
            if (isInventory)
                CheckInventory();
        }

        if (!isPaused && !isInventory && !isAlchemy && !isDead)
        {
            if (b_Potion01)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Potion01++;
                    PlayerPrefs.SetInt("Potion01", Potion01);
                    CheckInventory();
                    b_Potion01 = false;
                }
            }
            if (b_Potion02)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Potion02++;
                    PlayerPrefs.SetInt("Potion02", Potion02);
                    CheckInventory();
                    b_Potion02 = false;
                }
            }
            if (b_Apple)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Apple++;
                    PlayerPrefs.SetInt("Apple", Apple);
                    CheckInventory();
                    b_Apple = false;
                }
            }
            if (b_Book)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Book++;
                    PlayerPrefs.SetInt("Book", Book);
                    CheckInventory();
                    b_Book = false;
                }
            }
            
            if (b_Plant)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Flower++;
                    Leaf++;
                    PlayerPrefs.SetInt("Flower", Flower);
                    PlayerPrefs.SetInt("Leaf", Leaf);
                    CheckInventory();
                    b_Plant = false;
                }
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            isAbleToPush = Physics.CheckSphere(PushCheck.position, PushDistance, PushMask);

            MouseTurn.x = Input.GetAxis("Mouse X") * Time.deltaTime * TurnSensitivity;
            MouseTurn.y = Input.GetAxis("Mouse Y") * Time.deltaTime * -TurnSensitivityY;
            if (Input.GetAxis("Controller X") > .35f || Input.GetAxis("Controller X") < -.35f)
                ControllerTurn.x = Input.GetAxis("Controller X") * Time.deltaTime * TurnSensitivity * 2;
            if (Input.GetAxis("Controller Y") > .35f || Input.GetAxis("Controller Y") < -.35f)
                ControllerTurn.y = Input.GetAxis("Controller Y") * Time.deltaTime * -TurnSensitivityY * 2;
            if (Input.GetAxis("Controller X") < .35f && Input.GetAxis("Controller X") > -.35f)
                ControllerTurn.x = 0;
            if (Input.GetAxis("Controller Y") < .35f && Input.GetAxis("Controller Y") > -.35f)
                ControllerTurn.y = 0;

            PlayerTurn.x = MouseTurn.x + ControllerTurn.x;
            PlayerTurn.y = MouseTurn.y + ControllerTurn.y;
            CharCont.transform.Rotate(0, PlayerTurn.x, 0);

            var transposer = CVC.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer.m_FollowOffset.y > 1.0f && PlayerTurn.y < 0)
                transposer.m_FollowOffset.y += PlayerTurn.y;
            else if (transposer.m_FollowOffset.y < 10.0f && PlayerTurn.y > 0)
                transposer.m_FollowOffset.y += PlayerTurn.y;

            playerVelocity.x = 0;

            playerVelocity.y += Gravity * Time.deltaTime;
            CharCont.Move(playerVelocity * Time.deltaTime);
            float SpeedX = Input.GetAxis("Horizontal");
            float SpeedZ = Input.GetAxis("Vertical");

            if ((SpeedX == 0 && SpeedZ == 0) || !isGrounded)
                FootSteps.SetActive(false);
            if ((SpeedX != 0 || SpeedZ != 0) && isGrounded)
                FootSteps.SetActive(true);

            CharacterMovement = CharCont.transform.forward * SpeedZ;
            CharacterMovementX = CharCont.transform.right * SpeedX;
            CharCont.Move((CharacterMovement + CharacterMovementX) * Time.deltaTime * playerSpeed);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
                playerVelocity.y = JumpHeight;
                StartCoroutine("PlayerJump");
            }
            if (Input.GetButtonDown("Fire1") && !inRange && isGrounded && !isInAlchemyRange && !inAltar)
            {
                AS.clip = ScytheSwing;
                AS.Play();
                if (isAttacking)
                    StopCoroutine("PlayerAttack");

                Attack.SetActive(true);

                int RandomAttack = Random.Range(0, 2);
                AttackNumber = RandomAttack;
                isAttacking = true;
                StartCoroutine("PlayerAttack");
            }
            if (Input.GetButtonDown("Fire1") && inAltar)
            {
                Mana = 100;
                ManaSlider.value = Mana / 100;
                PlayerPrefs.SetFloat("Mana", Mana);
            }    
            if (Input.GetButtonDown("Fire1") && isInAlchemyRange)
            {
                go_TableCanvas.SetActive(true);
                isAlchemy = true;
            }
            if (Input.GetButtonDown("Fire1") && inRange && isGrounded)
            {
                StartCoroutine("PlayerPushing");
            }
            if (Input.GetButtonUp("Fire1") && (isPushing || isStartingPush || isEndingPush))
            {
                StopCoroutine("PlayerPushing");
                isPushing = false;
                StartCoroutine("PlayerEndPush");
            }

            //
            // Animation States
            //

            if (!isAttacking && !isPushing && !isStartingPush && !isEndingPush)
            {
                if (SpeedZ == 0 && SpeedX == 0)
                {
                    if (isGrounded && !isJumping)
                        ChangeAnimations(Idle);
                    if (!isGrounded && !isJumping)
                        ChangeAnimations(InAir);
                    if (!isGrounded && isJumping)
                        ChangeAnimations(Jump);
                }
                if (SpeedZ != 0 || SpeedX != 0)
                {
                    if (isGrounded && !isJumping)
                        ChangeAnimations(Run);
                    if (!isGrounded && !isJumping)
                        ChangeAnimations(InAir);
                    if (!isGrounded && isJumping)
                        ChangeAnimations(Jump);
                }
            }
            else if (isAttacking && !isPushing && !isStartingPush && !isEndingPush)
            {
                if (AttackNumber == 0)
                    ChangeAnimations(Attack1);
                else
                    ChangeAnimations(Attack2);
            }
            else if (!isPushing && isStartingPush && !isEndingPush && !isAttacking)
                ChangeAnimations(PushStart);
            else if (isPushing && !isStartingPush && !isEndingPush && !isAttacking && SpeedZ == 0)
                ChangeAnimations(PushingIdle);
            else if (isPushing && !isStartingPush && !isEndingPush && !isAttacking && SpeedZ != 0)
                ChangeAnimations(Pushing);
            else if (!isPushing && !isStartingPush && isEndingPush && !isAttacking)
                ChangeAnimations(PushEnd);

            Inventory.SetActive(false);
            PauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        //
        // Pause Menu
        //


        else if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.SetActive(true);
        }
        else if (isInventory)
        {
            Cursor.lockState = CursorLockMode.None;
            Inventory.SetActive(true);
        }
        else if (isAlchemy)
        {
            Cursor.lockState = CursorLockMode.None;
            go_TableCanvas.SetActive(true);
        }

        //
        // Player Inventory
        //
    }
    
    public void ClosePause()
    {
        isPaused = false;
    }

    IEnumerator PlayerJump()
    {
        yield return new WaitForSeconds(1);
        isJumping = false;
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        Attack.SetActive(false);
    }
    [SerializeField] GameObject Attack;
    IEnumerator PlayerPushing()
    {
        isStartingPush = true;
        yield return new WaitForSeconds(1f);
        isStartingPush = false;
        isPushing = true;
    }
    IEnumerator PlayerEndPush()
    {
        isPushing = false;
        isEndingPush = true;
        yield return new WaitForSeconds(1f);
        isEndingPush = false;
    }

    private int AttackNumber;
    private bool inRange, isPushing, isStartingPush, isEndingPush, isHoldingObject, isInAlchemyRange, inAltar;
    private bool b_Potion01, b_Potion02, b_Apple, b_Book, b_Plant;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Altar")
        {
            inAltar = true;
        }
        if (other.gameObject.tag == "Pushable")
        {
            inRange = true;
        }
        if (other.gameObject.tag == "Potion01")
        {
            b_Potion01 = true;
        }
        if (other.gameObject.tag == "FoxPlushie")
        {
            FoxPlushie++;
            PlayerPrefs.SetInt("FoxPlushie", FoxPlushie);
            Destroy(other.gameObject);
            CheckInventory();
        }
        if (other.gameObject.tag == "Potion02")
        {
            b_Potion02 = true;
        }
        if (other.gameObject.tag == "Apple")
        {
            b_Apple = true;
        }
        if (other.gameObject.tag == "Book")
        {
            b_Book = true;
        }
        if (other.gameObject.tag == "Plant")
        {
            b_Plant = true;
        }

        if (other.gameObject.tag == "Table")
            isInAlchemyRange = true;

        if (other.gameObject.tag == "FoxGhost")
        {
            inFoxGhostRange = true;
            NewGO = other.gameObject;
        }
    }
    GameObject NewGO;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FoxGhost")
            inFoxGhostRange = false;

        if (other.gameObject.tag == "Altar")
        {
            inAltar = false;
        }
        if (other.gameObject.tag == "Pushable")
        {
            inRange = false;
        }
        if (other.gameObject.tag == "Potion01")
        {
            b_Potion01 = false;
        }
        if (other.gameObject.tag == "Potion02")
        {
            b_Potion02 = false;
        }
        if (other.gameObject.tag == "Apple")
        {
            b_Apple = false;
        }
        if (other.gameObject.tag == "Book")
        {
            b_Book = false;
        }
        if (other.gameObject.tag == "Plant")
        {
            b_Plant = false;
        }

        if (other.gameObject.tag == "Table")
            isInAlchemyRange = false;
    }
}
