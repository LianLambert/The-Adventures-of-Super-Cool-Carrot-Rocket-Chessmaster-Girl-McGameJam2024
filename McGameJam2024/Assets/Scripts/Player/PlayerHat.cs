using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHat : MonoBehaviour
{
    /* hat collisions have been disabled between:
        hat and hat
        hat and player
        hat and player bullets
        also top bar has no collider
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("StickyItem"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
