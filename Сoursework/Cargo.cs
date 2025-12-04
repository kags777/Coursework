using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class Cargo
    {
        public Cargo() { }

        private string nomination; 
        private int quantity;
        private float weight;
        private int insuranceСost;//стоимость страховки
        private int cost; //оценочная стоимость всех товаров
        private int baseInsurance = 5000;//фиксированная страховка
        private bool fragileCargo;

        public void CalcInsurance()
        {
            insuranceСost = baseInsurance + (cost * (3/100));
        }

    }
}
