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

        [Theory, MemberData(nameof(GetTestData), "adoption01.dat")]
        public void Adoption01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "adoption02.dat")]
        public void Adoption02(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "comments01.dat")]
        public void Comments01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "doctype01.dat")]
        public void Doctype01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "domjs-unsafe.dat")]
        public void DomjsUnsafe(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "entities01.dat")]
        public void Entities01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "entities02.dat")]
        public void Entities02(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "foreign-fragment.dat")]
        public void ForeignFragment(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "html5test-com.dat")]
        public void Html5TestCom(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "inbody01.dat")]
        public void Inbody01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "isindex.dat")]
        public void Isindex(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "main-element.dat")]
        public void MainElement(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "math.dat")]
        public void Math(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "menuitem-element.dat")]
        public void MenuitemElement(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "namespace-sensitivity.dat")]
        public void NamespaceSensitivity(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "noscript01.dat")]
        public void Noscript01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "pending-spec-changes-plain-text-unsafe.dat")]
        public void PendingSpecChangesPlainTextUnsafe(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "pending-spec-changes.dat")]
        public void PendingSpecChanges(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "plain-text-unsafe.dat")]
        public void PlainTextUnsafe(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "ruby.dat")]
        public void Ruby(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "scriptdata01.dat")]
        public void Scriptdata01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tables01.dat")]
        public void Tables01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "template.dat")]
        public void Template(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests1.dat")]
        public void Tests1(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests10.dat")]
        public void Tests10(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests11.dat")]
        public void Tests11(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests12.dat")]
        public void Tests12(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests14.dat")]
        public void Tests14(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests15.dat")]
        public void Tests15(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests16.dat")]
        public void Tests16(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests17.dat")]
        public void Tests17(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests18.dat")]
        public void Tests18(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests19.dat")]
        public void Tests19(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests2.dat")]
        public void Tests2(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests20.dat")]
        public void Tests20(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests21.dat")]
        public void Tests21(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests22.dat")]
        public void Tests22(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests23.dat")]
        public void Tests23(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests24.dat")]
        public void Tests24(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests25.dat")]
        public void Tests25(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests26.dat")]
        public void Tests26(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests3.dat")]
        public void Tests3(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests4.dat")]
        public void Tests4(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests5.dat")]
        public void Tests5(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests6.dat")]
        public void Tests6(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests7.dat")]
        public void Tests7(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests8.dat")]
        public void Tests8(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests9.dat")]
        public void Tests9(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tests_innerHTML_1.dat")]
        public void TestsInnerHtml1(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "tricky01.dat")]
        public void Tricky01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "webkit01.dat")]
        public void Webkit01(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "webkit02.dat")]
        public void Webkit02(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory, MemberData(nameof(GetTestData), "gh40_form_in_template.dat")]
        public void Gh40FormInTemplate(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        [Theory(Skip = "Scripting not available"), MemberData(nameof(GetTestData), "document_write.dat")]
        public void DocumentWrite(int line, string html, string documentFragment, string[] document) =>
            Dat(line, html, documentFragment, document);

        public void Dat(int      line,
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
                        foreach (var a in from a in e.Attributes
                                          let prefix = !string.IsNullOrEmpty(a.prefix) ? a.prefix + " " : null
                                          select new
                                          {
                                              name = prefix + a.name,
                                              a.value
                                          }
                                          into a
                                          orderby a.name
                                          select a)
                        {
                            yield return Print(level + 1, a.name, "=", "\"", a.value, "\"");
                        }
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

        public static IEnumerable<object[]> GetTestData(string dat)
        {
            var assembly = MethodBase.GetCurrentMethod().DeclaringType.Assembly;

            return
                from name in assembly.GetManifestResourceNames()
                let tokens = name.Split('.').SkipWhile(e => e != "data").Skip(1).ToArray()
                where tokens.Length > 1
                   && tokens.First().StartsWith("tree_construction", StringComparison.OrdinalIgnoreCase)
                   && name.EndsWith("." + dat, StringComparison.OrdinalIgnoreCase)
                from test in
                    ParseTestData(
                        ReadTextResourceLines(name),
                        (line, data, isScriptOn, errors, documentFragment, document) => new object[]
                        {
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
