using System.Threading.Tasks;

namespace Application.InfrastructureInterfaces
{
    public interface IEmailGateway
    {
        Task SendEmailAsync(string to, string title, string body);
    }
}