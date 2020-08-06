using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public Camera camera;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce;

    private int ctr, indicatorCtr;

    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerParent.projectileIncomingIndicatorStatic.activeSelf)
        {
            indicatorCtr++;
            if(indicatorCtr>=60)
            {
                indicatorCtr=0;
                PlayerParent.projectileIncomingIndicatorStatic.SetActive(false);
            }
        }
        HandleShootInterval();

    }

    void Shoot()
    {
        PlayerParent.projectileIncomingIndicatorStatic.SetActive(true);

        camera.transform.localRotation = Quaternion.Euler(0,DetermineTargetSlot(),0);
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = camera.transform.forward * shootForce;
        

    }

    private float DetermineTargetSlot()
    {
        int randNum = Random.Range(1,100);
        if(randNum>=1 && randNum<=33)
        {
            PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition = new Vector3(
                -0.319f,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.y,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.z);
            return 4.23f;
        }
        else if(randNum>33 && randNum<=66)
        {
            PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition = new Vector3(
                0.356f,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.y,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.z);
            return -2f;
        }
        else
        {
            PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition = new Vector3(
                0f,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.y,
                PlayerParent.projectileIncomingIndicatorStatic.transform.localPosition.z);
            return 0.4f;
        }

        
    }

    void HandleShootInterval()
    {
        if(canShoot)
        {
            ctr++;
            if(ctr>=120)
            {
                Shoot();
                ctr = 0;
            }
        }
    }
}
