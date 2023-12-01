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
    public class ExceedThresholdAutomationTests
    {
        [TestMethod()]
        public void ExceedThresholdAutomation_EmptyActionList()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            Assert.AreEqual(actions, automation.actions);

        }

        [TestMethod()]
        public void ExceedThresholdAutomation_FilledActionList()
        {
            List<IAction> actions = new List<IAction>
            {
                new NotifyAction()
            };

            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            Assert.AreEqual(actions, automation.actions);

        }

        [TestMethod()]
        public void AddAction_EmptyActionList()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            NotifyAction newAction = new NotifyAction();
            
            automation.AddAction(newAction);

            CollectionAssert.Contains(automation.actions, newAction);
        }

        [TestMethod()]
        public void AddAction_FilledActionList()
        {
            List<IAction> actions = new List<IAction>
            {
                new NotifyAction()
            };

            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            NotifyAction newAction = new NotifyAction();

            automation.AddAction(newAction);

            CollectionAssert.Contains(automation.actions, newAction);
        }


        [TestMethod()]
        public void RemoveActions_FilledActionList()
        {
            List<IAction> actions = new List<IAction>
            {
                new NotifyAction(),
                new NotifyAction(),
                new NotifyAction()
            };

            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);

            automation.RemoveActions();

            Assert.IsTrue(automation.actions.Count() == 0);
        }

        [TestMethod()]
        public void RemoveActions_EmptyActionList()
        {
            List<IAction> actions = new List<IAction>();

            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);

            try
            {
                automation.RemoveActions();

            }
            catch (Exception ex)
            {
                Assert.Fail("Keine Fehlermeldung erwartet, aber erhielt: " + ex.Message);
            }
        }

        [TestMethod()]
        public void SetThreshold()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            automation.SetThreshold(2.0);

            Assert.AreEqual(2.0, automation.GetThreshold());
        }

        [TestMethod()]
        public void GetThreshold()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);

            Assert.AreEqual(1.0, automation.GetThreshold());
        }

        [TestMethod()]
        public void GetSensors()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            automation.Subscribe(sensor);
            
            CollectionAssert.Contains(automation.GetSensors(), sensor);
        }

        [TestMethod()]
        public void CheckValue_IsAboveThreshold()
        {
            double threshold = 14.0;
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            sensor.EnableSensor();
            Thread.Sleep(5000);
            SensorData data = sensor.sensorData[0];
            sensor.stop = true;

            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(threshold, actions);

            bool result = automation.CheckValue(data);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckValue_IsBelowThreshold()
        {
            double threshold = 31.0;
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            sensor.EnableSensor();
            Thread.Sleep(5000);
            SensorData data = sensor.sensorData[0];
            sensor.stop = true;

            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(threshold, actions);

            bool result = automation.CheckValue(data);
            
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void SetSubscriptions()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            automation.Subscribe(sensor);

            CollectionAssert.Contains(automation.sensors, sensor);
        }

        [TestMethod()]
        public void Subscribe()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            automation.Subscribe(sensor);

            CollectionAssert.Contains(automation.sensors, sensor);
        }

        [TestMethod()]
        public void Unsubscribe()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            TemperatureSensor sensor = new TemperatureSensor(1, "test");
            automation.Subscribe(sensor);

            automation.Unsubscribe(sensor);

            Assert.IsFalse(automation.sensors.Contains(sensor));
        }

        [TestMethod()]
        public void UnsubscribeAll()
        {
            List<IAction> actions = new List<IAction>();
            ExceedThresholdAutomation automation = new ExceedThresholdAutomation(1.0, actions);
            TemperatureSensor sensor1 = new TemperatureSensor(1, "test1");
            TemperatureSensor sensor2 = new TemperatureSensor(1, "test2");
            List<ISensor>sensors = new List<ISensor>() { sensor1, sensor2 };
            automation.SetSubscriptions(sensors);
            automation.UnsubscribeAll();

            Assert.IsFalse(automation.sensors.Contains(sensor1) && automation.sensors.Contains(sensor2));
        }
    }
}