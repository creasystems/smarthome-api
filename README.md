# smarthome-api
A api (written in C#) for smart home devices from CREA SYSTEMS Electronic GmbH.

## Contents

  - [Requirements for use] (#requirements-for-use)
  - [Supported Devices](#supported-devices)
  - [SUNNYHEAT Infrared heater](#sunnyheat-infrared-heater)

### Requirements for use

To use this smarthome-api, you need the following reguirements
  - CREA SmartHome Gateway
  - Minimum one of the supported devices
  - The smartphone app CREAconnect to preconfigure the mesh
  
### Supported Devices

Currently, the smarthome-api supports the following devices.

| **Device**                |                                           **Description**                                           |
|---------------------------|:---------------------------------------------------------------------------------------------------:|
| SUNNYHEAT Infrared heater | You can set the setpoint temperature and the light state. You can also ask for the current values.    |

### SUNNYHEAT Infrared heater

With this device type, you can set the setpoint temperature and the light state. You can also ask for the current values. You can see here the possible commands.

| **Command**                |                                           **Description & Example**                                                        |
|----------------------------|:--------------------------------------------------------------------------------------------------------------------------:|
| Set Target temperature     | With this command you can set the target temperature of the heater:<br>Possible values are 0 to 30.0 degress in 0.5 steps. |
|                            | http://creashgateway.local:8000/setTargetTemp?meshid=[MeshName]&mac=[MacAdressHeater]&value=[Degree]&ref=[UniqueMessageId] |
|                            | http://creashgateway.local:8000/setTargetTemp?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=20.0&ref=abc12345                |
| Set Light state            | With this command you can set the light state of a connected light: Possible values are 0 (off) or 1 (on).                 |
|                            | http://creashgateway.local:8000/setLightState?meshid=[MeshName]&mac=[MacAdressHeater]&value=[0or1]&ref=[UniqueMessageId]   |
|                            | http://creashgateway.local:8000/setLightState?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=1&ref=abc12345                   |
| Get current data           | With this command you can get the current data from the heater.                                                            |
|                            | http://creashgateway.local:8000/getCurrentData?meshid=[MeshName]&mac=[MacAdressHeater]&value=[1]&ref=[UniqueMessageId]     |
|                            | http://creashgateway.local:8000/getCurrentData?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=1&ref=abc12345                  |
