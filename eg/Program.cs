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
            else
            {
                using (var http = new HttpClient())
                    html = http.GetStringAsync(source).GetAwaiter().GetResult();
            }
            var parser = new Parser();
            var doc = parser.parse(html);
            string indent = string.Empty;
            Dump(doc, Console.Out);
            Console.WriteLine();

            string Indented(int level, params string[] strings)
            {
                if (level == 0)
                    return string.Concat(strings);
                var width = level * 2;
                var indentation =
                    indent.Length < width
                    ? indent = "|" + new string(' ', width - 1)
                    : indent.Substring(0, width);
                return indentation + string.Concat(strings);
            }

            void Dump(Node node, TextWriter output, int level = 0)
            {
                switch (node)
                {
                    case Document d:
                    {
                        output.WriteLine(Indented(level, "#document"));
                        foreach (var child in d.ChildNodes)
                            Dump(child, output, level + 1);
                        break;
                    }
                    case Element e:
                    {
                        output.WriteLine(Indented(level, "<", e.TagName, ">"));
                        foreach (var a in e.Attributes)
                            output.WriteLine(Indented(level + 1, a.name, "=", Jsonify(a.value)));
                        foreach (var child in e.ChildNodes)
                            Dump(child, output, level + 1);
                        break;
                    }
                    case Text t: output.WriteLine(Indented(level, Jsonify(t.Value))); break;
                    case Comment c: output.WriteLine(Indented(level, "#comment", Jsonify(c.Data))); break;
                }
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
