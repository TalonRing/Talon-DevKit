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

namespace TalonSDK
{
    using System;
    using UnityEngine;
    using TalonSDK.Internal;

    /// <summary>
    /// Class that represents the connected Talon Ring instance.
    /// </summary>
    public class TalonRing
    {
        private TalonBridge talon;
        private TalonConfig config;
        private string deviceId;

        /*
         * constructor 
         */
        public TalonRing(TalonBridge talon, TalonConfig config, string deviceId)
        {
            this.talon = talon;
            this.deviceId = deviceId;
            this.config = new TalonConfig(config);
        }

        /*
         * Returns TalonRing's deviceId 
         */
        public string DeviceId
        {
            get {
                return deviceId;
            }
        }

        /*
         * Returns TalonRing's current orientation in space, as a quaternion 
         */
        public Quaternion Orientation
        {
            get {
                Quaternion adjust = Quaternion.identity;
                if (RingMode == TalonConfig.RingMode.Fist)
                {
                    adjust = Quaternion.Euler(-47.2f, 0, 0);
                }
                else
                {
                    if (Handedness == TalonConfig.Handedness.Left)
                    {
                        adjust = Quaternion.Euler(0, 0, -42.8f);
                    }
                    else
                    {
                        adjust = Quaternion.Euler(0, 0, 42.8f);
                    }
                }
                return talon.GetOrientation(deviceId) * adjust;
            }
        }

        /*
         * Returns TalonRing's accelerometer reading.
         */
        public Vector3 Accel
        {
            get {
                return talon.GetAccel(deviceId);
            }
        }

        /*
         * Returns TalonRing's gyroscope reading.
         */
        public Vector3 Gyro
        {
            get {
                return talon.GetGyro(deviceId);
            }
        }

        /*
         * Returns TalonRing's magnetometer reading.
         */
        public Vector3 Mag
        {
            get {
                return talon.GetMag(deviceId);
            }
        }

        /*
         * Returns TalonRing's current battery level.
         */
        public int BatteryLevel
        {
            get {
                return talon.GetBattery(deviceId);
            }
        }

        /*
         * Updates TalonRing's mode.
         */
        public TalonConfig.RingMode RingMode
        {
            get {
                return config.ringMode;
            }
            set {
                config.ringMode = value;
                talon.SetConfig(deviceId, config);
            }
        }

        /*
         * Updates TalonRing's handedness.
         */
        public TalonConfig.Handedness Handedness
        {
            get {
                return config.handedness;
            }
            set {
                config.handedness = value;
                talon.SetConfig(deviceId, config);
            }
        }

        /*
         * Returns true while the user holds down the Top button.
         */
        public bool TopButton
        {
            get
            {
                return button[1];
            }
        }

        /*
         * Returns true Top button was just pressed.
         */
        public bool TopButtonDown
        {
            get
            {
                return buttonDown[1];
            }
        }
        
        /*
         * Returns true if Top button was just released.
         */
        public bool TopButtonUp
        {
            get
            {
                return buttonUp[1];
            }
        }


        /*
         * Returns true while the user holds down the Bottom button.
         */
        public bool BottomButton
        {
            get
            {
                return button[0];
            }
        }

        /*
         * Returns true Bottom button was just pressed.
         */
        public bool BottomButtonDown
        {
            get
            {
                return buttonDown[0];
            }
        }
        
        /*
         * Returns true if Bottom button was just released.
         */
        public bool BottomButtonUp
        {
            get
            {
                return buttonUp[0];
            }
        }
        

        /*
         * If true, TalonRing body was just tapped. 
         */
        public bool RingTapped
        {
            get
            {
                return ringTapped;
            }
        }

        /*
         * If true, TalonRing was just recentered. 
         */
        public bool Recentered
        {
            get
            {
                return recentered;
            }
        }

        //states
        private bool[] buttonState = {false, false};
        private bool[] buttonDown = {false, false};
        private bool[] buttonUp = {false, false};
        private bool[] button = {false, false};
        private bool ringTapped = false;
        private bool recentered = false;

        /*
         * Update
         */
        internal void Update()
        {
            for(int i = 0; i < 2; ++i)
            {
                bool state = talon.GetButtonState(deviceId, i);
                if ( buttonState[i] != state)
                {
                    buttonDown[i] = state;
                    buttonUp[i] = !state;
                    button[i] = state;
                }
                else
                {
                    buttonDown[i] = false;
                    buttonUp[i] = false;
                }

                buttonState[i] = state;
            }

            ringTapped = talon.GetTapped(deviceId);
            recentered = talon.Recentered(deviceId);
        }

    }

}