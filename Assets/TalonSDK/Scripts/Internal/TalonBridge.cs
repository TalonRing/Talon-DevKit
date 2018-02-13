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

    /// <summary>
    /// Interface for Native Talon SDK (Internal).
    /// </summary>
    public interface TalonBridge : IDisposable
    {
        void Init(GameObject gameObject, TalonConfig config);

        StatusMessage GetStatusMessage();

        void Scan();

        void Connect(string deviceId);

        void SetConfig(string deviceId, TalonConfig config);

        bool GetButtonState(string deviceId, int btn);

        bool GetTapped(String deviceId);

        bool Recentered(String deviceId);

        Quaternion GetOrientation(string deviceId);

        Vector3 GetGyro(string deviceId);

        Vector3 GetAccel(string deviceId);

        Vector3 GetMag(string deviceId);

        int GetBattery(string deviceId);
    }
}