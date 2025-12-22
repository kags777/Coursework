using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public interface IOrderServ
    {
        Client ClientSender { get; set; }
         Client ClientReceiver { get; set; }

        string LoadingAddress { get; set; }
        string UnloadingAddress { get; set; }

        float RouteLength { get; set; }
        float Cost { get; set; }

        List<Cargo> Loads { get; set; }

        Driver AssignedDriver { get; set; }
        Car AssignedCar { get; set; }

        System.DateTime StartDate { get; set; }
        System.DateTime EndDate { get; set; }

        void CalcCost(int insuranceCost, bool fragileCargo, int routeLength);
    }
}
