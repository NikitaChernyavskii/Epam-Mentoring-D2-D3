using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Sample03
{
    [TestClass]
    public class E3SProviderTests
    {
        [TestMethod]
        public void WithoutProvider()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS<EmployeeEntity>("workstation:(EPRUIZHW0249)", 0, 1);

            foreach (var emp in res)
            {
                Console.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithoutProviderNonGeneric()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPRUIZHW0249)", 0, 10);

            foreach (var emp in res.OfType<EmployeeEntity>())
            {
                Console.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }


        [TestMethod]
        public void WithProvider_Subtask1()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"],
                ConfigurationManager.AppSettings["password"]);

            //foreach (var emp in employees.Where(e => e.workStation == "EPRUIZHW0249"))
            //{
            //    Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            //}

            //foreach (var emp in employees.Where(e => "EPRUIZHW0249" == e.workStation))
            //{
            //    Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            //}

            //foreach (var emp in employees.Where(e => e.workStation.StartsWith("EPRUIZHW0249")))
            //{
            //    Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            //}

            //foreach (var emp in employees.Where(e => e.workStation.EndsWith("HW0249")))
            //{
            //    Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            //}

            //foreach (var emp in employees.Where(e => e.workStation.Contains("HW0249")))
            //{
            //    Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            //}

            foreach (var emp in employees.Where(e => e.workStation.Contains("HW0249") && e.nativeName == "Михаил Романов"))
            {
                Debug.WriteLine("{0} {1}", emp.nativeName, emp.shortStartWorkDate);
            }
        }
    }
}
