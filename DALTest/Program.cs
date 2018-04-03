using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolodDAL.Repositories;
using HolodDAL.Sorting;
using System.Linq.Expressions;
using HolodDAL.Filtering;
using CallCenterDAL.Repositories;
using CallCenterDAL;

namespace DALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EFUnitOfWork test = new EFUnitOfWork("CallCenterDb");

            foreach (var item in test.PhoneCalls.GetAll(null, SortOrder.Asc))
            {
                //Console.WriteLine(String.Format("Id = {0}, Duration = {1}", item.Id, item.DurationSeconds));
            }

            test.Dispose();
        }
    }
}
