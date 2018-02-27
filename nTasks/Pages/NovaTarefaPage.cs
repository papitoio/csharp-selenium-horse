using nTasks.Model;
using OpenQA.Selenium;

namespace nTasks.Pages
{
    public class NovaTarefaPage
    {
        private IWebDriver driver;

        public NovaTarefaPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Adiciona(TarefasModel tarefa)
        {
            IWebElement txtNome = driver.FindElement(By.Name("title"));
            IWebElement txtData = driver.FindElement(By.Name("dueDate"));
            IWebElement txtTag = driver.FindElement(By.CssSelector("div[class*=tagsinput] input"));

            txtNome.SendKeys(tarefa.Nome);
            txtData.SendKeys(tarefa.Data);

            txtTag.SendKeys(tarefa.Tags);
            txtTag.SendKeys(Keys.Tab);

            txtNome.Submit();
        }
    }
}
