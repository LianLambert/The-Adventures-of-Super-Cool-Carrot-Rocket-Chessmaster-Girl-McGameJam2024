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
    public float waveTimer = 6f;
    public float waveRate = 8f;
    public int powerupRate = 2;
    public int weaponRate = 2;

    // Wave Types
    [SerializeField] WrenchWave wrenchWave;

    // Start is called before the first frame update
    void Start()
    {
        // Getting waveTypes
        wrenchWave = GetComponent<WrenchWave>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updating fields with current game time
        timer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        difficulty = (int)Math.Clamp((timer / 15),0f,10f);
        waveRate = 1 + (7 - difficulty);

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
                

            }
        }
    }

    public void EnemyDown(GameObject enemy)
    {
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
                // Spawn powerup
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

        else
            waveType = "wrench";

        return waveType;
    }

}
