using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryBarScript : MonoBehaviour
{
    GameObject FurySliderGO;
    public Slider FurySlider;
    Animator FuryAnimator;
    public bool CanShoot = false;
    
    // Start is called before the first frame update
    void Start()
    {
        FurySliderGO = transform.GetChild(0).gameObject;
        FurySlider = FurySliderGO.GetComponent<Slider>();
        FuryAnimator = FurySliderGO.GetComponent<Animator>();
    }

    public void AddFury(float addFury)
    {
        FurySlider.value += addFury;


        if(FurySlider.value > FurySlider.maxValue)
            FurySlider.value = FurySlider.maxValue;
                
        if (FurySlider.value < FurySlider.minValue)
            FurySlider.value = FurySlider.minValue;



        if (FurySlider.value/ FurySlider.maxValue >= 0.8f)
        {
            FuryAnimator.Play("ReadyBar");
        }
        else
        {
            FuryAnimator.Play("IdleBar");
        }
    }

    /*private void FixedUpdate()
    {
        AddFury(1);
    }*/
}
