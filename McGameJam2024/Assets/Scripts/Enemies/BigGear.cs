using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGear : MonoBehaviour
{
    // We want access to the GameManager functions from this class.
    [SerializeField] GameManager gameManager;

    // Enemy fields
    private int health = 4;
    private float shootingTime = 0f; 

    // Prefabs
    [SerializeField] GameObject saw;

    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 4 + gameManager.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        shootingTime += Time.deltaTime;

        // Moving in increments
        transform.position += new Vector3(-1f,0f,0f) * 1f * Time.deltaTime;

        // Shooting
        if (shootingTime >= 5f)
        {
            StartCoroutine(Shoot());
            shootingTime = 0f;
        }
    }

    IEnumerator Shoot()
    {
        for(int i=0; i<3; i++)
        {
            GameObject bulletNW = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 135));
            bulletNW.GetComponent<EnemyBullet>().direction = new Vector3(-1, 1, 0).normalized;

            GameObject bulletNE = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 45));
            bulletNE.GetComponent<EnemyBullet>().direction = new Vector3(1, 1, 0).normalized;

            GameObject bulletN = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 90));
            bulletN.GetComponent<EnemyBullet>().direction = new Vector3(0, 1, 0).normalized;

            GameObject bulletE = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 0));
            bulletE.GetComponent<EnemyBullet>().direction = new Vector3(1, 0, 0).normalized;

            GameObject bulletW = Instantiate(saw, transform.position, Quaternion.Euler(0, 0, 180));
            bulletW.GetComponent<EnemyBullet>().direction = new Vector3(-1, 0, 0).normalized;

            gameManager.playerManager.AddScore(75);
            yield return new WaitForSeconds(0.33f);
        }
        

    }
    IEnumerator OnHit()
    {
        // Behaviour when the enemy is hit
        if (health > 0)
        {
            health--;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            OnDestroyed();
        }
    }
    // Behaviour when the enemy is destroyed
    private void OnDestroyed()
    {
        gameManager.playerManager.AddScore(200);
        gameManager.EnemyDown(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            StartCoroutine(OnHit());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (collision.GetComponent<Collider2D>().gameObject.CompareTag("PlayerBullet"))
        {
            StartCoroutine(OnHit());
        }
    }
}
