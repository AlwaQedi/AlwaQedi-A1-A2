using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public bool PlayerBullet=true;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip hitWall;


    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            audioSource.PlayOneShot(hitWall);
            Destroy(gameObject);
        }
        //Debug.Log("check trigger enter : "+ collision.gameObject.tag);
        if (collision.gameObject.tag=="Enemy" && PlayerBullet)
        {
            collision.gameObject.GetComponent<EnemyFire>().ApplyDamage();
            Destroy(gameObject);
        }
        else
        if (collision.gameObject.tag == "Player" && !PlayerBullet)
        {
            collision.gameObject.GetComponent<PlayerMovement>().ApplyDamage();
            Destroy(gameObject);
        }

    }

}
