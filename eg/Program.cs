namespace Demo
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using ParseFive.Parser;

    static class Program
    {
        static void Main(string[] args)
        {
            string html;

            var source = args.FirstOrDefault() ?? "-";
            if (source == "-")
            {
                html = Console.In.ReadToEnd();
            }
            else if (Uri.TryCreate(source, UriKind.Absolute, out var url) &&
                     url.Scheme != Uri.UriSchemeFile)
            {
                using (var http = new HttpClient())
                    html = http.GetStringAsync(url).GetAwaiter().GetResult();
            }
            else
            {
                html = File.ReadAllText(source);
            }

            var parser = new Parser();
            var doc = parser.parse(html);
            char[] indent = {};
            Dump(doc, Console.Out);
            Console.WriteLine();

            void Print(TextWriter output, int level, params string[] strings)
            {
                if (level > 0)
                {
                    var width = level * 2;
                    if (indent.Length < width)
                        indent = ("|" + new string(' ', width - 1)).ToCharArray();
                    output.Write(indent, 0, width);
                }
                foreach (var s in strings)
                    output.Write(s);
                output.WriteLine();
            }

            void Dump(Node node, TextWriter output, int level = 0)
            {
                switch (node)
                {
                    case Element e:
                        Print(output, level, "<", e.TagName, ">");
                        foreach (var a in e.Attributes)
                            Print(output, level + 1, a.name, "=", Jsonify(a.value));
                        break;
                    case Text t:
                        Print(output, level, Jsonify(t.Value));
                        return;
                    case Comment c:
                        Print(output, level, "#comment", Jsonify(c.Data));
                        return;
                }

                foreach (var child in node.ChildNodes)
                    Dump(child, output, level + 1);
            }
        }

        static string Jsonify(string s) => Jsonify(s, null).ToString();

        static StringBuilder Jsonify(string s, StringBuilder sb)
        {
            var length = (s ?? string.Empty).Length;

            (sb = sb ?? new StringBuilder()).Append('"');

            var ch = '\0';

            for (var index = 0; index < length; index++)
            {
                var last = ch;
                ch = s[index];

                switch (ch)
                {
                    case '\\':
                    case '"':
                    {
                        sb.Append('\\');
                        sb.Append(ch);
                        break;
                    }

                    case '/':
                    {
                        if (last == '<')
                            sb.Append('\\');
                        sb.Append(ch);
                        break;
                    }

                    case '\b': sb.Append("\\b"); break;
                    case '\t': sb.Append("\\t"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\r': sb.Append("\\r"); break;

                    default:
                    {
                        if (ch < ' ')
                        {
                            sb.Append("\\u");
                            sb.Append(((int)ch).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            sb.Append(ch);
                        }

                        break;
                    }
                }
            }

            sb.Append('"');
            return sb;
        }
    }
}
