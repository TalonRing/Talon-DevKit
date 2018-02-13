// Copyright 2017 Titanium Falcon Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TalonSDK;

public class TalonTester : MonoBehaviour 
{
    public GameObject ringModel;
    public GameObject gizmo;
    public Renderer ring;
    public Renderer button0;
    public Renderer button1;
    public Text logText;

    private TalonRing talonRing;

    public void Init(TalonRing ring)
    {
        this.talonRing = ring;
        UpdateModel();
    }

    public void UpdateModel()
    {
        if (this.talonRing.RingMode == TalonConfig.RingMode.Fist)
        {
            this.ring.transform.localRotation = Quaternion.Euler(0, -90, -90);
        }
        else
        {
            if (this.talonRing.Handedness == TalonConfig.Handedness.Left)
            {
				this.ring.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
				this.ring.transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
        }
    }

    public void OnModeChanged(int value)
    {
        if(value == 0)
            this.talonRing.RingMode = TalonConfig.RingMode.Fist;
        else
            this.talonRing.RingMode = TalonConfig.RingMode.Palm;
        UpdateModel();
    }

    public void OnHandedChanged(int value)
    {
        if(value == 0)
            this.talonRing.Handedness = TalonConfig.Handedness.Right;
        else
            this.talonRing.Handedness = TalonConfig.Handedness.Left;
        UpdateModel();
    }

    private void Update() 
    {
        if (talonRing == null)
            return;

        if (talonRing.RingTapped)
        {
            ring.material.color = new Color(1, 0, 1, 0.5f);
        }
        else
        {
            ring.material.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }

        if (talonRing.BottomButton)
        {
            button0.material.color = new Color(1, 1, 0, 0.5f);
        }
        else
        {
            button0.material.color = new Color(0.4f, 0.4f, 0.4f, 0.5f);
        }

        if (talonRing.TopButton)
        {
            button1.material.color = new Color(1, 1, 0, 0.5f);
        }
        else
        {
            button1.material.color = new Color(0.4f, 0.4f, 0.4f, 0.5f);
        }

        var click = GetButtonClick();
        if (click == ButtonClick.SingleClick)
        {
            Debug.Log("Single Click");
        }
        else if (click == ButtonClick.DoubleClick)
        {
            Debug.Log("Double Click");
        }

        if (talonRing.Recentered)
        {
            Debug.Log("Recentered");
        }

        ringModel.transform.rotation = talonRing.Orientation;
        gizmo.transform.rotation = talonRing.Orientation;

        if (logText != null)
        {
            logText.text = "Battery: " + talonRing.BatteryLevel + "%\n" + talonRing.DeviceId;
        }
    }

    //button clik
    private enum ButtonClick
    {
        None = 0,
        SingleClick = 1,
        DoubleClick = 2
    }

    private const float doubleClickDelay = 0.2f;
    private bool singleClick = false;
    private float timerForDoubleClick;

    private ButtonClick GetButtonClick()
    {
        if(talonRing.BottomButtonDown)
        {
            if(!singleClick)
            {
                singleClick = true;
                timerForDoubleClick = Time.time;
            } 
            else
            {
                singleClick = false;
                return ButtonClick.DoubleClick;
            }
        }

        if(singleClick)
        {
            if((Time.time - timerForDoubleClick) > doubleClickDelay)
            {
                singleClick = false;
                return ButtonClick.SingleClick;
            }
        }
        return ButtonClick.None;
    }
}
