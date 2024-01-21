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
    public float powerupTimer = 3f;

    // Player Manager
    [SerializeField] public playerManager playerManager;

    // Prefabs
    [SerializeField] GameObject bishopPowerup;
    [SerializeField] GameObject rookPowerup;
    [SerializeField] GameObject queenPowerup;
    [SerializeField] GameObject pawnUp;
    [SerializeField] GameObject pawnDown;

    // Wave Types
    [SerializeField] WrenchWave wrenchWave;
    [SerializeField] GearWave gearWave;
    [SerializeField] WrenchGearWave wrenchgear;
    [SerializeField] BigGearWave bigGear;


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
                    playerManager.Wave(waveCounter);
                    break;

                case "gear":
                    gearWave.Wave();
                    playerManager.Wave(waveCounter);
                    break;

                case "wrenchgear":
                    wrenchgear.Wave();
                    playerManager.Wave(waveCounter);
                    break;

                case "biggear":
                    bigGear.Wave();
                    playerManager.Wave(waveCounter);
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
            int rng = UnityEngine.Random.Range(1, 101);

            // Up pawn weapon
            if(rng <= 51)
            {
                GameObject pUp = GameObject.Instantiate(pawnUp, enemyPos, enemyQuat);
            }
            else
            {
                GameObject pDown = GameObject.Instantiate(pawnDown, enemyPos, enemyQuat);
            }
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
                    GameObject powerup = GameObject.Instantiate(rookPowerup, enemyPos, enemyQuat);
                    Destroy(powerup, powerupTimer);
                }

                // Bishop powerup
                if(rng <= 90 && rng > 45)
                {
                    GameObject powerup = GameObject.Instantiate(bishopPowerup, enemyPos, enemyQuat);
                    Destroy(powerup, powerupTimer);
                }

                // Queen powerup
                if(rng > 90)
                {
                    GameObject powerup = GameObject.Instantiate(queenPowerup, enemyPos, enemyQuat);
                    Destroy(powerup, powerupTimer);
                }
            }

        }
    }


    private String WaveSelector()
    {
        string waveType;
        int rng = UnityEngine.Random.Range(0, 100);

        // Mixed wave
        if (rng <= 3 * difficulty + 15)
        {
            waveType = "wrenchgear";
        }

        // Solo waves
        else if(rng <= 3 * difficulty + 20)
        {
            waveType = "biggear";
        }

        else if (rng <= 5 * difficulty + 20)
        {
            waveType = "gear";
        }

        else
            waveType = "wrench";

        waveCounter++;
        return waveType;
    }

}
