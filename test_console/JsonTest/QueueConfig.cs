using System;

namespace JsonTest
{
 internal class QueueConfig
    {
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }
    }

    class Time
    {
        private int minutes;
        private int hours;

        public int Hours
        {
            get { return hours; }
            set
            {
                if (value >= 0 && value <= 23)
                {
                hours = value;
                 return;
                }
                hours = 0;
            }
        }
        public int Minutes
        {
            get
            {
                return minutes;
            }

            set
            {
                if (value >= 0 && value <= 59)
                {
                    minutes = value;
                    return;
                }
                minutes = 0;
            }
        }
    }
}