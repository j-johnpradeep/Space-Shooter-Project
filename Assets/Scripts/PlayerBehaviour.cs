using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] // It is used for private variables which designer can access
    //private float _hspeed = 2.5f; //private only seen by player not by designer
    private float _vspeed = 5.5f; //private only seen by player not by designer

    float horizontalInput;
    float verticalInput;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotLaserPrefab;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0f;
    [SerializeField]
    private int _life = 3;
    private bool _isTripleShotEnabled = false;
    private bool _isSpeedEnabled = false;
    private bool _isShieldEnabled = false;
    public bool _Player1=false;
    public bool _Player2=false;

    private SpawnManager _spawnManager;
    [SerializeField]
    private List<GameObject> _hurtAnimation;
    [SerializeField]
    private int _score;
    private UI_Manager _uiManager;
    [SerializeField]
    AudioClip[] _audioClips;
    [SerializeField]
    AudioSource _playerAudio;

    [SerializeField]
    Animator _playerAniamtion;
    void Start()// Start is called before the first frame update
    {
        
        if (SceneManager.GetActiveScene().ToString() == "Game")
        {
            //change the current position of player when start the game
            transform.position = new Vector3(0, 0, 0); //Vector3 used to apply all position in unity 3=x,y,z axis
            transform.rotation = Quaternion.Euler(0, 0, 0); //Quaternion.Euler used to rotate objects in x,y,z axis
        }

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        _playerAudio = GetComponent<AudioSource>();
        _playerAniamtion = GetComponent<Animator>();

        if (_spawnManager != null)
        {
            Debug.Log("SpawnManager is present");
        }

        if (_playerAniamtion == null)
        {
            Debug.Log("SpawnManager is not present");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        //Time.time is how long the game is running if game runs for 1 min then Time.time take as 60 secs

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor || !Application.isMobilePlatform)
        {
            if (_Player1)
            {
                if (Input.GetKeyUp(KeyCode.Space) && Time.time > _nextFire)
                {
                    laserFire();
                }
            }

            if (_Player2)
            {
                if (Input.GetKeyUp(KeyCode.KeypadEnter) && Time.time > _nextFire)
                {
                    laserFire();
                }
            }
        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _nextFire)
            {
                laserFire();
            }
        }
        MovementAnimation();
    }

    private void MovementAnimation()
    {
        if (_Player1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _playerAniamtion.SetBool("isLeft", true);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _playerAniamtion.SetBool("isLeft", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _playerAniamtion.SetBool("isRight", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _playerAniamtion.SetBool("isRight", false);
            }
        }
        if (_Player2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _playerAniamtion.SetBool("isLeft", true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _playerAniamtion.SetBool("isLeft", false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _playerAniamtion.SetBool("isRight", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _playerAniamtion.SetBool("isRight", false);
            }
        }
        
    }

    void laserFire()
    {
        _nextFire = Time.time + _fireRate;
        //Instantiate used to spawn game object by cloning original object of prefab
        // 10 ways to instantiate gameobject here we used orginal obj,position,rotation 

        //transform.rotation also expressed as Quaternion.identity
        // transform.position also expressed as new Vector3(transform.position.x,transform.position.y,0)

        if (_isTripleShotEnabled)
        {
            Instantiate(_tripleShotLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.45f, 0), Quaternion.identity);
        }
        _playerAudio.clip = _audioClips[0];
        _playerAudio.PlayDelayed(.2f);
        
    }
    void playerMovement()
    {
        //transform.Translate(new Vector3(-1, 0, 0)); it moves 60 frames per second
        //it moves n frame per second default 1 frame

        //transform.Translate(Vector3.right * horizontalInput * _hspeed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _vspeed * Time.deltaTime);


        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
        {
            //horizontalInput = Input.GetAxis("Horizontal");
            //verticalInput = Input.GetAxis("Vertical");

            if (_Player1)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.up);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.down);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.left);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.right);
                }
            }
            if (_Player2)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.up);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.down);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.left);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(_vspeed * Time.deltaTime * Vector3.right);
                }
            }
             
        }
        else
        {
            horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        }


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _vspeed * Time.deltaTime);


        //if (transform.position.x >= 9.228806f)
        //{        //here we need to mention 3 axis otherwise c# wont accept it
        //    transform.position = new Vector3(9.228806f, transform.position.y, 0); 
        //}


        //Boundary for player around the corner
        //if (transform.position.y >= 5.961226f)
        //{
        //    transform.position = new Vector3(transform.position.x, 5.961226f, 0);
        //    if (transform.position.x >= 9.139139f)
        //    {
        //        transform.position = new Vector3(9.139139f, transform.position.y, 0);
        //    }
        //    else if (transform.position.x <= -9.246708f)
        //    {
        //        transform.position = new Vector3(-9.246708f, transform.position.y, 0);
        //    }
        //}
        //else if (transform.position.y <= -3.971544f)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.971544f, 0);
        //    if (transform.position.x <= -9.224866f)
        //    {
        //        transform.position = new Vector3(-9.224866f, transform.position.y, 0);
        //    }
        //    else if (transform.position.x >= 9.244018f)
        //    {
        //        transform.position = new Vector3(9.244018f, transform.position.y, 0);
        //    }
        //}
        //else if (transform.position.x <= -9.224866f)
        //{
        //    transform.position = new Vector3(-9.224866f, transform.position.y, 0);
        //}
        //else if (transform.position.x >= 9.19252f)
        //{
        //    transform.position = new Vector3(9.19252f, transform.position.y, 0);
        //}


        //We can use math.clamp to bound the object
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.19252f, 9.19252f), transform.position.y, 0);


        // Wrap Objects left to right or up to down when reach corner
        if (transform.position.y >= 7.961226f)
        {
            transform.position = new Vector3(transform.position.x, -7.961226f, 0);
        }
        else if (transform.position.y <= -7.961226f)
        {
            transform.position = new Vector3(transform.position.x, 7.961226f, 0);
        }

        else if (transform.position.x >= 11.244018f)
        {
            transform.position = new Vector3(-11.244018f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.246708f)
        {
            transform.position = new Vector3(11.246708f, transform.position.y, 0);
        }
    }
    public void damage() //other class can access only if this is public
    {
        if (_isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _playerShield.SetActive(_isShieldEnabled);
            return;
        }
        _life--;

        if(_life<=2 && _life >= 1)
        {
            int _random_hurt = UnityEngine.Random.Range(0, _hurtAnimation.Count);
            _hurtAnimation[_random_hurt].SetActive(true);
            _hurtAnimation.RemoveAt(_random_hurt);

        }
        _uiManager.UpdateLife(_life);

        if (_life < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject,.2f);
            _playerAudio.clip = _audioClips[1];
            _playerAudio.PlayDelayed(.2f);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedActive()
    {
        _isSpeedEnabled = true;
        
        StartCoroutine(SpeeedRoutine());
    }
    public void ShieldActive()
    {
        _isShieldEnabled = true;
        _playerShield.SetActive(_isShieldEnabled);
    }

    IEnumerator SpeeedRoutine()
    {
        while (_isSpeedEnabled)
        {
            _vspeed *= 2;
            yield return new WaitForSeconds(5f);
            _isSpeedEnabled=false;
            _vspeed/= 2;
        }
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        while (_isTripleShotEnabled)
        {
            yield return new WaitForSeconds(5f);
            _isTripleShotEnabled = false;
        }
    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
}
