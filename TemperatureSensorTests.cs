using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoSe22_SE_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoSe22_SE_SmartHome.Tests
{
    [TestClass()]
    public class TemperatureSensorTests
    {
        [TestMethod()]
        public void CreateTemperatureSensor_nameNull()
        {
            TemperatureSensor tempS = new TemperatureSensor(1);
            Assert.IsNotNull(tempS);
        }

        [TestMethod()]
        public void CreateTemperatureSensor_nameNotNull()
        {
            TemperatureSensor tempS = new TemperatureSensor(1, "test");
            Assert.IsNotNull(tempS);
        }

        [TestMethod()]
        public void DisableSensorTest()
        {
            TemperatureSensor tempS = new TemperatureSensor(1, "test");
            tempS.EnableSensor();
            tempS.DisableSensor();
            Assert.IsTrue(tempS.stop);
        }

        [TestMethod()]
        public void GetSensorDataAfterThreadGotInitialized()
        {
            TemperatureSensor tempS = new TemperatureSensor(1, "test");

            tempS.EnableSensor();

            Thread.Sleep(10000);
            SensorData sensorData = tempS.sensorData[0];
            Assert.IsNotNull(sensorData);
        }

        [TestMethod()]
        public void GetSensorName()
        {
            TemperatureSensor tempS = new TemperatureSensor(1, "test");
            Assert.AreEqual("test", tempS.GetSensorName());
        }

        [TestMethod()]
        public void SetSensorName()
        {
            TemperatureSensor tempS = new TemperatureSensor(1);
            tempS.SetSensorName("sensorname");
            Assert.AreEqual("sensorname", tempS.GetSensorName());
        }
    }
}