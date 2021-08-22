using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4
{
    [Serializable]
    public struct Price : IComparable<Price>
    {
        public string nameOfProduct;
        public string nameOfShop;
        public int Amount;
        public Price(string LineWithPriceData)
        {
            string[] dataSplitted = LineWithPriceData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            nameOfProduct = dataSplitted[0];
            nameOfShop = dataSplitted[1];
            Amount = int.Parse(dataSplitted[2]);
        }
        public int CompareTo(Price other)
        {

            return this.nameOfProduct.CompareTo(other.nameOfProduct);
        }
        public override string ToString()
        { return nameOfProduct + ", " + nameOfShop + ", " + Amount; }
    }
}
