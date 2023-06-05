using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeltaL
{
    public class ListData
    {
        public int Id { get; set; } = 0;
        public int TestStep { get; set; } = 1;
        public string Method { get; set; } = "3L";//"Description_Default";
        public string Layer { get; set; } = "L";//"Layer1";
        public string Description { get; set; } = "";
        public string ShortLength { get; set; } = "2";
        public int MediumLength { get; set; } = 5;
        public int LongLength { get; set; } = 12;
        public string RecordPath { get; set; } = "";
        public bool SaveCurve { get; set; } = true;
        public bool SaveImage { get; set; } = true;
    }

    public class MarkerData
    {
        public int Id { get; set; } = 1;
        public double Frequency{ get; set; } = 4;
        public double LossLowerLimite { get; set; } = 0.8D;//"Description_Default";
        public double LossUpperLimite { get; set; } = 1.2D;//"Layer1";
        public double Uncertainty { get; set; } = 0D;
        public double Difference { get; set; } = 0D;
    }

    public class KeyFreqLimit
    {
        public int Key { get; set; }
        public List<MarkerData> Data { get; set; } = new List<MarkerData>();
    }


    public class DeltaL
    {
        public List<ListData> Lists { get; set; }
        public List<MarkerData> Markers { get; set; }
    }


}
