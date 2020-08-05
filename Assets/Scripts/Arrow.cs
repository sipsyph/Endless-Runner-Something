using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody myBody;
    private float lifeTimer = 2f, timer;
    private bool hitSomething = false;

    private int ctr;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(myBody.velocity);

    }

    // Update is called once per frame
    void Update()
    {
        if(!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
        }else{
            ctr++;
            if(ctr>=120)
            {
                ctr = 0;
                this.transform.gameObject.SetActive(false);
                this.transform.parent = GarbageController.currentGarbageControllerChildObjStatic.transform;
            }
        }
        
    }

    private void OnCollisionEnter(Collision col)
    {
        
        if(col.collider.tag != "Projectile")
        {
            hitSomething = true;
            this.transform.parent = col.transform;
            myBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
