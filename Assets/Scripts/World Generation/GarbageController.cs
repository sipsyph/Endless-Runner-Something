using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    public GameObject garbageControllerChildObj, currentGarbageControllerChildObj;

	void Start () {
        CreateGarbageControllerChildObj();
        InvokeRepeating("DeleteCurrentGarbageControllerChild", 1, 1);
    }

    void CreateGarbageControllerChildObj()
    {
        currentGarbageControllerChildObj = GeneratedAreaController.staticGarbageControllerObj = Instantiate(garbageControllerChildObj, this.transform);
    }

    void DeleteCurrentGarbageControllerChild()
    {
        if(currentGarbageControllerChildObj.transform.childCount >= 3)
        {
            Destroy(currentGarbageControllerChildObj);
            CreateGarbageControllerChildObj();
        }
        
    }
}
