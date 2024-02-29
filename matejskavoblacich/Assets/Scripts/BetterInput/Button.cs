using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Button : BaseHoldable
{
    public void Start(){
        minTimeBeforeHold = 0.3f;
        maxTimeBetweenClicks = 0.4f;
    }
}
