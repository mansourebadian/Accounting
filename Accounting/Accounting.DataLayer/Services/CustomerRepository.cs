﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using System.Data.Entity;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities db;
        public CustomerRepository(Accounting_DBEntities contex)
        {
            db = contex;
        }
        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public IEnumerable<Customers> GetCustomersByFilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

    

        public bool UpdateCustomer(Customers customer)
        {
            //try
            //{
            var local = db.Set<Customers>()
                         .Local
                         .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }
            db.Entry(customer).State=EntityState.Modified;
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
    }
}
