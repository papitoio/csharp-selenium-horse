using nTasks.Componentes;
using nTasks.Comum;
using nTasks.Model;
using nTasks.Pages;
using NUnit.Framework;
using System.Configuration;

namespace nTasks
{
    public class TarefasTests : BaseTest
    {
        private LoginPage login;
        private TarefasPage tarefas;
        private Toaster toast;

        [SetUp]
        public void Start()
        {
            this.login = new LoginPage(driver);
            this.tarefas = new TarefasPage(driver);
            this.toast = new Toaster(driver);

            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["apiRemoveUserTasks"]);
        }

        [Test]
        public void AdicionarNovaTarefa()
        {
            login.Acessa();
            login.FazLogin("kd@qaninja.io", "secret");

            var tarefa = new TarefasModel
            (
               "Planejar minha viagem para Europa",
               "2017/11/28",
               "viagem"
            );

            tarefas.Novo().Adiciona(tarefa);
            
            Assert.AreEqual("The task has been added.", toast.RetornaMensagem());
            Assert.IsTrue(tarefas.VerificaCadastro(tarefa), "Erro ao verificar cadastro de tarefa");
        }


    }
}
