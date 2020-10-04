using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponButton : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Down on WPN BTN");
        PlayerParent.allowWalkAnimation = false;
        PlayerAnimation.PlayWalkingPrepSlash();
        SwordAnimation.PlayPreSlash();
        //SwordAnimation.PlaySwordBtnFadeInAnimation();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Up on WPN BTN");
        PlayerParent.attackingModeDurationCtr  = 0;
        PlayerParent.isAttacking = true;
        PlayerAnimation.PlayWalkingSlash();
        SwordAnimation.PlaySlash();
        //SwordAnimation.PlaySwordBtnIdleAnimation();
        //PlayerParent.allowWalkAnimation = true;
    }
}
