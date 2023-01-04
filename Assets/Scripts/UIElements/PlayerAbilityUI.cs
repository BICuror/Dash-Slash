using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private Animator anim;
    
    public void SetFillAmount(float amount)
    {
        image.fillAmount = amount;
    }

    public void SemgmentAppear(int segmentCount)
    {
        transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, (1 - segmentCount) * 90f);

        anim.Play("DashBarSegmentAppear");
    }

    public void DeactivateSegment()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
