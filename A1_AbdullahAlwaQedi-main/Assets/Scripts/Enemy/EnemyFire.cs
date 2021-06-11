using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    SpriteRenderer marioSprite;
    Animator anim;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile[] projectilePrefab;
    GameObject targetPlayer;
    public float attackDistance= 175;

    public AudioSource audioSource;
    public AudioClip hitSound, deathSound;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        marioSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!spawnPointLeft||!spawnPointRight||projectilePrefab.Length==0)
            Debug.Log("Unity Inspector Values Not Set");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(targetPlayer.transform.position, transform.position);
        if (distance<= attackDistance)
        {
            Vector2 direction = targetPlayer.transform.position - transform.position;
            if (direction.x < 0)
                marioSprite.flipX = true;
            else
                marioSprite.flipX = false;

            anim.SetBool("isShooting", true);

        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }

    void FireProjectile()
    {
        if (marioSprite.flipX)
        {
            Projectile projectileInstance = Instantiate(projectilePrefab[0], spawnPointLeft.position, spawnPointLeft.rotation);
            projectileInstance.speed = -projectileSpeed;
        }
        else
        {
            Projectile projectileInstance = Instantiate(projectilePrefab[1], spawnPointRight.position, spawnPointRight.rotation);
            projectileInstance.speed = projectileSpeed;
        }

    }

    void ResetFire()
    {
        anim.SetBool("isShooting", false);
    }
    bool isAlive = true;
    public void ApplyDamage()
    {
        if (isAlive)
        {
            isAlive = false;
            GameManager._instance.AddScore();
            audioSource.PlayOneShot(deathSound);
            anim.SetTrigger("Death");
            Destroy(this.gameObject, 1);
        }
    }
}