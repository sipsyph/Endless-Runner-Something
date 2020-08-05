using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    public GameObject garbageControllerChildObj, currentGarbageControllerChildObj;
    public static GameObject garbageControllerObject, currentGarbageControllerChildObjStatic;

	void Start () {
        garbageControllerObject = this.transform.gameObject;
        CreateGarbageControllerChildObj();
        InvokeRepeating("DeleteCurrentGarbageControllerChild", 1, 1);
    }

    void CreateGarbageControllerChildObj()
    {
        currentGarbageControllerChildObj = currentGarbageControllerChildObjStatic = Instantiate(garbageControllerChildObj, this.transform);
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
