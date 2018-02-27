using OpenQA.Selenium;


namespace nTasks.Componentes
{
    public class Toaster
    {
        private IWebDriver driver;

        public Toaster(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string RetornaMensagem()
        {
            var toast = driver.FindElement(By.Id("toast-container"));
            return toast.FindElement(By.ClassName("toast-message")).Text;
        }
    }
}
