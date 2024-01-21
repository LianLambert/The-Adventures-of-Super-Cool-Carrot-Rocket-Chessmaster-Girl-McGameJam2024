using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    // We want access to the GameManager functions from this class.
    [SerializeField] GameManager gameManager;

    // Enemy body
    [SerializeField] Rigidbody2D rb;

    // Enemy fields
    private int health = 2;

    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 2 + gameManager.difficulty;
        rb.velocity = new Vector2(-2f, 0f);
    }

    // Behaviour when the enemy is hit
    IEnumerator OnHit()
    {
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
        gameManager.playerManager.AddScore(50);
        gameManager.EnemyDown(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("EnemyWall"))
        {
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerBullet"))
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
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("PlayerBullet"))
        {
            StartCoroutine(OnHit());
        }
        // for debugging only
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Wrench registered collision with player");
        }
    }
}
