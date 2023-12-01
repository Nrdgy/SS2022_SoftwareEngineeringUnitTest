using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace SoSe22_SE_SmartHome.Tests
{
    [TestClass()]
    public class LightSensorTests
    {
        [TestMethod()]
        public void CreateLightSensor_nameNull()
        {
            LightSensor lS = new LightSensor(1);
            Assert.IsNotNull(lS);   
        }
        
        [TestMethod()]
        public void CreateLightSensor_nameNotNull()
        {
            LightSensor lS = new LightSensor(1, "test");
            Assert.IsNotNull(lS);
        }

        [TestMethod()]
        public void DisableSensorTest()
        {
            LightSensor lS = new LightSensor(1, "test");
            lS.EnableSensor();
            lS.DisableSensor();
            Assert.IsTrue(lS.stop);
        }

        [TestMethod()]
        public void GetSensorDataAfterThreadGotInitialized()
        {
            LightSensor lS = new LightSensor(1, "test");

            lS.EnableSensor();

            Thread.Sleep(10000);
            SensorData sensorData = lS.sensorData[0];
            Assert.IsNotNull(sensorData);
        }

        [TestMethod()]
        public void GetSensorName()
        {
            LightSensor lS = new LightSensor(1, "test");
            Assert.AreEqual("test", lS.GetSensorName());
        }

        [TestMethod()]
        public void SetSensorName()
        {
            LightSensor lS = new LightSensor(1);
            lS.SetSensorName("sensorname");
            Assert.AreEqual("sensorname", lS.GetSensorName());
        }

    }
}