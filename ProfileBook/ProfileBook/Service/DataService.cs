using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProfileBook.Models;
using ProfileBook.TreeView;
using ProfileBook.Service;

namespace ProfileBook.Service
{
    public class DataService : BaseService
    {
        //        //private static List<Department> departments;
        //private static List<Persons> employees;

        //        static DataService()
        //        {
        //            // fill static department and employee

        //            departments = new List<Department>();
        //            employees = new List<Employee>();

        //            var d1 = new Department() { Name = "Root department" };
        //            var d2 = new Department() { Name = "Department 1", Parent_Id = d1.Id };
        //            var d3 = new Department() { Name = "Department 2", Parent_Id = d2.Id };
        //            var d4 = new Department() { Name = "Department 3", Parent_Id = d1.Id };
        //            departments.Add(d1);
        //            departments.Add(d2);
        //            departments.Add(d3);
        //            departments.Add(d4);

        //            var u1 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user1login",
        //                FirstName = "Boss",
        //                LastName = "MainBoss",
        //                EMail = "asss@asdf.com",
        //                Phone = "095 55-22-444",
        //                DateOfBirth = DateTime.Today.AddYears(-30), 
        //                Performance = 95,
        //                Department = d1
        //            }; d1.Employees.Add(u1); employees.Add(u1);
        //            var u2 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user2login",
        //                FirstName = "Oleg",
        //                LastName = "Vetrov",
        //                EMail = "oleg.vetrov@metinvestholding.com",
        //                Phone = "095 575-55-10",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = 75,
        //                Department = d2
        //            }; d2.Employees.Add(u2); employees.Add(u2);

        //            var u3 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user3login",
        //                FirstName = "Natalia",
        //                LastName = "Vetrova",
        //                EMail = "a3@asdf.com",
        //                Phone = "095 605 45-49",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = 50,
        //                Department = d3
        //            }; d3.Employees.Add(u3); employees.Add(u3);
        //            var u4 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user4login",
        //                FirstName = "Andrey",
        //                LastName = "Dmitrenko",
        //                EMail = "a4@asdf.com",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = -50,
        //                Department = d3, 
        //                Phone = "050 673 2196"
        //            }; d3.Employees.Add(u4); employees.Add(u4);
        //            var u5 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user5login",
        //                FirstName = "User5",
        //                LastName = "LastName5",
        //                EMail = "a5@asdf.com",
        //                Phone = "095 77-11-333",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = 25,
        //                Department = d3
        //            }; d3.Employees.Add(u5); employees.Add(u5);
        //            var u6 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user6login",
        //                FirstName = "User6",
        //                LastName = "LastName6",
        //                EMail = "a6@asdf.com",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = 60,
        //                Department = d3
        //            }; d3.Employees.Add(u6); employees.Add(u6);
        //            var u7 = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                UserName = "user7login",
        //                FirstName = "User7",
        //                LastName = "LastName7",
        //                EMail = "a7@asdf.com",
        //                DateOfBirth = DateTime.Today.AddYears(-30),
        //                Performance = 15,
        //                Department = d4
        //            }; d4.Employees.Add(u7); employees.Add(u7);
        //        }

        public DataService(string endPoint, string sessionToken)
        {
            this.EndPoint = endPoint;

            CustomHeaders = new NameValueCollection();

            if (sessionToken != null)
            {
                CustomHeaders[SESSION_TOKEN_HEADER] = sessionToken;
            }
        }

        //public async Task<List<Employee>> getEmployees()
        //{
        //    //var args = new Dictionary<string, object>();
        //    //return await InvokeAsync<string>("getEmployees", args);

        //    // emulate get Employees
        //    var empls = await Task.Factory.StartNew(() =>
        //    {
        //        Thread.Sleep(500);

        //       // return employees;
        //    });
        //    return empls;
        //}

        //public async Task<List<Department>> getDepartments()
        //{
        //    //var args = new Dictionary<string, object>();
        //    //return await InvokeAsync<string>("getEmployees", args);

        //    // emulate get Employees
        //    var deps = await Task.Factory.StartNew(() =>
        //    {
        //        Thread.Sleep(500);

        //        return departments;
        //    });
        //    return deps;
        //}
    }
}
