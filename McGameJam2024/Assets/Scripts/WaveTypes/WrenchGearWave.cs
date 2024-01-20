using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchGearWave : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject gear;
    [SerializeField] GameObject wrench;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
    }

    public void Wave()
    {
        Debug.Log("Wrench and Gear Wave!");
        StartCoroutine(SpawnRoutine());
    }

    // Cooldown between wrenches during waves
    IEnumerator SpawnRoutine()
    {
        int num = 1 + gameManager.difficulty;

        for (int i = 0; i < num; i++)
        {
            if(i % 2  == 1) {
                // Random Y position on game space
                float xPos = 7.5f;
                float yPos = 0f;

                GameObject.Instantiate(gear, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            }
            else
            {
         
                // Random Y position on game space
                float xPos = 10f;
                float yPos = UnityEngine.Random.Range(0f, 7f) - 3.15f;

                GameObject.Instantiate(wrench, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
