namespace Coursework
{
    public class Cargo : ICargoServ
    {
        public string Nomination { get; set; }
        public int Quantity { get; set; }
        public float Weight { get; set; }
        public int InsuranceCost { get; set; }
        public int Cost { get; set; }
        public int BaseInsurance { get; set; } = 5000;
        public bool FragileCargo { get; set; }

        public void CalcInsurance()
        {
            InsuranceCost = BaseInsurance + (int)(Cost * 0.03f);
        }
    }
}