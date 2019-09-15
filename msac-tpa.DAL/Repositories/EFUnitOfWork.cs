using System;
using System.Collections.Generic;
using System.Text;
using msac_tpa.DAL.Interfaces;

namespace msac_tpa.DAL.Repositories
{
//    public class EFUnitOfWork : IUnitOfWork
//    {
//        private MobileContext db;
//        private PhoneRepository phoneRepository;
//        private OrderRepository orderRepository;

//        public EFUnitOfWork(string connectionString)
//        {
//            db = new MobileContext(connectionString);
//        }
//        public IRepository<Phone> Phones
//        {
//            get
//            {
//                if (phoneRepository == null)
//                    phoneRepository = new PhoneRepository(db);
//                return phoneRepository;
//            }
//        }

//        public IRepository<Order> Orders
//        {
//            get
//            {
//                if (orderRepository == null)
//                    orderRepository = new OrderRepository(db);
//                return orderRepository;
//            }
//        }

//        public void Save()
//        {
//            db.SaveChanges();
//        }

//        private bool disposed = false;

//        public virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    db.Dispose();
//                }
//                this.disposed = true;
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
}