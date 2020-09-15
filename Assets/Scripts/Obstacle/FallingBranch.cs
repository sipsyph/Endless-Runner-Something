using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBranch : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject branchType1, branchType2, branchType3;

    void Start()
    {
        int randNum = Random.Range(1,100);
        if(randNum<=33)
        {
            branchType1.SetActive(true);
        }
        else if(randNum > 33 && randNum <= 66)
        {
            branchType2.SetActive(true);
        }else{
            branchType3.SetActive(true);
        }
        this.transform.SetParent(null);
    }

    void Update()
    {
        if(!PlayerParent.playerClimbing)
        {
            ERSUtilities.DeleteThis(this.transform);
        }
    }

    public void AlertThatFallingEnded(string message)
    {
        if(message.Equals("FallingEnded"))
        {
            ERSUtilities.DeleteThis(this.transform);
        }
    }
}
