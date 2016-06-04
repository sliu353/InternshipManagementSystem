using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.MyUserManager
{
    public class MyUserManager
    {
        private static ApplicationUserManager myUserManager;

        public static void SetMyUserManager(ApplicationUserManager myUserManager)
        {
            MyUserManager.myUserManager = myUserManager;
        }

        public static async void DeleteCompany(string companyToBeDeletedEmail)
        {
            var companyToBeDeleted = myUserManager.Users.Where(u => u.Email == companyToBeDeletedEmail).FirstOrDefault();
            await myUserManager.DeleteAsync(companyToBeDeleted);
        }
    }
}