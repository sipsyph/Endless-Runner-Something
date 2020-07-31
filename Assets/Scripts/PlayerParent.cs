using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ConstantForwardMovement();
    }

    void ConstantForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
