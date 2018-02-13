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
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TalonSDK.Internal;

    /// <summary>
    /// The main entry point of Talon SDK.
    /// </summary>
    public class Talon : MonoBehaviour
    {
        /* 
         * Configurations for Talon Ring.
         */
        [SerializeField]
        public TalonConfig config = new TalonConfig();

        /*
         * TalonRing discovered event.
         */
        [SerializeField]
        public TalonStatusEvent OnDiscovered;

        /*
         * TalonRing connected event.
         */
        [SerializeField]
        public TalonStatusEvent OnConnected;

        /*
         * TalonRing disconnected event.
         */
        [SerializeField]
        public TalonStatusEvent OnDisconnected;

        /*
         * Scan nearby TalonRing devices. 
         */
        public static void Scan()
        {
            if (_instance != null) _instance.talon.Scan();
        }

        /*
         * Connect TalonRing by device ID.
         */
        public static void Connect(string deviceId)
        {
            if (_instance != null) _instance.talon.Connect(deviceId);
        }

        /*
         * Check if TalonRing is connected by device ID.
         */
        public static bool IsConnected(string deviceId)
        {
            if (_instance != null) 
                return _instance.connectedRings.ContainsKey(deviceId);
            else 
                return false;
        }

        /*
         * Returns connected TalonRing's instance by device ID.
         */
        public static TalonRing GetConnectedRing(string deviceId)
        {
            if (!IsConnected(deviceId))
                return null;
            else if (_instance != null) 
                return _instance.connectedRings[deviceId];
            else 
                return null;
        }

        private static Talon _instance;
        private TalonBridge talon;

        private Dictionary<string, TalonRing> connectedRings = new Dictionary<string, TalonRing>();

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("More than one Talon instance was found in your scene");
                enabled = false;
                return;
            }
            _instance = this;
            connectedRings.Clear();

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_IOS || UNITY_STANDALONE_WIN || UNITY_WSA
            talon = new NativeTalonBridge();
#elif UNITY_ANDROID && !UNITY_EDITOR
            talon = new AndroidTalonBridge();
#else
            talon = new DummyTalonBridge();
#endif                        
            talon.Init(this.gameObject, config);
        }

        void OnDestroy()
        {
            connectedRings.Clear();
            talon.Dispose();
            _instance = null;
        }

        //status
        void onDiscovered(string deviceId)
        {
            Debug.Log("onDiscovered " + deviceId);
            OnDiscovered.Invoke(deviceId);
        }

        void onConnected(string deviceId)
        {
            Debug.Log("onConnected " + deviceId);
            if(!connectedRings.ContainsKey(deviceId))
            {
                connectedRings.Add(deviceId, new TalonRing(talon, config, deviceId));
            }
            OnConnected.Invoke(deviceId);
        }

        void onDisconnected(string deviceId)
        {
            Debug.Log("onDisconnected " + deviceId);
            OnDisconnected.Invoke(deviceId);
            if(connectedRings.ContainsKey(deviceId))
            {
                connectedRings.Remove(deviceId);
            }
        }

        //Sensor data
        private IEnumerator sensorUpdate;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        void OnEnable()
        {
            sensorUpdate = EndOfFrame();
            StartCoroutine(sensorUpdate);
        }

        void OnDisable()
        {
            StopCoroutine(sensorUpdate);
        }

        IEnumerator EndOfFrame() 
        {
            while (true) 
            {
                yield return waitForEndOfFrame;
                
                if (talon != null)
                {
                    StatusMessage msg = talon.GetStatusMessage();
                    if (msg != null)
                    {
                        SendMessage(msg.method, msg.message);
                    }
                }

                var enumerator = connectedRings.GetEnumerator();
                while (enumerator.MoveNext()) 
                {
                    enumerator.Current.Value.Update();
                }
            }
        }
    }
}
