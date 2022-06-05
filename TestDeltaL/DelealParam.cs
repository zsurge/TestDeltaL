using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeltaL
{
    class DelealParam
    {
        public string Id { get; set; } = "";
        public int TestStep { get; set; } = 1;
        public string Method { get; set; } = "3L";//"Description_Default";
        public string Layer { get; set; } = "L1";//"Layer1";
        public string Description { get; set; } = "";
        public int ShortLength { get; set; } = 2;
        public int MediumLength { get; set; } = 5;
        public int LongLength { get; set; } = 12;
        public string RecordPath { get; set; } = "";
        public bool SaveCurve { get; set; } = true;
        public bool SaveImage { get; set; } = true;
    }

    class ResultLimite
    {
        public string Id { get; set; } = "";
        public double Frequency{ get; set; } = 1;
        public double LossLowerLimite { get; set; } = 0.0D;//"Description_Default";
        public double LossUpperLimite { get; set; } = 0.0D;//"Layer1";
        public double Uncertainty { get; set; } = 0D;
        public double Difference { get; set; } = 0D;
    }
}
