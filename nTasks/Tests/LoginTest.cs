using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
using nTasks.Pages;
using System.Configuration;
using nTasks.Comum;

namespace nTasks
{
    public class LoginTest : BaseTest
    {
        private LoginPage login;

        [SetUp]
        public void Start()
        {
            this.login = new LoginPage(driver);
        }

        [Test]
        public void LoginComSucesso()
        {
            login.Acessa();
            login.FazLogin("kd@qaninja.io", "secret");

            Assert.True(login.EstouLogado("kd@qaninja.io"), "Verifica se o usuário está logado");
        }
    }
}
