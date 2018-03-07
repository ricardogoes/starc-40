
using System.Configuration;

namespace STARC.Infra.Data.Configuration
{
    public class RepositoryConfiguration
    {
        public string __connectionString = ConfigurationManager.ConnectionStrings["STARCConnection"].ToString();
    }
}
