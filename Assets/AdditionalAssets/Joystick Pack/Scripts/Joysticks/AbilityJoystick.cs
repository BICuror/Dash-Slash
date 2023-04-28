using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        base.OnPointerDown(new PointerEventData(null));
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Main.playerAbility.ActivateAbility(Vector3.up * Vertical + Vector3.right * Horizontal);
        
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}
