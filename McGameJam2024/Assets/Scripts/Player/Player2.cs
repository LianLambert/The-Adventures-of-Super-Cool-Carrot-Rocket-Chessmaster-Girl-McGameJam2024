using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    private Player1 p1Script;

    private float moveSpeed;
    private float scrollSpeed;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float damageCooldownTime;
    private GameObject bishopHat1;
    private GameObject rookHat1;
    private GameObject queenHat1;
    [SerializeField] private GameObject bishopHat2;
    [SerializeField] private GameObject rookHat2;
    [SerializeField] private GameObject queenHat2;
    private Rigidbody2D rb;
    private float damageCooldownTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("player1");
        p1Script = player1.GetComponent<Player1>();

        moveSpeed = p1Script.moveSpeed;
        scrollSpeed = p1Script.scrollSpeed;
        xMin = p1Script.xMin;
        xMax = p1Script.xMax;
        yMin = p1Script.yMin;
        yMax = p1Script.yMax;
        bishopHat1 = p1Script.bishopHat1;
        rookHat1 = p1Script.rookHat1;
        queenHat1 = p1Script.queenHat1;
        damageCooldownTime = p1Script.damageCooldownTime;

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

    // Keep BOTH OnTriggerEnter2D and OnCollisionEnter2D
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
        p1Script.ReduceHealth();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
