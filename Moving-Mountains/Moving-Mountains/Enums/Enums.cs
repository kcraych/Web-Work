using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Enums
{
    public class Enums
    {
        public enum State
        {
            CO
        }

        public enum LocationType
        {
            Home,
            Activity
        }

        public enum ActivityType
        {
            Trail,
            Sightsee,
            Course,
            Other
        }

        public enum StateTerritory
        {
            Central,
            Central_East,
            Central_West,
            North_Central,
            North_East,
            North_West,
            South_Central,
            South_East,
            South_West
        }
    }
}