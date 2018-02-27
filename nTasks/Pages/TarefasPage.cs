using nTasks.Model;
using OpenQA.Selenium;
using System.Linq;

namespace nTasks.Pages
{
    public class TarefasPage
    {
        private IWebDriver driver;

        public TarefasPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public NovaTarefaPage Novo()
        {
            IWebElement botaoNovo = driver.FindElement(By.Id("insert-button"));
            botaoNovo.Click();
            return new NovaTarefaPage(driver);
        }

        public bool VerificaCadastro(TarefasModel tarefa)
        {
            var linhas = driver.FindElements(By.CssSelector("table tbody tr"));
            var alvo = linhas.FirstOrDefault(x => x.Text.Contains(tarefa.Nome));

            return alvo.Text.Contains(tarefa.Data) && alvo.Text.Contains(tarefa.Tags);
        }

        public bool VeriricaCadastroSimples(string nome)
        {
            return driver.PageSource.Contains(nome);
        }
    }
}
