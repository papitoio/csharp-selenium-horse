namespace nTasks.Model
{
    public class TarefasModel
    {
        public string Nome { get; set; }
        public string Data { get; set; }
        public string Tags { get; set; }

        public TarefasModel(string nome, string data, string tags)
        {
            this.Nome = nome;
            this.Data = data;
            this.Tags = tags;
        }
    }
}
