using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGearWave : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject bigGear;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
    }

    public void Wave()
    {
        Debug.Log("BigGear Wave!");
        StartCoroutine(SpawnRoutine());
    }

    // Cooldown between big gears during waves
    IEnumerator SpawnRoutine()
    {
        int wrenchNum = 1 + gameManager.difficulty / 4;

        for (int i = 0; i < wrenchNum; i++)
        {
            // Random Y position on game space
            float xPos = 8f;
            float yPos = -4.3f;

            GameObject.Instantiate(bigGear, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
