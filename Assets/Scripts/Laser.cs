using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.5f;
    void Update()
    {
        transform.Translate(/*Vector3.up*/ _speed * Time.deltaTime * new Vector3(0, 1.5f, 0));
        if (transform.position.y >= 8)
        {
            if (transform.parent!=null)
            {
                Destroy(this.transform.parent.gameObject,0.25f);
            }
            else
            {
                Destroy(this.gameObject, 0.25f);
            }
            // this.gameObject refer to object or component which is attached to this script
        }
        if (transform.position.y <= -6.25f)
        {
            Destroy(this.gameObject, 0.25f);
        }
    }
    //private void OnBecameInvisible()
    //{
    //    if (transform.parent != null)
    //    {
    //        Destroy(this.transform.parent.gameObject, 0.25f);
    //    }
    //    else
    //    {
    //        Debug.Log(Time.time + gameObject.name);
    //        Destroy(this.gameObject, 0.25f);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag!="Laser")
        {
            Debug.Log("hit");
            PlayerBehaviour playerBehaviour = collision.GetComponent<PlayerBehaviour>();
            if (playerBehaviour != null)
            {
                Debug.Log("hit123");
                playerBehaviour.damage();

            }
            Destroy(gameObject);
        }
    }
}
