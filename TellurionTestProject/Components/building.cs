using System;
using System.Collections.Generic;
using System.Text;

namespace TellurionTestProject.Components
{
    public class Building
    {
        public int Level { get; set; }

        public double UnitsCount { get; set; }

        public OwnerEnum Owner { get; set; }

        public TimeSpan? UpgradeStart { get; set; }
    }
}
