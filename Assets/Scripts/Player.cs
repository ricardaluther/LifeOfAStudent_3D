using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    [SerializeField]
    //Variables for ects, as well as the start and maximum value:
    private static int ects;
    public static readonly int EctsStart = 0;
    public static readonly int EctsMax = 180;
    
    //Variables for stress, as well as the start and maximum value:
    private static int stress;
    public static readonly int StressStart = 0;
    public static readonly int StressMax = 20;
    
    //Variables for money, as well as the start and maximum value:
    private static int money;
    public static readonly int MoneyStart = 5000;
    public static readonly int MoneyMax = 5000;
    
    //Values for the transition between different throwing tiers:
    public static readonly int Tier2Border = 3500;
    public static readonly int Tier1Border = 1500;
    

    public bool _drunk = false;
    private float _soberingTime = 10f;

    public bool _distracted = false;
    private float _phoneTime = 4f;

    public bool _caffeinated;
    private float _coffeeTime = 8f;

    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpSpeed = 4f;
    [SerializeField] private float _gravity = 9.81f;
    private float _directionY;
    private bool _readyDoubleJump;
    private CharacterController _controller;
    private Animation anim;
    private BoxCollider col;

    public AudioClip nyanCat;
    
    [SerializeField]
    public GameOverScreen GameOverScreen;
    [SerializeField]
    public WinScreen WinScreen;
    [SerializeField]
    public SpawnManager SpawnManager;
    
    [SerializeField]
    public AudioListener audioListener;

    void Start()
    {
        _drunk = false;
        _distracted = false;
        _controller = GetComponent<CharacterController>();
        audioListener = GetComponent<AudioListener>();
        transform.position = new Vector3(0f, 1f, 0f);
        anim = gameObject.GetComponent<Animation>();
        ects = EctsStart;
        stress = StressStart;
        money = MoneyStart;
    }

    void Update()
    {
        PlayerMovement();
        Player.AddMoney(-1);
        if (audioListener != null && audioListener != this.audioListener)
        {
            Destroy(this.gameObject);
        }
        else if(audioListener == null)
        {
            audioListener = GetComponent<AudioListener>();
        }
        if (stress > StressMax)
        {
            
            audioListener.enabled = false;
            GameOver();
            Cursor.lockState = CursorLockMode.None;
            //use nullchecks
            //Destroy(transform.parent.gameObject);
            if (SpawnManager != null)
            {
                SpawnManager.onPlayerDeath();
            }
            else if (SpawnManager == null)
            {
                Debug.LogError("SpawnManager not assigned you tired idiot!");
            }
        }
        if(ects >= 180)
        {
            audioListener.enabled = false;
            Win();
            Cursor.lockState = CursorLockMode.None;
            //use nullchecks
            //Destroy(transform.parent.gameObject);
            if (SpawnManager != null)
            {
                SpawnManager.onPlayerDeath();
            }
            else if (SpawnManager == null)
            {
                Debug.LogError("SpawnManager not assigned you tired idiot!");
            }
        }
    
    }

    void PlayerMovement()
    {
        // pass basic movements to playertranslate
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 playerTranslate = transform.right * horizontalInput + transform.forward * verticalInput;
        //basic animations when moving
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.Play("Run");
        }
        else
        {
            anim.Play("Idle");
        }

        // is the player is grounded and we press space, allow the player to doublejump and apply _jumpspeed to the direction Y of the playertranslate
        if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _readyDoubleJump = true;
            _directionY = _jumpSpeed;
        }
        // if the player is not grounded but doublejump is activated, enable another jump of have the speed
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _readyDoubleJump)
            {
                _directionY = _jumpSpeed * 0.5f;
                _readyDoubleJump = false;
            }
        }

        // apply gravity to the y-direction, pass the y-direction to the playertranslate and pass the playertranslate to the controller
        _directionY -= _gravity * Time.deltaTime;
        playerTranslate.y = _directionY;
        if (!_drunk && !_distracted && !_caffeinated)
        {
            _controller.Move(playerTranslate * _speed * Time.deltaTime);
        }
        else if (_drunk && !_caffeinated && !_distracted)
        {
            _controller.Move(new Vector3(-playerTranslate.x, playerTranslate.y, -playerTranslate.z) * _speed * Time.deltaTime);
        }
        else if (!_drunk && _caffeinated && !_distracted)
        {
            _controller.Move(playerTranslate * _speed * 3 * Time.deltaTime);
        }
        else if (_drunk && _caffeinated && !_distracted)
        {
            _controller.Move(new Vector3(-playerTranslate.x, playerTranslate.y, -playerTranslate.z) * _speed * 3 *
                             Time.deltaTime);
        }
        
    }

    //gets called, when the player enters an ActivePoint
    public void LocationChange(float x, float y, float z)
    {
        Debug.LogWarning("LocationChange called");
        _controller.Move(new Vector3(x, y, z));
    }

    public void GetDrunk()
    {
        _drunk = true;
        StartCoroutine(SoberUp());
    }

    IEnumerator SoberUp()
    {
        yield return new WaitForSeconds(_soberingTime);
        _drunk = false;
    }
    
    public void GetDistracted()
    {
        _distracted = true;
		AudioSource.PlayClipAtPoint(nyanCat, transform.position);
        StartCoroutine(BackToFocus());
    }

    IEnumerator BackToFocus()
    {
        yield return new WaitForSeconds(_phoneTime);
        _distracted = false;
    }
    public void GetCaffeinated()
    {
        _caffeinated = true;
        StartCoroutine(DecreasedCoffeine());
    }

    IEnumerator DecreasedCoffeine()
    {
        yield return new WaitForSeconds(_coffeeTime);
        _caffeinated = false;
    }
    
    public static int GetEcts()
    {
        return ects;
    }

    public static void AddEcts(int amount)
    {
        ects += amount;
    }

    public static int GetMoney()
    {
        return money;
    }

    public static void AddMoney(int amount)
    {
        if (money + amount > 0)
        {
            money += amount;
        }
        else
        {
            money = 0;
        }
    }

    public static int GetStress()
    {
        return stress;
    }

    public static void AddStress(int amount)
    {
        if (stress + amount > 0)
        {
            stress += amount;
        }
        else
        {
            stress = 0;
        }
    }

    public void GameOver()
    {
        GameOverScreen.Setup(Player.GetEcts());
    }
    public void Win()
    {
        WinScreen.SetupWin(Player.GetEcts());
    }

    
}

