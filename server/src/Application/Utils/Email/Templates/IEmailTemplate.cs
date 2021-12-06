namespace Application.Utils.Email.Templates
{
    public interface IEmailTemplateData
    {
    }

    public interface IEmailTemplate<in T> where T : IEmailTemplateData
    {
        string RenderTitle(T data);
        string RenderBody(T data);
    }
}