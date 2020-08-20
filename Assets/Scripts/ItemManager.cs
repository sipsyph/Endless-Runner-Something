using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<GameObject> itemPool = new List<GameObject>();
    public List<GameObject> allItems = new List<GameObject>();

    //public static GameObject testPrefab;
    void Start()
    {
        itemPool = allItems;
    }
}
