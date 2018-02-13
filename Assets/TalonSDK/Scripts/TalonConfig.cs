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

    /// <summary>
    /// Configurations for Talon Ring.
    /// </summary>
    [Serializable]
    public class TalonConfig
    {
        /*
         * ring mode 
         */
        public enum RingMode
        {
            Fist = 0,
            Palm = 1
        }
        public RingMode ringMode = RingMode.Fist;

        /*
         * handedness
         */
        public enum Handedness
        {
            Right = 0,
            Left = 1
        }
        public Handedness handedness = Handedness.Right;

        /*
         * sensor enabled
         */
        public bool sensorEnabled = true;

        /*
         * sensor raw data
         * gyro, accel, mag raw data
         */
        public bool sensorRawData = false;

        /*
         * sensor update period
         * range: 1 – 50hz
         * default: 20hz
         */
        public byte sensorPeriod = 20;

        /*
         * low power mode
         */
        public bool lowPowerMode = false;

        /*
         * sleep mode
         */
        public bool sleepMode = false;


        /*
         * default constructor 
         */
        public TalonConfig()
        {
        }

        /*
         * copy constructor
         */
        public TalonConfig(TalonConfig config)
        {
            ringMode = config.ringMode;
            handedness = config.handedness;
            sensorEnabled = config.sensorEnabled;
            sensorRawData = config.sensorRawData;
            sensorPeriod = config.sensorPeriod;
            lowPowerMode = config.lowPowerMode;
            sleepMode = config.sleepMode;
        }

        /*
         * convert it to byte for internal usage
         *  bit 0:   sensor switch      - 0: off, 1: on
         *  bit 1:   sensor raw data    - 0: disable, 1: enable raw sensor data
         *  bit 2:   low power mode     - 0: off, 1: on
         *  bit 3:   sleep mode         - 0: disable, 1: enable 60s
         *  bit 4:   ring mode          - 0: fist, 1: palm
         *  bit 5:   handedness         - 0: right, 1: left
         *  bit 6-7: n.a
         */
        public byte toByte()
        {
            byte config = 0;
            if (sensorEnabled) config |= 0x01;
            if (sensorRawData) config |= 0x02;
            if (lowPowerMode) config |= 0x04;
            if (sleepMode) config |= 0x08;
            if (ringMode == RingMode.Palm) config |= 0x10;
            if (handedness == Handedness.Left) config |= 0x20;
            return config;
        }
    }


}