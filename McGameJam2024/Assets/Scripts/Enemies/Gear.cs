using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // We want access to the GameManager functions from this class.
    [SerializeField] GameManager gameManager;
    Animator animator;

    // Enemy fields
    private int health = 2;
    private float time = 0f;
    private float shootingTime = 0f;
    private float chargeTime = 0f;
    private bool goingUp;
    private bool shoot;

    // Prefabs
    [SerializeField] GameObject saw;

    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        animator = this.GetComponent<Animator>();
        shoot = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 2 + gameManager.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting animator speed
        if (!shoot) {

            time += Time.deltaTime;
            shootingTime += Time.deltaTime;

            // Cancelling charge time to timer
            time -= chargeTime;
            chargeTime = 0f;

            // Sin movement
            float x = 2 * time;
            float y = (float)(2 * (Math.Sin(x)));
            animator.SetFloat("Vert_speed", y);

            // Moving in increments
            transform.position = new Vector3(7.5f, 0f, 0f) - new Vector3(x, y, 0f);
        }

        

        // Shooting
        if(shootingTime >= 2f)
        {
            StartCoroutine(Shoot());
            shootingTime = 0f;
        }


    }

    IEnumerator Shoot()
    {
        shoot = true;
        animator.SetFloat("Vert_speed", 0f);
        animator.SetTrigger("shoot");
        yield return new WaitForSeconds(2f);

        // Shooting the saw octagonally
        DetachSaws();

        animator.ResetTrigger("shoot");
        chargeTime += Time.deltaTime;
        
        shoot = false;
        
    }

    // Behaviour when the enemy is hit
    public void OnHit()
    {
        if (health >= 0)
        {
            health--;
        }
        else
        {
            OnDestroyed();
        }
    }

    // Behaviour when the enemy is destroyed
    private void OnDestroyed()
    {
        gameManager.EnemyDown(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("EnemyWall"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("PlayerBullet"))
        {
            OnHit();
        }
    }

    private void DetachSaws()
    {
        GameObject bulletNW = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 135));
        bulletNW.GetComponent<EnemyBullet>().direction = new Vector3(-1, 1, 0).normalized;

        GameObject bulletNE = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 45));
        bulletNE.GetComponent<EnemyBullet>().direction = new Vector3(1, 1, 0).normalized;

        GameObject bulletSE = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, -45));
        bulletSE.GetComponent<EnemyBullet>().direction = new Vector3(1, -1, 0).normalized;

        GameObject bulletSW = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, -135));
        bulletSW.GetComponent<EnemyBullet>().direction = new Vector3(-1, -1, 0).normalized;

        GameObject bulletN = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 90));
        bulletN.GetComponent<EnemyBullet>().direction = new Vector3(0, 1, 0).normalized;

        GameObject bulletE = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 0));
        bulletE.GetComponent<EnemyBullet>().direction = new Vector3(1, 0, 0).normalized;

        GameObject bulletS = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, -90));
        bulletS.GetComponent<EnemyBullet>().direction = new Vector3(0, -1, 0).normalized;

        GameObject bulletW = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 180));
        bulletW.GetComponent<EnemyBullet>().direction = new Vector3(-1, 0, 0).normalized;
    }
}
