using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private float _espeed=3f;
    PlayerBehaviour playerBehaviour;
    [SerializeField]
    private Animator _enemyDestroy_Anim;
    [SerializeField]
    AudioSource _source;
    [SerializeField]
    GameObject _enemyLaser;
    bool _isDestroyed = true;
    Coroutine _laserDestroyCoroutine;

    void Start()
    {
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        _source = GetComponent<AudioSource>();
        transform.position = new Vector3(transform.position.x,6f,0);
        if (playerBehaviour != null)
        {
            _enemyDestroy_Anim = GetComponent<Animator>();
        }
        _laserDestroyCoroutine = StartCoroutine(EnemyLaserShoot());
    }
    IEnumerator EnemyLaserShoot()
    {
        while (_isDestroyed)
        {
            GameObject laser = Instantiate(_enemyLaser, transform.position + new Vector3(0, -0.79f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2.5f, 5));
        }
    }
    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _espeed * Time.deltaTime);
        if (transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //other stores information about which object is colliding
    {
        {
            //Debug.Log("You hit:" + other.transform.name);//other refers to object transform.name name of object
            if (other.tag == "Player")
            {
                //using this without reference variable when we remove playerscript it gives error
                //NullReferenceException: Object reference not set to an instance of an object bcz of it will consider
                //there is no object reference to this component

                //other.transform.GetComponent<PlayerBehaviour>().damage(); //it is not proper way to use this

                //We should get component using only reference variable
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();// transform is root of the object
                StartCoroutine(EnemyDestroyCoroutine());

                if (player != null)
                {
                    
                    player.damage();
                }
            }
            else if (other.CompareTag("Laser")) // efficient way to compare tags
            {
                if (playerBehaviour != null)
                {
                    //this.gameObject.GetComponent<Collider2D>().enabled = false;
                    //_espeed = 0;
                    //_enemyDestroy_Anim.SetTrigger("Enemy_Destroy");
                    playerBehaviour.AddScore();
                }
                Destroy(other.gameObject); //it defines and destroy laser
                //Destroy(GameObject.FindWithTag("Enemies"));// another way to destroy gameObjects using tags
                StartCoroutine(EnemyDestroyCoroutine());


            }
            if (other.CompareTag("Enemies"))
            {
                StartCoroutine(EnemyDestroyCoroutine());
            }
        }
    }
    IEnumerator EnemyDestroyCoroutine()
    {
        _isDestroyed = true;
        StopCoroutine(_laserDestroyCoroutine);
        gameObject.GetComponent<Collider2D>().enabled = false;
        _espeed = 0;
        _enemyDestroy_Anim.SetTrigger("Enemy_Destroy");
        _source.PlayDelayed(.1f);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
    private void OnDestroy()
    {
        StopCoroutine(_laserDestroyCoroutine);
    }
}
