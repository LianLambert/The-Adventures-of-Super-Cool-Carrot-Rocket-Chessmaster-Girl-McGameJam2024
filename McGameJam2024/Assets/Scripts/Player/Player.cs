using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;
    [SerializeField] private GameObject life4;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float scrollSpeed = 2.5f;
    [SerializeField] private float xMin = -5.28f;
    [SerializeField] private float xMax = 5.04f;
    [SerializeField] private float yMin = -3.85f;
    [SerializeField] private float yMax = 3.55f;
    private GameObject playerManager;
    private GameObject bishopHat;
    private GameObject rookHat;
    private GameObject queenHat;
    private Rigidbody2D rb;
    [SerializeField] private int playerHealth = 4;
    [SerializeField] private float damageCooldownTime = 0.05f;
    private float damageCooldownTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("playerManager");
        bishopHat = playerManager.GetComponent<playerManager>().bishopHat;
        rookHat = playerManager.GetComponent<playerManager>().rookHat;
        queenHat = playerManager.GetComponent<playerManager>().queenHat;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        UpdateMovement();
    }

    void UpdateTimers()
    {
        if (damageCooldownTimer > 0)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
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

        // Clamp the position values
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);

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
        // enemy collisions (take damage)
        else if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("BigGear"))
        {
            if(damageCooldownTimer <= 0)
            {
                StartCoroutine(OnHit());
            }
                
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
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
        // enemy collisions (take damage)
        else if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("BigGear"))
        {
            if (damageCooldownTimer <= 0)
            {
                StartCoroutine(OnHit());
            }

        }
    }

    // blink when hit
    IEnumerator OnHit()
    {
        damageCooldownTimer = damageCooldownTime;
        ReduceHealth();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // update health(battery)
    private void ReduceHealth()
    {
        playerHealth -= 1;

        if(playerHealth == 4)
        {
            life4.SetActive(false);
        }
        else if (playerHealth == 3)
        {
            life3.SetActive(false);
        }
        else if (playerHealth == 2)
        {
            life2.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            life1.SetActive(false);
            playerManager.GetComponent<playerManager>().GameOver();
        }
    }
}
