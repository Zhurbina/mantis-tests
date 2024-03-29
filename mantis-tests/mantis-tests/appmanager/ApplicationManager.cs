﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager : TestBase
    {
        protected IWebDriver driver;
        protected string baseURL;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();


        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-1.2.17";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Project = new ProjectHelper(this);
            Admin = new AdminHelper(this, baseURL);

            Admin = new AdminHelper(this, baseURL);
        }


        ~ApplicationManager()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";

                app.Value = newInstance;
            }
            return app.Value;
        }

        //http://localhost/mantisbt-1.2.17/admin/install.php


        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public ProjectHelper Project { get; set; }
        public AdminHelper Admin { get; set; }
    }
}