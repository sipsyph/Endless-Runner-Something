using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERSUtilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DeleteThis(Transform transformObj)
    {
        transformObj.SetParent(GarbageController.currentGarbageControllerChildObjStatic.transform);
        transformObj.gameObject.SetActive(false);
    }
}
