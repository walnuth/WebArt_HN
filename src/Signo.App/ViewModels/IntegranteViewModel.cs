using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Signo.Business.Models;


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


        [DisplayName("Matrícula")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
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



        [DisplayName("CEP")]

        public int CepEndereco { get; set; }

        [DisplayName("Logradouro")]
        public string LogradouroEndereco { get; set; }

        [DisplayName("Número")]
        public string NumeroEndereco { get; set; }

        [DisplayName("Complemento")]
        public string ComplementoEndereco { get; set; }

        [DisplayName("Bairro")]
        public string BairroEndereco { get; set; }

        [DisplayName("Cidade")]
        public string LocalidadeEndereco { get; set; }

        [DisplayName("Estado")]
        public string UfEndereco { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve no máximo {1} caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nacionalidade { get; set; }

        [DisplayName("Admin?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool Admin { get; set; }

        [DisplayName("Ativo?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool Ativo { get; set; }

        [DisplayName("Gênero")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Genero { get; set; }



        [DisplayName("Data da Última Edição")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Admissao { get; set; }

        [DisplayName("Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime DoB { get; set; }



        [DisplayName("Foto do Integrante")]
        public IFormFile ImgFotoUpload { get; set; }

        [DisplayName("Foto")]
        public string ImgFoto { get; set; }

        [DisplayName("Assinatura Digitalizada")]
        public IFormFile ImgSignUpload { get; set; }

        [DisplayName("Assinatura Digital")]
        public string ImgSign { get; set; }


        public IEnumerable<EnumRig> Unidades { get; set; }
        public IEnumerable<IntegranteViewModel> Integrantes { get; set; }

        public IEnumerable<Claim> UserClaims { get; set; }

        [DisplayName("Sistema")]
        public string ManipulateUserClaimsType { get; set; }

        [DisplayName("Ações Permitidas")]
        public string ManipulateUserClaimsValue { get; set; }
    }


}
