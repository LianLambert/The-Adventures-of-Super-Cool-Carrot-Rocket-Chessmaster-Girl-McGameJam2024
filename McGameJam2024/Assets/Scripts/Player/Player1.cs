using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // info for player2
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
        else if(playerManager.mode == "bishopPowerUp")
        {
            bishopHat1.SetActive(true);
            rookHat1.SetActive(false);
            queenHat1.SetActive(false);
        }
        else if (playerManager.mode == "rookPowerUp")
        {
            bishopHat1.SetActive(false);
            rookHat1.SetActive(true);
            queenHat1.SetActive(false);
        }
        else if (playerManager.mode == "queenPowerUp")
        {
            bishopHat1.SetActive(false);
            rookHat1.SetActive(false);
            queenHat1.SetActive(true);
        }
    }
}