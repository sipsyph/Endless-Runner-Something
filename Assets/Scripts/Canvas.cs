using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Button leftHandBtn, rightHandBtn;
    void Start()
    {
        SetupButtonEvents();
    }

    void Update()
    {
        
    }

    void SetupButtonEvents()
    {
        leftHandBtn.onClick.AddListener(() =>
        {
            Player.rightHandSelected = false;
            Player.leftHandSelected = true;
        });

        rightHandBtn.onClick.AddListener(() =>
        {
            Player.leftHandSelected = false;
            Player.rightHandSelected = true;
        });
    }
}
