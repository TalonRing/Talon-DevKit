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
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IOS || UNITY_STANDALONE_WIN || UNITY_WSA
    using System;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using System.Collections;
    using System.Collections.Generic;
    using AOT;

    public class NativeTalonBridge : TalonBridge
    {
    #if UNITY_IOS && !UNITY_EDITOR
        private const string DLLImportName = "__Internal";
    #else    
        private const string DLLImportName = "TalonSDK";
    #endif

        [DllImport(DLLImportName)]
        private static extern void talon_init(UnityCallback callback, byte config, byte period);

        [DllImport(DLLImportName)]
        private static extern void talon_destroy();

        [DllImport(DLLImportName)]
        private static extern void talon_scan();

        [DllImport(DLLImportName)]
        private static extern void talon_connect(string deviceId);
        
        [DllImport(DLLImportName)]
        private static extern void talon_config(string deviceId, byte config);

        [DllImport(DLLImportName)]
        private static extern IntPtr talon_orientation(string deviceId);

        [DllImport(DLLImportName)]
        private static extern IntPtr talon_accel(string deviceId);

        [DllImport(DLLImportName)]
        private static extern IntPtr talon_gyro(string deviceId);

        [DllImport(DLLImportName)]
        private static extern IntPtr talon_mag(string deviceId);

        [DllImport(DLLImportName)]
        private static extern bool talon_button(string deviceId, int btn);

        [DllImport(DLLImportName)]
        private static extern bool talon_tapped(string deviceId);

        [DllImport(DLLImportName)]
        private static extern bool talon_recentered(string deviceId);

        [DllImport(DLLImportName)]
        private static extern int talon_battery(string deviceId);
        
        public delegate void UnityCallback(string method,string message);

        [MonoPInvokeCallback(typeof(UnityCallback))]
        public static void unityCallback(string method, string message)
        {
            queuedMessage.Enqueue(new StatusMessage(method, message));
        }

        private static Queue<StatusMessage> queuedMessage = new Queue<StatusMessage>();

        public void Init(GameObject obj, TalonConfig config)
        {
            talon_init(unityCallback, config.toByte(), config.sensorPeriod);
        }

        public StatusMessage GetStatusMessage()
        {
            if (queuedMessage.Count > 0)
                return queuedMessage.Dequeue();
            else
                return null;
        }

        public void Dispose()
        {
            talon_destroy();
        }

        public void Scan()
        {
            talon_scan();
        }

        public void Connect(string deviceId)
        {
            talon_connect(deviceId);
        }

        public void SetConfig(string deviceId, TalonConfig config)
        {
            talon_config(deviceId, config.toByte());
        }

        public bool GetButtonState(string deviceId, int btn)
        {
            return talon_button(deviceId, btn);
        }

        public bool GetTapped(String deviceId)
        {
            return talon_tapped(deviceId);
        }

        public bool Recentered(String deviceId)
        {
            return talon_recentered(deviceId);
        }

        public Quaternion GetOrientation(string deviceId)
        { 
            return _toQuaternion( talon_orientation(deviceId) );
        }

        public Vector3 GetGyro(string deviceId)
        { 
            return _toVector3( talon_gyro(deviceId) );
        }

        public Vector3 GetAccel(string deviceId)
        { 
            return _toVector3( talon_accel(deviceId) );
        }

        public Vector3 GetMag(string deviceId)
        {
            return _toVector3( talon_mag(deviceId) );
        }

        public int GetBattery(string deviceId)
        {
            return talon_battery(deviceId);
        }

        private float[] q = new float[4];
        private Quaternion _toQuaternion(IntPtr ptr)
        {
            Marshal.Copy(ptr, q, 0, 4);
            return new Quaternion(q[1], q[2], q[3], q[0]);
        }

        private Vector3 _toVector3(IntPtr ptr)
        {
            Marshal.Copy(ptr, q, 0, 3);
            return new Vector3(q[0], q[1], q[2]);
        }
    }

#endif
}