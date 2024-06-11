# smarthome-api
A api (written in C#) for smart home devices from CREA SYSTEMS Electronic GmbH.

## Contents

  - [Supported Devices](#supported-devices)
  - [SUNNYHEAT Infrared heater](#sunnyheat-infrared-heater)
  
### Supported Devices

Currently, the smarthome-api supports the following devices.

| **Device**                |                                           **Description**                                           |
|---------------------------|:---------------------------------------------------------------------------------------------------:|
| SUNNYHEAT Infrared heater | You can set the setpoint temperature and the light state. You can also ask for the current values.    |

### SUNNYHEAT Infrared heater

With this device type, you can set the setpoint temperature and the light state. You can also ask for the current values. You can see here the possible commands.

| **Command**                |                                           **Description & Example**                                                                                  |
|----------------------------|:----------------------------------------------------------------------------------------------------------------------------------------------------:|
| Set Target temperature     | With this command you can set the target temperature of the heater:<br>Possible values are 0 to 30.0 degress in 0.5 steps.                           |
|                            | http://creashgateway.local:8000/setTargetTemp?meshid=[NameOfTheMesh]&mac=[MacAdressOfTheHeater]&value=[0to30.0]&ref=[UniqueIdentifierForThisMessage] |
|                            | http://creashgateway.local:8000/setTargetTemp?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=20.0&ref=abc12345                                          |
|                            |                                                                                                                                                      |
| Set Light state            | With this command you can set the light state of a connected light: Possible values are 0 (off) or 1 (on).                                           |
|                            | http://creashgateway.local:8000/setLightState?meshid=[NameOfTheMesh]&mac=[MacAdressOfTheHeater]&value=[0or1]&ref=[UniqueIdentifierForThisMessage]    |
|                            | http://creashgateway.local:8000/setLightState?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=1&ref=abc12345                                             |
|                            |                                                                                                                                                      |
| Get current data           | With this command you can get the current data from the heater.                                                                                      |
|                            | http://creashgateway.local:8000/getCurrentData?meshid=[NameOfTheMesh]&mac=[MacAdressOfTheHeater]&value=[1]&ref=[UniqueIdentifierForThisMessage]      |
|                            | http://creashgateway.local:8000/getCurrentData?meshid=testMesh&mac=0a:0a:0a:0a:0a:0a&value=1&ref=abc12345                                            |
|                            |                                                                                                                                                      |