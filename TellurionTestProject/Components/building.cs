using System;
using System.Collections.Generic;
using System.Text;

namespace TellurionTestProject.Components
{
    public class building
    {
        public int Level { get; set; }

        public float UnitsCount { get; set; }

        public OwnerEnum Owner { get; set; }

        public TimeSpan? UpgradeStart { get; set; }
    }
}
