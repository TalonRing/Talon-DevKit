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

namespace TalonSDK.Internal
{
    using System;
    using UnityEngine;

    public class DummyTalonBridge : TalonBridge
    {
        public void Init(GameObject gameObject, TalonConfig config)
        {

        }

        public StatusMessage GetStatusMessage()
        {
            return null;
        }

        public void Dispose()
        {

        }

        public void Scan()
        {

        }

        public void Connect(string deviceId)
        {

        }

        public void SetConfig(string deviceId, TalonConfig config)
        {

        }

        public bool GetButtonState(string deviceId, int btn)
        {
            return false;
        }

        public bool GetTapped(String deviceId)
        {
            return false;
        }

        public bool Recentered(String deviceId)
        {
            return false;
        }

        public Quaternion GetOrientation(string deviceId)
        {
            return Quaternion.identity;
        }

        public Vector3 GetGyro(string deviceId)
        {
            return Vector3.zero;
        }

        public Vector3 GetAccel(string deviceId)
        {
            return Vector3.zero;
        }

        public Vector3 GetMag(string deviceId)
        {
            return Vector3.zero;
        }

        public int GetBattery(string deviceId)
        {
            return 100;
        }
    }
}