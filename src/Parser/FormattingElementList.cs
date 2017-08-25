using System;
using ParseFive.Extensions;
using System.Text;
using NeAttrsMap = System.Collections.Generic.Dictionary<string, int>;


interface IEntry
{
    string type { get; set; }
    string element { get; set; }
}

public class MarkerEntry : IEntry
{
    public string type { get; set; }

    MarkerEntry(string type)
    {
        this.type = type;
    }
}

public class ElementEntry : IEntry
{
    public string type { get; set; }
    public string element { get; set; }
    public Token token { get; set; }
    ElementEntry(string type, string element, Token token)
    {
        this.type = type;
        this.element = element;
        this.token = token;
    }
}

public interface TreeAdapter
{
     List<Attr> getAttrList();
     string getTagName();
     string getNamespaceURI();

}

namespace ParseFive.Parser
{
    class FormattingElementList
    {
        const int NOAH_ARK_CAPACITY = 3;

        int length;
        List<IEntry> entries;
        TreeAdapter treeAdapter;
        object bookmark;

        const string MARKER_ENTRY = "MARKER_ENTRY";
        const string ELEMENT_ENTRY = "ELEMENT_ENTRY";

        FormattingElementList(TreeAdapter treeAdapter)
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

        void insertMarker()
        {
            entries.push(new MarkerEntry(MARKER_ENTRY));
            length++;
        }

        void pushElement(object element, Token token)
        {
            this.ensureNoahArkCondition(element);

            this.entries.push(new ElementEntry(ELEMENT_ENTRY, element, token));
            length++;
        }

        void removeEntry(IEntry entry)
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

        void clearToLastMarker()
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
        IEntry getElementEntryInScopeWithTagName(string tagName)
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

        IEntry getElementEntry(element)
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
