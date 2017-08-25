using System;
using ParseFive.Extensions;
using System.Text;
using NeAttrsMap = System.Collections.Generic.Dictionary<string, int>;
using ParseFive.Tokenizer;
//using Attrs = ParseFive.Extensions.List<Attr>;


public abstract class IEntry
{
    public string type { get; set; }
    public string element { get; set; }
}

public class MarkerEntry : IEntry
{
    public MarkerEntry(string type)
    {
        this.type = type;
    }
}

public class ElementEntry : IEntry
{
    public Token token { get; set; }

    public ElementEntry(string type, string element, Token token)
    {
        this.type = type;
        this.element = element;
        this.token = token;
    }
}

public interface TreeAdapter
{
    object options { get; set; }
    List<Attr> getAttrList(object o);
    string getTagName(object o);
    string getNamespaceURI(object o);
    Document createDocument();
    Document createElement(string tEMPLATE, string hTML, List<Attr> p);
    object getFirstChild(object documentMock);
    object createDocumentFragment();
    void setDocumentType(object document, string name, string publicId, string systemId);
    void insertText(object parent, string chars);
    void insertTextBefore(object parent, object chars, object beforeElement);
    void appendChild(object parent, object element);
    void insertBefore(object parent, object element, object beforeElement);
    object getParentNode(object openElement);
    object getTemplateContent(object openElement);
    object createCommentNode(string data);
    void detachNode(object child);
    void setTemplateContent(object tmpl, object content);
    void adoptAttributes(object p, List<Attr> attrs);
    void setDocumentMode(Document document, string mode);
}

public class Document
{

}
public class DocumentFragment
{

}

namespace ParseFive.Parser
{
    class FormattingElementList
    {
        const int NOAH_ARK_CAPACITY = 3;

        Int length;
        public List<IEntry> entries;
        public TreeAdapter treeAdapter;
        object bookmark;

        public const string MARKER_ENTRY = "MARKER_ENTRY";
        public const string ELEMENT_ENTRY = "ELEMENT_ENTRY";

        public FormattingElementList(TreeAdapter treeAdapter)
        {
            length = 0;
            entries = new List<IEntry>();
        }

        private List<object> getNoahArkConditionCandidates(object newElement)
        {
            var candidates = new List<Object>();

            if (length >= NOAH_ARK_CAPACITY)
            {
                var neAttrsLength = this.treeAdapter.getAttrList(newElement).length;
                var neTagName = this.treeAdapter.getTagName(newElement);
                var neNamespaceURI = this.treeAdapter.getNamespaceURI(newElement);

                for (var i = this.length - 1; i >= 0; i--)
                {
                    var entry = this.entries[i];

                    if (entry.type == MARKER_ENTRY)
                        break;

                    var element = entry.element;
                    var elementAttrs = this.treeAdapter.getAttrList(element);
                    var isCandidate = this.treeAdapter.getTagName(element) == neTagName &&
                                      this.treeAdapter.getNamespaceURI(element) == neNamespaceURI &&
                                      elementAttrs.length == neAttrsLength;

                    if (isCandidate)
                        candidates.push(new { idx = i, attrs = elementAttrs });
                }
            }

            return candidates.Count < NOAH_ARK_CAPACITY ? new List<object>() : candidates;
        }

        private void ensureNoahArkCondition(object newElement)
        {
            var candidates = this.getNoahArkConditionCandidates(newElement);
            var cLength = candidates.length;

            if (cLength)
            {
                var neAttrs = this.treeAdapter.getAttrList(newElement);
                var neAttrsLength = neAttrs.length;
                var neAttrsMap = new NeAttrsMap();

                for (var i = 0; i < neAttrsLength; i++)
                {
                    var neAttr = neAttrs[i];

                    neAttrsMap.Add(neAttr.name, neAttr.value);
                    //neAttrsMap[neAttr.name] = neAttr.value;
                }

                for (var i = 0; i < neAttrsLength; i++)
                {
                    for (var j = 0; j < cLength; j++)
                    {
                        var cAttr = candidates[j].attrs[i];

                        if (neAttrsMap[cAttr.name] != cAttr.value)
                        {
                            candidates.splice(j, 1);
                            cLength--;
                        }

                        if (candidates.length < NOAH_ARK_CAPACITY)
                            return;
                    }
                }

                //NOTE: remove bottommost candidates until Noah's Ark condition will not be met
                for (var i = cLength - 1; i >= NOAH_ARK_CAPACITY - 1; i--)
                {
                    this.entries.splice(candidates[i].idx, 1);
                    this.length--;
                }
            }
        }

        public void insertMarker()
        {
            entries.push(new MarkerEntry(MARKER_ENTRY));
            length++;
        }

        public void pushElement(object element, Token token)
        {
            this.ensureNoahArkCondition(element);

            this.entries.push(new ElementEntry(ELEMENT_ENTRY, element, token));
            length++;
        }

        public void removeEntry(IEntry entry)
        {
            for (var i = this.length - 1; i >= 0; i--)
            {
                if (this.entries[i] == entry)
                {
                    this.entries.splice(i, 1);
                    this.length--;
                    break;
                }
            }
        }

        public void clearToLastMarker()
        {
            while (this.length)
            {
                var entry = this.entries.pop();

                this.length--;

                if (entry.type == FormattingElementList.MARKER_ENTRY)
                    break;
            }
        }

        //Search
        public IEntry getElementEntryInScopeWithTagName(string tagName)
        {
            for (var i = this.length - 1; i >= 0; i--)
            {
                var entry = this.entries[i];

                if (entry.type == FormattingElementList.MARKER_ENTRY)
                    return null;

                if (this.treeAdapter.getTagName(entry.element) == tagName)
                    return entry;
            }

            return null;
        }

        public IEntry getElementEntry(element)
        {
            for (var i = this.length - 1; i >= 0; i--)
            {
                var entry = this.entries[i];

                if (entry.type == ELEMENT_ENTRY && entry.element == element)
                    return entry;
            }

            return null;
        }
    }
}
