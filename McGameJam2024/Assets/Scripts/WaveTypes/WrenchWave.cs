using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WrenchWave : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject wrench;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wave()
    {
        Debug.Log("Wrench Wave!");
        int wrenchNum = 2 + gameManager.difficulty;
        for (int i = 0; i < wrenchNum; i++)
        {
            // Random Y position on game space
            float xPos = 10f;
            float yPos = UnityEngine.Random.Range(0,10) - 5f;

            GameObject.Instantiate(wrench, new Vector3(xPos, yPos, 0f),Quaternion.identity);

        }
    }

    // Cooldown between wrenches during waves
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
