namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class ServerConfigsModel
    {
        public string baseCdnUrl;
        public int test2;

        public ServerConfigsModel()
        {
        }

        public ServerConfigsModel(string baseCdnUrl, int test2)
        {
            this.baseCdnUrl = baseCdnUrl;
            this.test2 = test2;
        }
    }
}