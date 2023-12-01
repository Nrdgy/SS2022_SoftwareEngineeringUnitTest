using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoSe22_SE_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SoSe22_SE_SmartHome.Tests
{
    [TestClass()]
    public class CO2SensorTests
    {
        [TestMethod()]
        public void CreateCO2Sensor_nameNull()
        {
            CO2Sensor co2S = new CO2Sensor(1);
            Assert.IsNotNull(co2S);
        }

        [TestMethod()]
        public void CreateCO2Sensor_nameNotNull()
        {
            CO2Sensor co2S = new CO2Sensor(1, "test");
            Assert.IsNotNull(co2S);
        }

        [TestMethod()]
        public void DisableSensorTest()
        {
            CO2Sensor co2S = new CO2Sensor(1, "test");
            co2S.EnableSensor();
            co2S.DisableSensor();
            Assert.IsTrue(co2S.stop);
        }

        [TestMethod()]
        public void GetSensorDataAfterThreadGotInitialized()
        {
            CO2Sensor co2S = new CO2Sensor(1, "test");

            co2S.EnableSensor();

            Thread.Sleep(10000);
            SensorData sensorData = co2S.sensorData[0];
            Assert.IsNotNull(sensorData);
        }

        [TestMethod()]
        public void GetSensorName()
        {
            CO2Sensor co2S = new CO2Sensor(1, "test");
            Assert.AreEqual("test", co2S.GetSensorName());
        }

        [TestMethod()]
        public void SetSensorName()
        {
            CO2Sensor co2S = new CO2Sensor(1);
            co2S.SetSensorName("sensorname");
            Assert.AreEqual("sensorname", co2S.GetSensorName());
        }
    }
}