using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgileContentTest
{
    public class LogConverterApplication
    {
        private readonly MinhaCdnToAgoraLogConverter _converter;

        public LogConverterApplication(MinhaCdnToAgoraLogConverter converter)
        {
            _converter = converter;
        }

        public async Task ConvertMinhaCdnToAgora(string sourceUrl, string destinationPath)
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(sourceUrl);
            var result = _converter.Convert(content);
            File.WriteAllText(destinationPath, result);
        }
    }
}
