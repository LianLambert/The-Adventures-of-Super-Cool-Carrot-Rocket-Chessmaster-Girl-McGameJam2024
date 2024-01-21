using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private GameObject bishopHat2;
    [SerializeField] private GameObject rookHat2;
    [SerializeField] private GameObject queenHat2;

    [SerializeField] private GameObject player1;
    private Player1 p1Script;
    private playerManager playerManager;

    private Rigidbody2D rb;
    private float damageCooldownTimer = 0.0f;
    public bool shooting = false;

    // get info from player1
    private float damageCooldownTime;
    private float moveSpeed;
    private float scrollSpeed;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    


    // Start is called before the first frame update
    void Start()
    {
        // player1 reference
        p1Script = player1.GetComponent<Player1>();
        p1Script.lastLife.AddListener(() => StartCoroutine(BlinkRed()));

        // playerManager script reference
        playerManager = p1Script.playerManager;
        playerManager.OnModeChanged.AddListener(HandleModeChanged);

        // rigid body
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // get info from player1
        damageCooldownTime = p1Script.damageCooldownTime;
        moveSpeed = p1Script.moveSpeed;
        scrollSpeed = p1Script.scrollSpeed;
        xMin = p1Script.xMin;
        xMax = p1Script.xMax;
        yMin = p1Script.yMin;
        yMax = p1Script.yMax;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        UpdateMovement();
        CheckShooting();
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


        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
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
            playerManager.ChangeMode("bishop");
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("rookPowerUp"))
        {
            playerManager.ChangeMode("rook");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("queenPowerUp"))
        {
            playerManager.ChangeMode("queen");
            Destroy(other.gameObject);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // hat collisions
        if (other.gameObject.CompareTag("bishopPowerUp"))
        {
            playerManager.ChangeMode("bishop");
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("rookPowerUp"))
        {
            playerManager.ChangeMode("rook");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("queenPowerUp"))
        {
            playerManager.ChangeMode("queen");
            Destroy(other.gameObject);
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

    private void HandleModeChanged()
    {
        if (playerManager.mode == "basic")
        {
            bishopHat2.SetActive(false);
            rookHat2.SetActive(false);
            queenHat2.SetActive(false);
        }
        else if (playerManager.mode == "bishop")
        {
            bishopHat2.SetActive(true);
            rookHat2.SetActive(false);
            queenHat2.SetActive(false);
        }
        else if (playerManager.mode == "rook")
        {
            bishopHat2.SetActive(false);
            rookHat2.SetActive(true);
            queenHat2.SetActive(false);
        }
        else if (playerManager.mode == "queen")
        {
            bishopHat2.SetActive(false);
            rookHat2.SetActive(false);
            queenHat2.SetActive(true);
        }
    }

    void CheckShooting()
    {
        // check if the player is holding the shooting key
        if (Input.GetKeyDown(KeyCode.Period))
        {
            shooting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Period))
        {
            shooting = false;
        }
    }

    IEnumerator BlinkRed()
    {
        // Store the original color
        Color originalColor = this.gameObject.GetComponent<SpriteRenderer>().color;

        // Set the sprite to red
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 162f / 255f, 162f / 255f); // FFA2A2

        // Wait for one second
        yield return new WaitForSeconds(0.1f);

        // Restore the original color
        this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
