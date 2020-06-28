using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace Signo.App.ViewModels
{
    public class IntegranteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Unidade { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve no máximo {1} caracteres")]
        public string Matricula { get; set; }

        [DisplayName("Função a Bordo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string FuncaoBordo { get; set; }

        [DisplayName("Função Contrato")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string FuncaoContrato { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Empresa { get; set; }

        [DisplayName("Endereço")]
        [StringLength(350, ErrorMessage = "O campo {0} deve no máximo {1} caracteres")]
        public string Endereco { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve no máximo {1} caracteres")]
        public string Telefone { get; set; }

        [DisplayName("Foto do Integrante")]
        public IFormFile ImgFotoUpload { get; set; }

        [DisplayName("Foto")]
        public string ImgFoto { get; set; }

        [DisplayName("Assinatura Digitalizada")]
        public IFormFile ImgSignUpload { get; set; }

        [DisplayName("Assinatura Digital")]
        public string ImgSign { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nacionalidade { get; set; }

        [DisplayName("Adim?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool Admin { get; set; }

        [DisplayName("Ativo?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool Ativo { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Genero { get; set; }

        [DisplayName("Data de Adminissão")]
        public DateTime Admissao { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime DoB { get; set; }

       public IEnumerable<IntegranteViewModel> Integrantes { get; set; }

       public IEnumerable<Claim> UserClaims { get; set; }
       public string ManipulateUserClaimsType { get; set; }
       public string ManipulateUserClaimsValue { get; set; }
    }


}
