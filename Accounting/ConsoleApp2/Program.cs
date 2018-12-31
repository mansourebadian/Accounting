using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using Accounting.DataLayer.Context;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //ICustomerRepository customer = new CustomerRepository();
            UnitOfWork db = new UnitOfWork();
            var list = db.CustomerRepository.GetAllCustomers();
            db.Dispose();
        }
    }
}
