using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public bool stuckToPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // NOTE: used both OnTriggerEnter2D and OnCollisionEnter2D just to make sure to cover all bases

    // assuming StickyItems are NOT triggers but that enemies and enemy bullets are
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if already attached to player
        if (stuckToPlayer)
        {
            // if hit by an enemy or enemy bullet, destroy it along with its chilren
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Wrench"))
            {
                Destroy(this.gameObject);
            }
        }
        // if not already attached to player
        else
        {
            // if encounters the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // make this object a child of the player
                transform.SetParent(collision.gameObject.transform);
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                stuckToPlayer = true;
            }
        }
        
    }

    // assuming StickyItems are NOT triggers but that enemies and enemy bullets are
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if already attached to player
        if (stuckToPlayer)
        {
            // if hit by an enemy or enemy bullet, destroy it along with its chilren
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Wrench"))
            {
                Destroy(this.gameObject);
            }
        }
        // if not already attached to player
        else
        {
            // if encounters the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // make this object a child of the player
                transform.SetParent(collision.gameObject.transform);
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                stuckToPlayer = true;
            }
        }

    }
}
