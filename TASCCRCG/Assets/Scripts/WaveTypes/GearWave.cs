using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearWave : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject gear;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
    }

    public void Wave()
    {
        Debug.Log("Gear Wave!");
        StartCoroutine(SpawnRoutine());
    }

    // Cooldown between wrenches during waves
    IEnumerator SpawnRoutine()
    {
        int gearNum = 1 + gameManager.difficulty / 2;

        for (int i = 0; i < gearNum; i++)
        {
            // Random Y position on game space
            float xPos = 7.5f;
            float yPos = 0f;

            GameObject.Instantiate(gear, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
