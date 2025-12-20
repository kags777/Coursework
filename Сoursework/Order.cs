using System.Collections.Generic;

namespace Coursework
{
    public class Order : IOrderServ
    {
        public Client ClientSender { get; set; }
        public string LoadingAddress { get; set; }
        public string UnloadingAddress { get; set; }
        public float RouteLength { get; set; }
        public float Cost { get; set; }
        public List<Cargo> Loads { get; set; } = new List<Cargo>();

        public Driver AssignedDriver { get; set; }
        public Car AssignedCar { get; set; }

        public int BaseRate = 15000;
        public float RiskCoefficient = 1.0f;

        // Добавляем статус заказа
        public string Status { get; set; } = "Создан"; // Возможные значения: "Создан", "Активен", "Завершён"
        public string DisplayName
        {
            get
            {
                if (ClientSender == null)
                    return "(Нет клиента)";
                return ClientSender.ClientType == "Физическое"
                    ? ClientSender.NameClient
                    : ClientSender.NameLegalEntity;
            }
        }
        public void CalcCost(int insuranceCost, bool fragileCargo, int routeLength)
        {
            if (fragileCargo) RiskCoefficient += 0.5f;
            if (routeLength > 1000) RiskCoefficient += 0.2f;
            Cost = (BaseRate * routeLength) + (insuranceCost * RiskCoefficient);
        }
    }
}
