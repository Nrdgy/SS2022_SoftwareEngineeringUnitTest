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
    public class NotifyActionTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            double threshold = 1.0;
            string reason = "überschritten";

            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            sensor.EnableSensor();
            Thread.Sleep(10000);
            
            SensorData data = sensor.sensorData[0];
            
            NotifyAction action = new NotifyAction();
            
            action.Execute(data, threshold, reason);
            
            string actual = action.notificationContent;
            string expected = String.Format("Sensor {0} hat Grenze {1} mit Wert {2} {3}!", data.Name, threshold, data.Value, reason);
            Assert.AreEqual(expected, actual);
        }
    }
}