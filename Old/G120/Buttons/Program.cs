﻿using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Pins;

namespace Buttons {
    class Program {
        static GpioPin led1;
        static void Main() {
            var gpio = GpioController.GetDefault();
            led1 = gpio.OpenPin(G120E.GpioPin.P3_27);
            var btnSelect = gpio.OpenPin(G120E.GpioPin.P2_25);
            btnSelect.SetDriveMode(GpioPinDriveMode.InputPullUp);
            led1.SetDriveMode(GpioPinDriveMode.Output);

            btnSelect.DebounceTimeout = new System.TimeSpan(0, 0, 0, 0, 1);
            // Call this event when the button is pressed
            btnSelect.ValueChanged += btnSelect_ValueChanged;

            // you can also read the button directly
            var state = btnSelect.Read();

            // Sleep forever, low power!
            Thread.Sleep(Timeout.Infinite);
        }

        private static void btnSelect_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e) {
            if (e.Edge == GpioPinEdge.FallingEdge)
                led1.Write(GpioPinValue.High);
            else
                led1.Write(GpioPinValue.Low);
        }
    }
}
