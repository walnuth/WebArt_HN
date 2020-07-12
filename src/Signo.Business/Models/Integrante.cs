using System;


namespace Signo.Business.Models
{
    public class Integrante : Entity
    {

       public string Nome { get; set; }
        public string Unidade { get; set; }
        public string Matricula { get; set; }
        public string FuncaoBordo { get; set; }
        public string FuncaoContrato { get; set; }
        public string Empresa { get; set; }
        
        public string CepEndereco { get; set; }
        public string LogradouroEndereco { get; set; }
        public string BairroEndereco { get; set; }
        public string LocalidadeEndereco { get; set; }
        public string UfEndereco { get; set; }
        public string NumeroEndereco { get; set; }
        public string ComplementoEndereco { get; set; }
        public string Telefone { get; set; }
        public string ImgFoto { get; set; }
        public string ImgSign { get; set; }
        public string Nacionalidade { get; set; }
        public bool Admin { get; set; }
        public bool Ativo { get; set; }
        public int Genero { get; set; }
        public DateTime Admissao { get; set; }
        public DateTime DoB { get; set; }
    }

}
