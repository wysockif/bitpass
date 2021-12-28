using Application.ViewModels;
using AutoMapper;
using Domain.Model;

namespace Application.Utils.AutoMapperProfiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<CipherLogin, CipherLoginViewModel>();
            CreateMap<AccountActivity, AccountActivityViewModel>();
            CreateMap<Session, SessionViewModel>();
        }
    }
}