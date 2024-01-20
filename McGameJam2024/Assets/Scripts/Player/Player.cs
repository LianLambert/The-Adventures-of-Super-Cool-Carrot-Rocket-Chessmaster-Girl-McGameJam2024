using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float scrollSpeed = 2.5f;
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

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }


    void UpdateMovement() {
        Vector3 pos = transform.position;
        pos.x -= scrollSpeed * Time.deltaTime;


        if (Input.GetKey("w"))
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // hat collisions
        if (other.gameObject.CompareTag("bishopPowerUp"))
        {
            
            bishopHat.SetActive(true);
            rookHat.SetActive(false);
            queenHat.SetActive(false);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("rookPowerUp"))
        {
            Destroy(other.gameObject);
            bishopHat.SetActive(false);
            rookHat.SetActive(true);
            queenHat.SetActive(false);
        }
        else if (other.gameObject.CompareTag("queenPowerUp"))
        {
            Destroy(other.gameObject);
            bishopHat.SetActive(false);
            rookHat.SetActive(false);
            queenHat.SetActive(true);
        }
    }
}
