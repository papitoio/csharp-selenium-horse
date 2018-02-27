using OpenQA.Selenium;
using System.Configuration;

namespace nTasks.Pages
{
    public class LoginPage
    {

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Acessa()
        {
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["defaultURL"]);
        }

        public void FazLogin(string email, string senha)
        {
            IWebElement campoEmail = driver.FindElement(By.Id("login_email"));
            IWebElement campoSenha = driver.FindElement(By.Id("login_password"));

            campoEmail.SendKeys(email);
            campoSenha.SendKeys(senha);

            campoEmail.Submit();
        }

        public bool EstouLogado(string email)
        {
            IWebElement menuUsuario = driver.FindElement(By.ClassName("profile-address"));

            return menuUsuario.Text.Contains(email);
        }
    }
}
