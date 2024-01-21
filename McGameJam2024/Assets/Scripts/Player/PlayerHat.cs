using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour
{
    private Player1 p1Script;
    [SerializeField] private playerManager playerManager;

    private void Start()
    {
        playerManager = p1Script.playerManager;
    }
    /* hat collisions have been disabled between:
        hat and hat
        hat and player
        hat and player bullets
        also top bar has no collider
    */

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("BigGear"))
        {
            this.gameObject.SetActive(false);
            playerManager.ChangeMode("basic");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("BigGear"))
        {
            this.gameObject.SetActive(false);
            playerManager.ChangeMode("basic");
        }
    }
}
