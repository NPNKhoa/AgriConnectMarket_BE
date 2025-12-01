using AgriConnectMarket.Application.Interfaces;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly string _templateFolder;

        public EmailTemplateService()
        {
            _templateFolder = Path.Combine(AppContext.BaseDirectory, "EmailTemplates");
        }

        public string RenderTemplate(string templateFileName, Dictionary<string, string> values)
        {
            var path = Path.Combine(_templateFolder, templateFileName);

            if (!File.Exists(path))
                throw new FileNotFoundException($"Template not found: {path}");

            string content = File.ReadAllText(path);

            foreach (var kv in values)
            {
                content = content.Replace($"{{{{{kv.Key}}}}}", kv.Value);
            }

            return content;
        }
    }
}
