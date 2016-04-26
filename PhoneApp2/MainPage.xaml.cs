using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;


namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        //declaration of variable accelerometer
        Accelerometer accelerometer;
       
        int count = 0;
        bool step;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (!Accelerometer.IsSupported)
            {
                // Checks if the accelerometer is supported and if not it notifies the user and disables the start/stop buttons
               
                statusTextBlock.Text = "device does not support accelerometer";
                startButton.IsEnabled = false;
                stopButton.IsEnabled = false;
            }
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (accelerometer == null)
            {
                //checks if the accelerometer is null and if so it is initilized 
                
                accelerometer = new Accelerometer();
                //sets how fast data is recieves 
                accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometer.CurrentValueChanged +=
                    new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
                try
                {
                    //displays messages to user depending on whether the accelerometer has started 
                    statusTextBlock.Text = "starting accelerometer.";
                    accelerometer.Start();
                }
                catch (InvalidOperationException )
                {
                    statusTextBlock.Text = "unable to start accelerometer.";
                }
            }
        }
            void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
{
                
                 // Call UpdateUI on the UI thread and pass the AccelerometerReading.
                    Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
}
        private void UpdateUI(AccelerometerReading accelerometerReading)
{
             statusTextBlock.Text = "getting data";

             
                 Vector3 acceleration = accelerometerReading.Acceleration;

            // Show the numeric values.
               
                textBox1.Text = count.ToString("");
               
            // Show the values graphically.
            // states that if a certain axis is larger than a value then to add 1 
                if
                    (acceleration.Z > -0.35  && step == false)
                {
                    count++;
                    step = true;
                }

                if (acceleration.Z < -0.35)
                {
                    step = false;
                }

              
}
        // if the accelerometer is not null then stop it
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (accelerometer != null)
             {
                // Stop the accelerometer.
                accelerometer.Stop();
                statusTextBlock.Text = "accelerometer stopped.";
            }
        }
        }
    }
