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

public class TalonConnect : MonoBehaviour 
{
    public Text logText;
    public GameObject ringObject;
    private Dictionary<string, GameObject> testObjects = new Dictionary<string, GameObject>();

    public void Scanning()
    {
        Log("Talon Scanning ...");
        TalonSDK.Talon.Scan();
    }

    public void OnDiscovered(string deviceId)
    {
        TalonSDK.Talon.Connect(deviceId);
        Log("Talon Connecting: " + deviceId);
    }

    public void OnConnected(string deviceId)
    {
        Log("Talon Connected: " + deviceId);
        AddTalonRing(deviceId);
    }

    public void OnDisconnected(string deviceId)
    {
        Log("Talon Disconnected: " + deviceId);
        RemoveTalonRing(deviceId);
    }

    private void AddTalonRing(string deviceId)
    {
        if(testObjects.ContainsKey(deviceId))
            return;
        TalonSDK.TalonRing ring = TalonSDK.Talon.GetConnectedRing(deviceId);
        GameObject testObj = Instantiate(ringObject) as GameObject;
        testObj.SetActive(false);
        testObj.GetComponent<TalonTester>().Init(ring);
        testObj.transform.SetParent(transform, false);
        testObjects.Add(deviceId, testObj);
        UpdateVisibleRing();
    }

    private void RemoveTalonRing(string deviceId)
    {
        if(!testObjects.ContainsKey(deviceId))
            return;
        GameObject testObj = testObjects[deviceId];
        if (testObj != null)
        {
            Destroy(testObj);
            testObjects.Remove(deviceId);
        }
        UpdateVisibleRing();
    }

    private void Log(string log)
    {
        if (logText != null)
            logText.text = log;
    }

    public void UpdateVisibleRing()
    {
        GameObject nextVisibleRing = null;
        bool lastVisibleRing = false;
        var enumerator = testObjects.GetEnumerator();
        while (enumerator.MoveNext()) 
        {
            if (nextVisibleRing == null || lastVisibleRing)
            {
                nextVisibleRing = enumerator.Current.Value;
                lastVisibleRing = false;
            }
            lastVisibleRing = enumerator.Current.Value.activeSelf;
            enumerator.Current.Value.SetActive(false);
        }

        if (nextVisibleRing != null)
        {
            nextVisibleRing.SetActive(true);
        }
    }

}
