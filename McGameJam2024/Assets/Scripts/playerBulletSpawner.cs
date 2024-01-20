using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private float bulletCooldownTime = 0.05f;
    private float bulletCooldownTimer = 0.0f;
    private bool shooting = false;

    [SerializeField] public bool basicShots = true;
    [SerializeField] public bool rookShots = false;
    [SerializeField] public bool bishopShots = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckShooting();
        UpdateTimers();
        if (shooting && bulletCooldownTimer <= 0)
        {
            Shoot();
        }
    }

    void CheckShooting()
    {
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

        if (basicShots)
        {
            GameObject bullet0 = Instantiate(playerBullet, transform.position, Quaternion.identity);
            bullet0.GetComponent<playerBullet>().direction = new Vector3(1, 0, 0).normalized;
        }
        if (rookShots)
        {
            GameObject bulletN = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, 90));
            bulletN.GetComponent<playerBullet>().direction = new Vector3(0, 1, 0).normalized;

            GameObject bulletE = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, 0));
            bulletE.GetComponent<playerBullet>().direction = new Vector3(1, 0, 0).normalized;

            GameObject bulletS = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, -90));
            bulletS.GetComponent<playerBullet>().direction = new Vector3(0, -1, 0).normalized;

            GameObject bulletW = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, 180));
            bulletW.GetComponent<playerBullet>().direction = new Vector3(-1, 0, 0).normalized;
        }
        if (bishopShots)
        {
            GameObject bulletNW = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, 135));
            bulletNW.GetComponent<playerBullet>().direction = new Vector3(-1, 1, 0).normalized;

            GameObject bulletNE = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, 45));
            bulletNE.GetComponent<playerBullet>().direction = new Vector3(1, 1, 0).normalized;

            GameObject bulletSE = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, -45));
            bulletSE.GetComponent<playerBullet>().direction = new Vector3(1, -1, 0).normalized;

            GameObject bulletSW = Instantiate(playerBullet, transform.position, Quaternion.Euler(0, 0, -135));
            bulletSW.GetComponent<playerBullet>().direction = new Vector3(-1, -1, 0).normalized;
        }

        

    }
}
