using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player1 : MonoBehaviour
{

    [SerializeField] public GameObject bishopHat1;
    [SerializeField] public GameObject rookHat1;
    [SerializeField] public GameObject queenHat1;

    [SerializeField] public int playerHealth = 4;
    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;
    [SerializeField] private GameObject life4;

    private Rigidbody2D rb;
    private float damageCooldownTimer = 0.0f;
    public bool shooting = false;

    // info for player2
    public UnityEvent lastLife;
    public playerManager playerManager;

    [SerializeField] public float damageCooldownTime = 0.05f;
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] public float scrollSpeed = 2.5f;
    [SerializeField] public float xMin = -5.28f;
    [SerializeField] public float xMax = 5.04f;
    [SerializeField] public float yMin = -3.85f;
    [SerializeField] public float yMax = 3.55f;

    // Awake is called before Start() so that reference is initialized
    void Awake()
    {
        // playerManager script reference
        playerManager = GameObject.Find("playerManager").GetComponent<playerManager>();
        playerManager.OnModeChanged.AddListener(HandleModeChanged);

        // rigid body
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            playerManager.ChangeMode("bishop");
            playerManager.AddScore(10);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("rookPowerUp"))
        {
            playerManager.ChangeMode("rook");
            playerManager.AddScore(10);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("queenPowerUp"))
        {
            playerManager.ChangeMode("queen");
            playerManager.AddScore(10);
            Destroy(other.gameObject);
        }
        // enemy collisions (take damage)
        else if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("Big Gear"))
        {
            Debug.Log("collided with: " + other.gameObject);
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
            playerManager.AddScore(10);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("rookPowerUp"))
        {
            playerManager.ChangeMode("rook");
            playerManager.AddScore(10);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("queenPowerUp"))
        {
            playerManager.ChangeMode("queen");
            playerManager.AddScore(10);
            Destroy(other.gameObject);
        }
        // enemy collisions (take damage)
        else if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("Wrench") || other.gameObject.CompareTag("Gear") || other.gameObject.CompareTag("BigGear"))
        {
            Debug.Log("collided with: " + other.gameObject);
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
    public void ReduceHealth()
    {
        playerHealth -= 1;

        if(playerHealth == 3)
        {
            life4.SetActive(false);
        }
        else if (playerHealth == 2)
        {
            life3.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            life2.SetActive(false); 
            lastLife.Invoke(); 
            StartCoroutine(BlinkRed());
        }
        else if (playerHealth == 0)
        {
            life1.SetActive(false);
            playerManager.GetComponent<playerManager>().GameOver();
        }
    }

    private void HandleModeChanged()
    {
        if (playerManager.mode == "basic")
        {
            bishopHat1.SetActive(false);
            rookHat1.SetActive(false);
            queenHat1.SetActive(false);
        }
        else if(playerManager.mode == "bishop")
        {
            bishopHat1.SetActive(true);
            rookHat1.SetActive(false);
            queenHat1.SetActive(false);
        }
        else if (playerManager.mode == "rook")
        {
            bishopHat1.SetActive(false);
            rookHat1.SetActive(true);
            queenHat1.SetActive(false);
        }
        else if (playerManager.mode == "queen")
        {
            bishopHat1.SetActive(false);
            rookHat1.SetActive(false);
            queenHat1.SetActive(true);
        }
    }

    void CheckShooting()
    {
        // check if the player is holding the shooting key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            shooting = false;
        }
    }

    IEnumerator BlinkRed()
    {
        int blinkCount = 3;
        Color originalColor = this.gameObject.GetComponent<SpriteRenderer>().color;

        for (int i = 0; i < blinkCount; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 162f / 255f, 162f / 255f); // FFA2A2
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
