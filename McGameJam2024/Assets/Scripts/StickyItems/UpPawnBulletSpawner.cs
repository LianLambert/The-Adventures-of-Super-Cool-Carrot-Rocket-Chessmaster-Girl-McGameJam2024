using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPawnBulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject diagonalPlayerBullet;
    [SerializeField] private float bulletTime = 2.0f;
    [SerializeField] private float bulletCooldownTime = 0.25f;
    private float bulletCooldownTimer = 0.0f;
    private bool shooting = false;

    [SerializeField] private bool basicShots = true;
    [SerializeField] private bool rookShots = false;
    [SerializeField] private bool bishopShots = false;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<PawnBulletSpawner>().attachedToPlayer)
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

        // up pawn
        GameObject bulletNE = Instantiate(diagonalPlayerBullet, transform.position, Quaternion.Euler(0, 0, 0));
        bulletNE.GetComponent<PlayerBullet>().direction = new Vector3(1, 1, 0).normalized;
        Destroy(bulletNE, bulletTime);

    }
}
