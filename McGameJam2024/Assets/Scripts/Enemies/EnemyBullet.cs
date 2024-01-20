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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("PlayerBullet") ||
            collision.collider.gameObject.CompareTag("Wall") ||
                collision.collider.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
