using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoSe22_SE_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoSe22_SE_SmartHome.Tests
{
    /************************** Benötigt wohl Mockup der gesamten Application
    [TestClass()]
    public class AppTests
    {
        [TestMethod()]
        public void AddSensor_EmptySensorList()
        {
            App app = new App();
            Sensors sensorType = Sensors.TemperatureSensor;
            app.AddSensor(sensorType, "test");
            Assert.IsTrue(app.sensorList[0].GetSensorName().Equals("test") && app.sensorList[0].GetType() == typeof(TemperatureSensor));
        }

        [TestMethod()]
        public void AddSensor_FilledSensorList()
        {
            App app = new App();
            app.sensorList.Add(new CO2Sensor(1, "co2sensor"));
            Sensors sensorType = Sensors.TemperatureSensor;
            app.AddSensor(sensorType, "tempsensor");
            Assert.IsTrue(app.sensorList[1].GetSensorName().Equals("tempsensor") && app.sensorList[1].GetType() == typeof(TemperatureSensor));
        }

        [TestMethod()]
        public void EditSensor()
        {
            App app = new App();
            Sensors sensorType = Sensors.TemperatureSensor;
            app.AddSensor(sensorType, "test");
            ISensor sensor = app.sensorList[0];
            app.EditSensor(sensor, "test2");

            Assert.IsTrue(app.sensorList[0].GetSensorName().Equals("test2"));
        }

        [TestMethod()]
        public void AddAutomation_EmptyAutomationList_EmptySensorList()
        {
            App app = new App();
            
            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();
            
            app.AddAutomation(automationType, threshold, actionType, sensors);
            
            Assert.IsTrue(app.automationList[0].GetThreshold() == threshold && app.automationList[0].GetType() == typeof(ExceedThresholdAutomation));
        }

        [TestMethod()]
        public void AddAutomation_EmptyAutomationList_FilledSensorList()
        {
            App app = new App();

            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>
            {
                new TemperatureSensor(1, "tempSensor1"),
                new TemperatureSensor(2, "tempSensor2")
            };

            app.AddAutomation(automationType, threshold, actionType, sensors);

            Assert.IsTrue(app.automationList[0].GetThreshold() == threshold && app.automationList[0].GetType() == typeof(ExceedThresholdAutomation));
        }

        [TestMethod()]
        public void AddAutomation_FilledAutomationList_EmptySensorList()
        {
            App app = new App();

            app.automationList.Add(new FallBelowThresholdAutomation(2.0, new List<IAction> { new NotifyAction()})) ;

            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();

            app.AddAutomation(automationType, threshold, actionType, sensors);

            Assert.IsTrue(app.automationList[1].GetThreshold() == threshold && app.automationList[1].GetType() == typeof(ExceedThresholdAutomation));
        }

        [TestMethod()]
        public void EditAutomation_AutomationIsInAutomationList()
        {
            App app = new App();
            
            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();
           
            app.AddAutomation(automationType, threshold, actionType, sensors);

            double newThreshold = 13.0;
            sensors.Add(new TemperatureSensor(1, "tempSensor"));
            app.EditAutomation(app.automationList[0], newThreshold, actionType, sensors);
            
            Assert.IsTrue(app.automationList[0].GetThreshold() == newThreshold && app.automationList[0].GetSensors()[0].GetSensorName().Equals("tempSensor"));
        }

        public void EditAutomation_AutomationIsNotInAutomationList()
        {
            App app = new App();

            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();

            app.AddAutomation(automationType, threshold, actionType, sensors);

            double newThreshold = 13.0;
            sensors.Add(new TemperatureSensor(1, "tempSensor"));
            
            ExceedThresholdAutomation differentAutomation = new ExceedThresholdAutomation(threshold, new List<IAction> { new NotifyAction() });

            app.EditAutomation(differentAutomation, newThreshold, actionType, sensors);

            Assert.IsTrue(app.automationList[0].GetThreshold() == threshold && app.automationList[0].GetSensors().Count == 0);
        }

        [TestMethod()]
        public void DeleteAutomation_AutomationIsInAutomationList()
        {
            App app = new App();

            app.automationList.Add(new FallBelowThresholdAutomation(2.0, new List<IAction> { new NotifyAction() }));

            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();

            app.AddAutomation(automationType, threshold, actionType, sensors);

            app.DeleteAutomation(app.automationList[0]);

            Assert.IsTrue(app.automationList.Count() == 1);
        }

        [TestMethod()]
        public void DeleteAutomation_AutomationIsNotInAutomationList()
        {
            App app = new App();


            Automations automationType = Automations.ExceedThresholdAutomation;
            Actions actionType = Actions.NotifyAction;
            double threshold = 12.0;
            List<ISensor> sensors = new List<ISensor>();

            app.AddAutomation(automationType, threshold, actionType, sensors);

            ExceedThresholdAutomation differentAutomation = new ExceedThresholdAutomation(threshold, new List<IAction> { new NotifyAction() });

            app.DeleteAutomation(differentAutomation);

            Assert.IsTrue(app.automationList[0].GetThreshold() == threshold && app.automationList[0].GetType() == typeof(ExceedThresholdAutomation));
        }

        [TestMethod()]
        public void NewSensor_LightSensor()
        {
            App app = new App();
            ISensor newSensor = app.NewSensor(Sensors.LightSensor, 1, "test");

            Assert.IsTrue(newSensor.GetType() == typeof(LightSensor) && newSensor.GetSensorName().Equals("test"));
        }

        [TestMethod()]
        public void NewAction_NotifyAction()
        {
            App app = new App();
            IAction newAction = app.NewAction(Actions.NotifyAction);

            Assert.IsTrue(newAction.GetType() == typeof(NotifyAction)); 
        }

        [TestMethod()]
        public void NewAutomation_ExceedThresholdAutomation()
        {
            App app = new App();
            IAutomation newAutomation = app.NewAutomation(Automations.ExceedThresholdAutomation, 12.0, Actions.NotifyAction);

            Assert.IsTrue(newAutomation.GetType() == typeof(ExceedThresholdAutomation) && newAutomation.GetThreshold() == 12.0);
        }

        [TestMethod()]
        public void ShutdownApplicationTest()
        {
            App app = new App();
            app.sensorList.Add(new LightSensor(1, "lightSensor1"));
            app.automationList.Add(new ExceedThresholdAutomation(2.0, new List<IAction> { new NotifyAction() }));
            app.ShutdownApplication();

            Assert.IsTrue(app.sensorList.Count() == 0 && app.automationList.Count() == 0);
        }
    }
    */
}