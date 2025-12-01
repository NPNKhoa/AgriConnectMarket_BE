namespace AgriConnectMarket.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        string RenderTemplate(string templateFileName, Dictionary<string, string> values);
    }
}
