using System;
using Xunit;

namespace ParseFive.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using MoreLinq;
    using Parser;

    public class TreeConstructionTests
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Dat(string   source,
                        int      line,
                        string   html,
                        string   documentFragment,
                        string[] document)
        {
            var parser = new Parser();
            var doc = parser.parse(html);
            char[] indent = {};
            var actuals = Dump(doc);
            foreach (var t in document.ZipLongest(actuals, (exp, act) => new { Expected = exp, Actual = act }))
                Assert.Equal(t.Expected, t.Actual);

            string Print(int level, params string[] strings)
            {
                var sb = new StringBuilder();
                if (level > 0)
                {
                    var width = level * 2;
                    if (indent.Length < width)
                        indent = ("|" + new string(' ', width - 1)).ToCharArray();
                    sb.Append(indent, 0, width);
                }
                foreach (var s in strings)
                    sb.Append(s);
                return sb.ToString();
            }

            IEnumerable<string> Dump(Node node, int level = 0)
            {
                switch (node)
                {
                    case DocumentType dt:
                        yield return Print(
                            level,
                            "<!DOCTYPE ",
                            new StringBuilder()
                                .Append(dt.Name)
                                .Append(dt.PublicId != null || dt.SystemId != null ? " \"" + dt.PublicId + "\" \"" + dt.SystemId + "\"" : null)
                                .ToString(),
                            ">");
                        break;
                    case Element e:
                        var ns = e.NamespaceUri == "http://www.w3.org/2000/svg" ? "svg "
                               : e.NamespaceUri == "http://www.w3.org/1998/Math/MathML" ? "math "
                               : null;
                        yield return Print(level, "<", ns, e.TagName, ">");
                        foreach (var a in e.Attributes)
                            yield return Print(level + 1, a.name, "=", "\"" + a.value + "\"");
                        break;
                    case Text t:
                        yield return Print(level, "\"" + t.Value + "\"");
                        break;
                    case Comment c:
                        yield return Print(level, $"<!-- {c.Data} -->");
                        break;
                }

                foreach (var dump in from child in node.ChildNodes
                                     from dump in Dump(child, level + 1)
                                     select dump)
                {
                    yield return dump;
                }
            }
        }

        public static IEnumerable<object[]> GetTestData()
        {
            var assembly = MethodBase.GetCurrentMethod().DeclaringType.Assembly;

            return
                from name in assembly.GetManifestResourceNames()
                let tokens = name.Split('.').SkipWhile(e => e != "data").Skip(1).ToArray()
                where tokens.Length > 1
                    && tokens.First().StartsWith("tree_construction", StringComparison.OrdinalIgnoreCase)
                from test in
                    ParseTestData(
                        ReadTextResourceLines(name),
                        (line, data, isScriptOn, errors, documentFragment, document) => new object[]
                        {
                            string.Join(".", tokens),
                            line, data, documentFragment, document.ToArray()
                        })
                select test;

            IEnumerable<string> ReadTextResourceLines(string rn)
            {
                using (var stream = assembly.GetManifestResourceStream(rn))
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        yield return line;
                }
            }
        }

        static IEnumerable<T>
            ParseTestData<T>(
                IEnumerable<string> lines,
                Func<int                , // Line,
                     string             , // Data,
                     bool               , // IsScriptOn,
                     IEnumerable<string>, // Errors,
                     string             , // DocumentFragment,
                     IEnumerable<string>, // Document>
                     T> resultSelector)
        {
            var nonBlankLines =
                from e in lines.Select((s, i) => (Nr: i + 1, Line: s))
                where !string.IsNullOrWhiteSpace(e.Line)
                select (Nr: e.Nr, Line: e.Line.TrimEnd()) into e
                where e.Line.Length > 0
                select e;

            int start = 0;
            string data = null;
            IEnumerable<string> errors = null;
            string documentFragment = null;
            IEnumerable<string> document = null;
            var isScriptOn = false;

            string ReadLine(IEnumerator<(int, string Line)> e) =>
                e.MoveNext() ? e.Current.Line : throw new FormatException();

            List<string> ReadLines(IEnumerator<(int, string)> e, ref int nr, ref string line)
            {
                var list = new List<string>();
                while (e.MoveNext())
                {
                    (nr, line) = e.Current;
                    if (line[0] == '#')
                        break;
                    list.Add(line);
                    nr = 0; line = null;
                }
                return list;
            }

            using (var e = nonBlankLines.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    var (lnr, line) = e.Current;

                    do
                    {
                        if (line == null)
                            break;

                        switch (line)
                        {
                            case "#data":
                                if (data != null)
                                    yield return resultSelector(start, data, isScriptOn, errors, documentFragment, document);
                                start = lnr;
                                data = string.Join("\n", ReadLines(e, ref lnr, ref line));
                                continue;
                            case "#errors": errors = ReadLines(e, ref lnr, ref line); continue;
                            case "#document-fragment": documentFragment = ReadLine(e); break;
                            case "#document": document = ReadLines(e, ref lnr, ref line); continue;
                            case "#script-on": isScriptOn = true; break;
                            case "#script-off": isScriptOn = false; break;
                            default: throw new FormatException($"Error parsing line #{lnr}: {line}");
                        }

                        if (!e.MoveNext())
                            yield break;

                        (lnr, line) = e.Current;
                    }
                    while (true);
                }
            }

            if (data != null)
                yield return resultSelector(start, data, isScriptOn, errors, documentFragment, document);
        }
    }
}
