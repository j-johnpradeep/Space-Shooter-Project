using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.5f;
    [SerializeField]
    private float _powerupID;
    [SerializeField]
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            // instantiate clip and destroy after played even source object destroyed
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position); 
            PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0: // tripleLaser PowerUP
                        player.TripleShotActive();
                        break;
                    case 1: // speed PowerUP
                        player.SpeedActive();
                        break;
                    case 2: // shield PowerUP
                        player.ShieldActive();
                        break;
                }
            }
            
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
