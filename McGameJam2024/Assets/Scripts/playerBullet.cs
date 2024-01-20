using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colided with: " + collision.gameObject);
        Destroy(this.gameObject);
    }
}
