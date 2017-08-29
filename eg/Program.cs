using System;
using ParseFive.Parser;

namespace Demo
{
    using System.Linq;
    using System.Net.Http;

    static class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri(args.FirstOrDefault() ?? "https://www.example.com/");
            string html;
            using (var http = new HttpClient())
                html = http.GetStringAsync(url).GetAwaiter().GetResult();
            var parser = new Parser();
            parser.parse(html);
        }
    }
}
