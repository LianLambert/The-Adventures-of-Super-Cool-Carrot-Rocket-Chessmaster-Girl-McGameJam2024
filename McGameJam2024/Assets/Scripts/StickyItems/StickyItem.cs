using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    public bool stuckToPlayer = false;
    private playerManager playerManager;
    private GameObject bishopHat;
    private GameObject rookHat;
    private GameObject queenHat;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("playerManager").GetComponent<playerManager>(); ;
    }

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D

    // assuming StickyItems are NOT triggers but that enemies and enemy bullets are
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if already attached to player
        if (stuckToPlayer)
        {
            // hat collisions
            if (collision.gameObject.CompareTag("bishopPowerUp"))
            {
                playerManager.ChangeMode("bishop");
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.CompareTag("rookPowerUp"))
            {
                playerManager.ChangeMode("rook");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("queenPowerUp"))
            {
                playerManager.ChangeMode("queen");
                Destroy(collision.gameObject);
            }
            // if hit by an enemy or enemy bullet, destroy it along with its chilren
            if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Wrench") || collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("BigGear"))
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
                this.gameObject.GetComponent<PawnBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                this.gameObject.GetComponent<PawnBulletSpawner>().attachedToPlayer = true;
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
            // hat collisions
            if (collision.gameObject.CompareTag("bishopPowerUp"))
            {
                playerManager.ChangeMode("bishop");
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.CompareTag("rookPowerUp"))
            {
                playerManager.ChangeMode("rook");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("queenPowerUp"))
            {
                playerManager.ChangeMode("queen");
                Destroy(collision.gameObject);
            }
            // if hit by an enemy or enemy bullet, destroy it along with its chilren
            if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Wrench") || collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("BigGear"))
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
                this.gameObject.GetComponent<PawnBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                this.gameObject.GetComponent<PawnBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;
            }
        }
    }
}