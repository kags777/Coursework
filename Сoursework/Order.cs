using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class Order : IOrderServ
    {
        public DateTime DateLoading { get; set; }
        public string ClientSender { get; set; }
        public string LoadingAddress { get; set; }
        public string ClientRecipient { get; set; }
        public string UnloadingAddress { get; set; }
        public float RouteLength { get; set; }
        public float Cost { get; set; }
        public List<Cargo> Loads { get; set; } = new List<Cargo>();

        public int baseRate = 15000;//Базовая ставка на заказ
        public float riskCoefficient = 1.0f; // базовый риск

        public void CalcCost(int insuranceСost, bool fragileCargo, int routeLength)
        {   
            if (fragileCargo == true) riskCoefficient += 0.5f;
            if (routeLength > 1000) riskCoefficient += 0.2f;
            Cost = (baseRate * routeLength) + (insuranceСost * riskCoefficient);
        }
    }
}
