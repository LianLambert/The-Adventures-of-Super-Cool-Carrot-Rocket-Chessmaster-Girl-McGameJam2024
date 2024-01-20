using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public float bulletCooldownTime = 0.05f;
    public float bulletCooldownTimer = 0.0f;
    public bool basicShots = true;
    public bool rookShots = false;
    public bool bishopShots = false;
    private bool shooting = false;
    
    

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
        bulletCooldownTimer -= Time.deltaTime;
    }

    void Shoot()
    {

    }
}
