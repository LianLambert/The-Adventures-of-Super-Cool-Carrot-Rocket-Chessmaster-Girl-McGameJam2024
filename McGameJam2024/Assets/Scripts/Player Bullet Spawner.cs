using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletCooldownTime = 0.05f;
    private float bulletCooldownTimer = 0.0f;
    private bool shooting = false;

    public bool basicShots = true;
    public bool rookShots = false;
    public bool bishopShots = false;



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
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        if (rookShots)
        {

        }
        if (bishopShots)
        {

        }

        

    }
}
