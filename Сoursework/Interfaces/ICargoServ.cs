using System;

namespace Coursework
{
    public interface ICargoServ
    {
        string Nomination { get; set; }
        int Quantity { get; set; }
        float Weight { get; set; }
        int InsuranceCost { get; }
        int Cost { get; set; }
        int BaseInsurance { get; set; }
        bool FragileCargo { get; set; }

        void CalcInsurance();
    }
}