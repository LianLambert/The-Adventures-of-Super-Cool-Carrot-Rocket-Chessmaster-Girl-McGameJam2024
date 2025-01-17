using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [SerializeField] private Renderer red;
    [SerializeField] private float horizontal_speed;

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * horizontal_speed, 0f);
        red.material.mainTextureOffset = offset;
    }
}
