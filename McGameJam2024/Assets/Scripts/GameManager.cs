using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    // Audio Clips

    // Game Fields
    public float timer = 0f;
    public int difficulty = 0;
    public float waveTimer = 8f;
    public float waveRate = 9f;
    public int powerupRate = 2;
    public int weaponRate = 2;
    public int waveCounter = 0;

    // Prefabs
    [SerializeField] GameObject bishopPowerup;
    [SerializeField] GameObject rookPowerup;
    [SerializeField] GameObject queenPowerup;

    // Wave Types
    [SerializeField] WrenchWave wrenchWave;
    [SerializeField] GearWave gearWave;
    [SerializeField] WrenchGearWave wrenchgear;

    // Start is called before the first frame update
    void Start()
    {
        // Getting waveTypes
        wrenchWave = GetComponent<WrenchWave>();
        gearWave = GetComponent<GearWave>();
        wrenchgear = GetComponent<WrenchGearWave>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updating fields with current game time
        timer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        difficulty = (int)Math.Clamp((timer / 15f),0f,7f);
        waveRate = 2 + (7 - difficulty);

        // Looking if an enemy should be spawned
        if (waveTimer >= waveRate)
        {
            waveTimer = 0f;
            string waveType = WaveSelector();

            // Check which wave was selected
            switch(waveType)
            {
                case "wrench":
                    wrenchWave.Wave();
                    break;

                case "gear":
                    gearWave.Wave();
                    break;

                case "wrenchgear":
                    wrenchgear.Wave();
                    break;

            }
        }
    }

    public void EnemyDown(GameObject enemy)
    {
        // Enemy transform
        Vector3 enemyPos = enemy.transform.position;
        Quaternion enemyQuat = enemy.transform.rotation;

        // Enemies can drop weapons
        int randomIntWpn = UnityEngine.Random.Range(1, 11);

        // Weapons roll first
        if (randomIntWpn <= weaponRate)
        {
            // Spawn weapon
        }

        // Enemies can spawn powerups
        else
        {
            int randomIntPwrUp = UnityEngine.Random.Range(1, 11);

            // Powerup roll 
            if (randomIntPwrUp <= powerupRate)
            {
                // Making the queen powerup rarer
                int rng = UnityEngine.Random.Range(1, 101);

                // Rook powerup
                if(rng <= 45)
                {
                    GameObject.Instantiate(rookPowerup, enemyPos, enemyQuat);
                }

                // Bishop powerup
                if(rng <= 90 && rng > 45)
                {
                    GameObject.Instantiate(bishopPowerup, enemyPos, enemyQuat);
                }

                // Queen powerup
                if(rng > 90)
                {
                    GameObject.Instantiate(queenPowerup,  enemyPos, enemyQuat);
                }
            }

        }
    }


    private String WaveSelector()
    {
        string waveType;
        int rng = UnityEngine.Random.Range(0, 100);

        // Mixed waves
        if (rng <= 2 * difficulty)
        {
            waveType = "all";
        }

        else if (rng <= 5 * difficulty + 15)
        {
            waveType = "wrenchgear";
        }

        if (rng <= 7 * difficulty + 20)
        {
            waveType = "gear";
        }

        else
            waveType = "wrench";

        waveCounter++;
        return waveType;
    }

}
