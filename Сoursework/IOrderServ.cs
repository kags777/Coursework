using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public interface IOrderServ
    {
        void CalcCost(int insuranceСost, bool fragileCargo, int routeLength);
    }
}
