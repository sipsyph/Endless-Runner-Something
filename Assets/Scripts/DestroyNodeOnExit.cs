using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNodeOnExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetNode()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            ResetNode();
        }
    }
}
