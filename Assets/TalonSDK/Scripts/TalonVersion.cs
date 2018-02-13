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
    using UnityEngine;

    /// <summary>
    /// Talon SDK Version
    /// </summary>
    public class TalonVersion 
    {
        public const string TALON_SDK_VERSION = "0.5.8";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LogTalonSdkVersion() 
        {
            Debug.Log("Talon SDK Version: " + TALON_SDK_VERSION);
        }
        
    }
}
