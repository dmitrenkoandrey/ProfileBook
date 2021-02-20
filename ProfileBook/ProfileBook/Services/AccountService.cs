using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Threading;
using ProfileBook.TreeView;
using ProfileBook.Models;

namespace ProfileBook.Service
{
    public class AccountService
    { 
    //    public AccountService(string endPoint, string sessionToken)
    //    {
    //        this.EndPoint = endPoint;

    //        CustomHeaders = new NameValueCollection();

    //        if (sessionToken != null)
    //        {
    //            CustomHeaders[SESSION_TOKEN_HEADER] = sessionToken;
    //        }
    //    }

    //    public async Task<string> Login(IUserLogin Credential)
    //    {
    //        //var args = new Dictionary<string, object>();
    //        //args["UserName"] = Credential.UserName;
    //        //args["Password"] = Credential.Password;
    //        //return await InvokeAsync<string>("userLogin", args);

    //        // emulate tocken
    //        var tocken = await Task.Factory.StartNew(() =>
    //        {
    //            Thread.Sleep(1000);
    //            return Guid.NewGuid().ToString();
    //        });
    //        return tocken; 
    //    }

    //    public async Task<bool> Logout()
    //    {
    //        //var args = new Dictionary<string, object>();
    //        //return await InvokeAsync<string>("userLogout", args);

    //        //emulate logout
    //        await Task.Factory.StartNew(() =>
    //        {
    //            Thread.Sleep(500);
    //            return;
    //        });

    //        return true;
    //    }

    //   // public async Task<Person> getUserInfo()
    //   // {
    //        //var args = new Dictionary<string, object>();
    //        //return await InvokeAsync<string>("getUserInfo", args);

    //        // emulate get user info
    //       // var userInfo = await Task.Factory.StartNew(() =>
    //     //   {
    //       //     Thread.Sleep(500);

    //           // var empl = new Persons()
    //            //{
    //              //  Id = Guid.NewGuid(),
    //              //  UserName = "user",
    //              //  FirstName = "Bill",
    //              //  LastName = "Ivanov",
    //              //  DateOfBirth = DateTime.Today.AddYears(-30),
    //              //  EMail = "mail@mtig.com", 
    //              //  Phone = "061 747 77 77"
    //            //};

                
    }
}
