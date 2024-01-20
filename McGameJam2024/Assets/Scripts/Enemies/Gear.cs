using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // We want access to the GameManager functions from this class.
    [SerializeField] GameManager gameManager;

    // Enemy body
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    // Enemy fields
    private int health = 3;

    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 3 + gameManager.difficulty;
        rb.velocity = new Vector2(-2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

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
}
