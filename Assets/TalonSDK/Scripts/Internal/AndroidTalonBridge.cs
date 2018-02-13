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
#if UNITY_ANDROID && !UNITY_EDITOR
    using System;
    using UnityEngine;

    public class AndroidTalonBridge : TalonBridge
    {
        private AndroidJavaClass talon;

        public void Init(GameObject gameObject, TalonConfig config)
        {
            talon = new AndroidJavaClass("com.talon.sdk.TalonUnity");
            talon.CallStatic("init", gameObject.name, config.toByte(), config.sensorPeriod);
        }

        public StatusMessage GetStatusMessage()
        {
            return null;
        }

        public void Dispose()
        {
            talon.CallStatic("destroy");
        }

        public void Scan()
        {
            talon.CallStatic("scan");
        }

        public void Connect(string deviceId)
        {
            talon.CallStatic("connect", deviceId);
        }

        public void SetConfig(string deviceId, TalonConfig config)
        {
            talon.CallStatic("setConfig", deviceId, config.toByte());
        }

        public bool GetButtonState(string deviceId, int btn)
        {
            return talon.CallStatic<bool>("getButton", deviceId, btn); 
        }

        public bool GetTapped(String deviceId)
        {
            return talon.CallStatic<bool>("getTapped", deviceId); 
        }

        public bool Recentered(String deviceId)
        {
            return talon.CallStatic<bool>("recentered", deviceId); 
        }

        public Quaternion GetOrientation(string deviceId)
        {
            return _toQuaternion( talon.CallStatic<float[]>("getOrientation", deviceId) );
        }

        public Vector3 GetGyro(string deviceId)
        {
            return _toVector3( talon.CallStatic<float[]>("getGyro", deviceId) );
        }

        public Vector3 GetAccel(string deviceId)
        {
            return _toVector3( talon.CallStatic<float[]>("getAccel", deviceId) );
        }

        public Vector3 GetMag(string deviceId)
        {
            return _toVector3( talon.CallStatic<float[]>("getMag", deviceId) );
        }

        public int GetBattery(string deviceId)
        {
            return talon.CallStatic<int>("getBattery", deviceId);
        }

        private Quaternion _toQuaternion(float[] q)
        {
            return new Quaternion(q[1], q[2], q[3], q[0]);
        }

        private Vector3 _toVector3(float[] q)
        {
            return new Vector3(q[0], q[1], q[2]);
        }
    }

#endif
}