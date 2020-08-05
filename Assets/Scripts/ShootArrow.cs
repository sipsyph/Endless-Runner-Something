using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public Camera camera;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce = 10f;

    private int ctr;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleShootInterval();

    }

    void Shoot()
    {
            GameObject go = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.velocity = camera.transform.forward * shootForce;
    }

    void HandleShootInterval()
    {
        if(canShoot)
        {
            ctr++;
            if(ctr>=60)
            {
                Shoot();
                ctr = 0;
            }
        }
    }
}
