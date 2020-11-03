using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Live_UWP
{
    public class ViewModel
    {
        private ObservableCollection<OrderInfo> _orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public ViewModel()
        {
            _orders = new ObservableCollection<OrderInfo>();
            this.GenerateOrders();
        }

        private void GenerateOrders()
        {
            _orders.Add(new OrderInfo(1001, "Maria Anders", "Germany", "ALFKI", 5));
            _orders.Add(new OrderInfo(1002, "Ana Trujilo", "Mexico", "ANATR", 10));
            _orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", 5));
            _orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", 10));
            _orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", 5));
            _orders.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", 10));
            _orders.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", 5));
            _orders.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", 10));
            _orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", 5));
            _orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", 10));
        }
    }
}
