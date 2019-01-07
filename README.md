# iotcodetestbackend
Web API developed in .Net Core to fetch telemetry records from blob and return the response based on requested endpoint.

* Steps to Run
1. Download or Clone into local Machine.
2. Build the Application.
3. Run the Application.
4. Navigate to Url localhost:port/swagger/index.html.

* Description
  
  Sensors are sending telemetry every 5 seconds which is getting stored in appendblob as deviceId/SensorType/yyyy-mm-dd.csv format.
  
  Number of Endpoints exposed : 2
  
  API Url : /api/v{version}/devices/getdata/{deviceId}/{date}/{sensor}
  Http Verb : GET
  Response : Data for particular DeviceId, date and sensor.
  Status Codes : 200 (OK) Successfull Response
                 400 (Bad Response) For Future Dates.
                 
  API Url : /api/v{version}/devices/getdatafordevice/{deviceId}/{date}
  Http Verb : GET
  Response : Data for particular DeviceId and date for all sensors.
  Status Codes : 200 (OK) Successfull Response
                 400 (Bad Response) For Future Dates.
                 
  
  Note : API details can be fetched using swagger url.
