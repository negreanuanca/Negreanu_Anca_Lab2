using System;
using System.Collections.Generic;
using System.Text;

namespace Negreanu_Anca_Lab2
{
    class Doughnut
    {
        private DoughnutType mFlavour;
        public DoughnutType Flavour
        {
            get
            {
                return mFlavour;
            }
            set
            {
                mFlavour = value;
            }
        }
        private float mPrice;
        public float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }
        public Doughnut(DoughnutType aFlavour) //constructor
        {
            mFlavour = aFlavour;
        }
        public enum DoughnutType
        {
            Glazed,
            Sugar,
            Lemon,
            Chocolate,
            Vanilla
        }
    }
}
