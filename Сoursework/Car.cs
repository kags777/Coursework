using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class Car : ICarServ
    {
        private string number; //госНомер
        private string brand;//марка
        private string model; //модель
        private int maxLoad; //грузоподъемность
        private string appointment; //назначение
        private int yearBorn; //год выпуска
        private int yearRepairs; //годКапРемонта
        private int mileage; //пробег
        private string photo; //хранит путь до файла


        public Car() { }

        //расчет пробега
        public void MileageCalc(int routeLength )
        {
            mileage = mileage + routeLength;
        }



    }
}
