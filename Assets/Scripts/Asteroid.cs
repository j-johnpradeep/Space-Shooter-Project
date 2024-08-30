using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    public float _rotateSpeed=20;
    [SerializeField]
    GameObject _Explosion;
    SpawnManager spawnManager;
    [SerializeField]
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,1 * _rotateSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Laser"))
            {
                GameObject explosion = Instantiate(_Explosion,transform.position,Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject,.5f);
                _audioSource.Play();
                Destroy(explosion, 3);
                spawnManager.StartSpawning();
            }
        }
    }
}
