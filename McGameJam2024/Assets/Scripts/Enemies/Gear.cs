using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // We want access to the GameManager functions from this class.
    [SerializeField] GameManager gameManager;

    // Enemy fields
    private int health = 2;
    private float time = 0f;

    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 2 + gameManager.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float x = 2 * time;
        float y = (float) (2*(Math.Sin(x)));
        transform.position =  new Vector3(10f,0f,0f) - new Vector3(x, y, 0f);
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
