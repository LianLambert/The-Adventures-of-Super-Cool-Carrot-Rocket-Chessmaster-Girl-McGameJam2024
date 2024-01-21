using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    private playerManager playerManager;

    [SerializeField] private GameObject straightPlayerBullet;
    [SerializeField] private GameObject diagonalPlayerBullet;
    [SerializeField] public bool attachedToPlayer = false;

    [SerializeField] private float bulletTime = 10.0f;
    [SerializeField] private float bulletCooldownTime = 0.05f;
    private float bulletCooldownTimer = 0.0f;
    private bool shooting = false;

    

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("playerManager").GetComponent<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedToPlayer)
        {
            UpdateTimers();
            CheckShooting();

            if (shooting && bulletCooldownTimer <= 0)
            {
                Shoot();
            }
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

    void UpdateTimers()
    {
        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        bulletCooldownTimer = bulletCooldownTime;

        if (playerManager.mode == "basic")
        {
            GameObject bullet0 = Instantiate(straightPlayerBullet, transform.position, Quaternion.identity);
            bullet0.GetComponent<PlayerBullet>().direction = new Vector3(1, 0, 0).normalized;
            Destroy(bullet0, bulletTime);
        }
        if (playerManager.mode == "rook")
        {
            GameObject bulletN = Instantiate(straightPlayerBullet, transform.position, Quaternion.Euler(0, 0, 90));
            bulletN.GetComponent<PlayerBullet>().direction = new Vector3(0, 1, 0).normalized;
            Destroy(bulletN, bulletTime);

            GameObject bulletE = Instantiate(straightPlayerBullet, transform.position, Quaternion.Euler(0, 0, 0));
            bulletE.GetComponent<PlayerBullet>().direction = new Vector3(1, 0, 0).normalized;
            Destroy(bulletE, bulletTime);

            GameObject bulletS = Instantiate(straightPlayerBullet, transform.position, Quaternion.Euler(0, 0, -90));
            bulletS.GetComponent<PlayerBullet>().direction = new Vector3(0, -1, 0).normalized;
            Destroy(bulletS, bulletTime);

            GameObject bulletW = Instantiate(straightPlayerBullet, transform.position, Quaternion.Euler(0, 0, 180));
            bulletW.GetComponent<PlayerBullet>().direction = new Vector3(-1, 0, 0).normalized;
            Destroy(bulletW, bulletTime);
        }
        if (playerManager.mode == "bishop")
        {
            GameObject bulletNW = Instantiate(diagonalPlayerBullet, transform.position, Quaternion.Euler(0, 0, 90));
            bulletNW.GetComponent<PlayerBullet>().direction = new Vector3(-1, 1, 0).normalized;
            Destroy(bulletNW, bulletTime);

            GameObject bulletNE = Instantiate(diagonalPlayerBullet, transform.position, Quaternion.Euler(0, 0, 0));
            bulletNE.GetComponent<PlayerBullet>().direction = new Vector3(1, 1, 0).normalized;
            Destroy(bulletNE, bulletTime);

            GameObject bulletSE = Instantiate(diagonalPlayerBullet, transform.position, Quaternion.Euler(0, 0, -90));
            bulletSE.GetComponent<PlayerBullet>().direction = new Vector3(1, -1, 0).normalized;
            Destroy(bulletSE, bulletTime);

            GameObject bulletSW = Instantiate(diagonalPlayerBullet, transform.position, Quaternion.Euler(0, 0, 180));
            bulletSW.GetComponent<PlayerBullet>().direction = new Vector3(-1, -1, 0).normalized;
            Destroy(bulletSW, bulletTime);
        }

        

    }
}
