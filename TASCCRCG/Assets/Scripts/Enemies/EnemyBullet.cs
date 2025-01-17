using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Bullet fields
    public Vector3 direction;
    [SerializeField] float bulletSpeed = 3f;

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("PlayerBullet") ||
                collision.collider.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") ||
               collision.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
