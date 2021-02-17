using System;
using System.Collections.Generic;
using System.Text;

namespace fitnessTraker
{
    public interface IStepCounter
    {
        int Steps { get; set; }

        bool IsAvailable();

        void InitSensorService();

        void StopSensorService();
    }
}
