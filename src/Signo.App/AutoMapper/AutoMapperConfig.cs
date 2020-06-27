using AutoMapper;
using Signo.Business.Models;
using Signo.App.ViewModels;


namespace Signo.App.AutoMapper

{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Integrante, IntegranteViewModel>().ReverseMap();
           
            // caminho de mão unica, sei transformar fornecedor em fornecedorView model, poisso no fim usa o reverse
            // perfil AUTOMAPPER funciona automatico e insere automapper nas controller
        }
    }
}