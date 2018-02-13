# Talon Dev Kit

Enabled Talon Ring as controller, support game & app development in Unity.

Copyright (c) 2018 [Titanium Falcon Inc](http://www.titaniumfalcon.com/). All rights reserved.

For updates, news, and instructions, see:
[talonring.com](http://www.talonring.com)

`Unity 5.6+` Recommended.

## Latest Vesion 
* `v0.5.8`

## Supported Platforms
* iOS 7.0+
* Android 4.3+
* MacOSX 10.9+ 
* Windows 8.0+

## Talon Firmware Requirement
* `v1.161`

## SDK Interfaces
| ClassName                  |                Description                                 |
|:--------------------------:|:----------------------------------------------------------:|
| Talon                      | The main entry point of the Talon SDK.                     |
| TalonConfig                | Configurations for Talon Ring.                             |
| TalonRing                  | Class that represents the connected Talon Ring instance.   |

## Talon Class 
The main entry point to the Talon SDK API is the Talon class.

> To use the Talon SDK, you must have a game object in your scene with the Talon script attached.

| Method                            |                Description                             |
|:---------------------------------:|:------------------------------------------------------:|
| Scan()                            | Scan nearby TalonRing devices.                         |
| Connect(string deviceId)          | Connect TalonRing by device ID.                        |
| IsConnected(string deviceId)      | Check if TalonRing is connected by device ID.          |
| GetConnectedRing(string deviceId) | Returns connected TalonRing's instance by device ID.   |

> Modify properties through Unity Insepector

| Property                          |                Description                             |
|:---------------------------------:|:------------------------------------------------------:|
| TalonConfig config                | Configurations for TalonRing.                          |
| OnDiscovered                      | TalonRing discovered event.                            |
| OnConnected                       | TalonRing connected event.                             |
| OnDisconnected                    | TalonRing disconnected event.                          |

## TalonConfig Class
Use it to modify configurations for Talon Ring. It is a property of Talon class, you can easily modify it through Unity inspector.

| Property                          |                Description                                 |
|:---------------------------------:|:----------------------------------------------------------:|
| ringMode          | Default TalonRing mode - Fist or Palm.                                     |
| handedness        | Default TalonRing handedness - Right or Left.                              |
| sensorEnabled     | Enable TalonRing Sensors.                                                  |
| sensorRawData     | Sensor raw data. Output raw sensors data - gyro, accel, and mag data.      |
| sensorPeriod      | Sensor update period. Range: 1 – 50hz. Default: 20hz                       |
| lowPowerMode      | Enable low power mode.                                                     |
| sleepMode         | Enable sleep mode.                                                         |

## TalonRing Class
The TalonRing API is a polling-style API which gives you access to the ring's data from buttons and sensors. You can easily access it from the Update() method in any of your game objects.

| Property                          |                Description                                         |
|:---------------------------------:|:------------------------------------------------------------------:|
| DeviceId                          | Returns TalonRing's deviceId.                                      |
| Orientation                       | Returns TalonRing's current orientation in space, as a quaternion. |
| Accel                             | Returns TalonRing's accelerometer reading.                         |
| Gyro                              | Returns TalonRing's gyroscope reading.                             |  
| Mag                               | Returns TalonRing's magnetometer reading.                          |
| BatteryLevel                      | Returns TalonRing's current battery level.                         |
| RingMode                          | Updates TalonRing's mode - Fist or Palm.                           |
| Handedness                        | Updates TalonRing's handedness - Right or Left.                    |
| TopButton                         | Returns true while the user holds down the Top button.             |
| TopButtonDown                     | Returns true Top button was just pressed.                          |
| TopButtonUp                       | Returns true if Top button was just released.                      |
| BottomButton                      | Returns true while the user holds down the Bottom button.          |
| BottomButtonDown                  | Returns true Bottom button was just pressed.                       |
| BottomButtonUp                    | Returns true if Bottom button was just released.                   |
| RingTapped                        | If true, TalonRing body was just tapped.                           |
| Recentered                        | If true, TalonRing was just recentered.                            |


## TalonRing Coordinate
The TalonRing coordinate system is left-handed, same to Unity's coordinate system.

- The positive X axis points to the right.
- The positive Y axis points upwards.
- The positive Z axis points forwards.


