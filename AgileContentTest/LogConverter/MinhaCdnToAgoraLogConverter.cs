using System;
using System.Globalization;
using System.Linq;

namespace AgileContentTest
{
    public class MinhaCdnToAgoraLogConverter
    {
        private LogRowData ParseMinhaCdnRow(string input)
        {
            var splitted = input.Split("|");
            var httpRequestData = splitted[3].Replace("\"", "").Split(" ");

            return new LogRowData
            {
                ResponseSize = splitted[0],
                StatusCode = splitted[1],
                CacheStatus = splitted[2],
                TimeTaken = Decimal.Parse(splitted[4], NumberStyles.Float, CultureInfo.InvariantCulture),
                HttpMethod = httpRequestData[0],
                UriPath = httpRequestData[1],
                Provider = "MINHA CDN"
            };
        }

        public string Convert(string input)
        {
            var header = $"#Version: 1.0{Environment.NewLine}";
            header += $"#Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}{Environment.NewLine}";
            header += "#Fields: provider http-method status-code uri-path time-taken response-size cache-status";

            var inputRows = input.Split(Environment.NewLine).Where(row => !String.IsNullOrWhiteSpace(row));

            var convertedRows = inputRows.Select(row =>
            {
                var parsedRow = ParseMinhaCdnRow(row);
                var cacheStatus = parsedRow.CacheStatus == "INVALIDATE"
                                  ? "REFRESH_HIT"
                                  : parsedRow.CacheStatus;
                return $"\"{parsedRow.Provider}\" {parsedRow.HttpMethod} " +
                       $"{parsedRow.StatusCode} {parsedRow.UriPath} {Math.Round(parsedRow.TimeTaken, 0)} " +
                       $"{parsedRow.ResponseSize} {cacheStatus}";
            }).ToList();

            return $"{header}{Environment.NewLine}{String.Join(Environment.NewLine, convertedRows)}";
        }
    }
}
