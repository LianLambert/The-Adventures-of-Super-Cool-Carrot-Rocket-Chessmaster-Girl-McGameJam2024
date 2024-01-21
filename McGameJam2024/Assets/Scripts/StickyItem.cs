using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    public bool stuckToPlayer = false;
    private GameObject playerManager;
    private GameObject bishopHat;
    private GameObject rookHat;
    private GameObject queenHat;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("playerManager");
        bishopHat = playerManager.GetComponent<playerManager>().bishopHat;
        rookHat = playerManager.GetComponent<playerManager>().rookHat;
        queenHat = playerManager.GetComponent<playerManager>().queenHat;
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

                bishopHat.SetActive(true);
                rookHat.SetActive(false);
                queenHat.SetActive(false);
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.CompareTag("rookPowerUp"))
            {
                Destroy(collision.gameObject);
                bishopHat.SetActive(false);
                rookHat.SetActive(true);
                queenHat.SetActive(false);
            }
            else if (collision.gameObject.CompareTag("queenPowerUp"))
            {
                Destroy(collision.gameObject);
                bishopHat.SetActive(false);
                rookHat.SetActive(false);
                queenHat.SetActive(true);
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
                this.gameObject.GetComponent<PlayerBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                this.gameObject.GetComponent<PlayerBulletSpawner>().attachedToPlayer = true;
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

                bishopHat.SetActive(true);
                rookHat.SetActive(false);
                queenHat.SetActive(false);
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.CompareTag("rookPowerUp"))
            {
                Destroy(collision.gameObject);
                bishopHat.SetActive(false);
                rookHat.SetActive(true);
                queenHat.SetActive(false);
            }
            else if (collision.gameObject.CompareTag("queenPowerUp"))
            {
                Destroy(collision.gameObject);
                bishopHat.SetActive(false);
                rookHat.SetActive(false);
                queenHat.SetActive(true);
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
                this.gameObject.GetComponent<PlayerBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;

            }

            // if encounters a sticky item that IS attached to the player
            if (collision.gameObject.CompareTag("StickyItem") && collision.gameObject.GetComponent<StickyItem>().stuckToPlayer)
            {
                // make this object a child of the other StickyItem
                transform.SetParent(collision.gameObject.transform);
                this.gameObject.GetComponent<PlayerBulletSpawner>().attachedToPlayer = true;
                stuckToPlayer = true;
            }
        }
    }
}