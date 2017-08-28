namespace ParseFive.Parser
{
    using System;
    using Extensions;

    class OpenElementStack
    {
        TreeAdapter treeAdapter;
        public Element currentTmplContent { get; }
        public Node current { get; }
        public List<Element> items;
        internal readonly string currentTagName;
        internal readonly int tmplCount;

        public int stackTop => items.length;

        public OpenElementStack(Node document, TreeAdapter treeAdapter)
        {
            this.current = document;
            this.treeAdapter = treeAdapter;
            items = new List<Element>();
        }

        public void push(Element e)
        {
            Extensions.push(items, e);
        }

        public Element pop()
        {
            return Extensions.pop<Element>(items);
        }

        internal void replace(Element element, Element newElement)
        {
            throw new NotImplementedException();
        }

        internal void popUntilElementPopped(Element element)
        {
            throw new NotImplementedException();
        }

        internal void generateImpliedEndTags()
        {
            throw new NotImplementedException();
        }

        internal void popUntilTableCellPopped()
        {
            throw new NotImplementedException();
        }

        internal void generateImpliedEndTagsWithExclusion(string p)
        {
            throw new NotImplementedException();
        }

        internal void popUntilTagNamePopped(string p)
        {
            throw new NotImplementedException();
        }

        internal Element getCommonAncestor(Element element)
        {
            throw new NotImplementedException();
        }

        internal void remove(Element element)
        {
            throw new NotImplementedException();
        }

        internal void clearBackToTableRowContext()
        {
            throw new NotImplementedException();
        }

        internal void clearBackToTableBodyContext()
        {
            throw new NotImplementedException();
        }

        internal bool hasTableBodyContextInTableScope()
        {
            throw new NotImplementedException();
        }

        internal bool hasInTableScope(string tn)
        {
            throw new NotImplementedException();
        }

        internal bool hasInSelectScope(string sELECT)
        {
            throw new NotImplementedException();
        }

        internal bool isRootHtmlElementCurrent()
        {
            throw new NotImplementedException();
        }

        internal object tryPeekProperlyNestedBodyElement()
        {
            throw new NotImplementedException();
        }

        internal void popAllUpToHtmlElement()
        {
            throw new NotImplementedException();
        }

        internal bool hasInButtonScope(string p)
        {
            throw new NotImplementedException();
        }

        internal bool hasInScope(string fORM)
        {
            throw new NotImplementedException();
        }

        internal bool hasNumberedHeaderInScope()
        {
            throw new NotImplementedException();
        }

        internal void popUntilNumberedHeaderPopped()
        {
            throw new NotImplementedException();
        }

        internal bool hasInListItemScope(string lI)
        {
            throw new NotImplementedException();
        }

        internal void clearBackToTableContext()
        {
            throw new NotImplementedException();
        }

        public bool contains(Element entryElement)
        {
            throw new NotImplementedException();
        }

        public void insertAfter(Element furthestBlock, Element newElement)
        {
            throw new NotImplementedException();
        }
    }
}