using FruitBasket.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FruitBasket.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int AvailableFruitSpace { get; set; }
        public int FruitLimit { get; set; } = 100;
        public virtual ObservableCollection<Fruit> Fruit { get; set; }

    }
}
