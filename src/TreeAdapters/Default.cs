using System;
using System.Collections.Generic;
using System.Text;

namespace ParseFive.TreeAdapters
{
    using Extensions;

    static class StockTreeAdapters
    {
        public static TreeAdapter defaultTreeAdapter = null;
    }

    class DefaultTreeAdapter : TreeAdapter
    {
        public object options { get; set; }
        public List<Attr> getAttrList(Node o)
        {
            throw new NotImplementedException();
        }

        public string getTagName(Node e)
        {
            throw new NotImplementedException();
        }

        public string getNamespaceURI(Node o)
        {
            throw new NotImplementedException();
        }

        public Document createDocument()
        {
            throw new NotImplementedException();
        }

        public Element createElement(string tEMPLATE, string hTML, List<Attr> p)
        {
            throw new NotImplementedException();
        }

        public Element getFirstChild(Element documentMock)
        {
            throw new NotImplementedException();
        }

        public DocumentFragment createDocumentFragment()
        {
            throw new NotImplementedException();
        }

        public void setDocumentType(object document, string name, string publicId, string systemId)
        {
            throw new NotImplementedException();
        }

        public void insertText(object parent, string chars)
        {
            throw new NotImplementedException();
        }

        public void insertTextBefore(object parent, object chars, Element beforeElement)
        {
            throw new NotImplementedException();
        }

        public void appendChild(Node parent, Node element)
        {
            throw new NotImplementedException();
        }

        public void insertBefore(object parent, Element element, Element beforeElement)
        {
            throw new NotImplementedException();
        }

        public Node getParentNode(Node openElement)
        {
            throw new NotImplementedException();
        }

        public Element getTemplateContent(Element openElement)
        {
            throw new NotImplementedException();
        }

        public Comment createCommentNode(string data)
        {
            throw new NotImplementedException();
        }

        public void detachNode(object child)
        {
            throw new NotImplementedException();
        }

        public void setTemplateContent(object tmpl, object content)
        {
            throw new NotImplementedException();
        }

        public void adoptAttributes(object p, List<Attr> attrs)
        {
            throw new NotImplementedException();
        }

        public void setDocumentMode(Node document, string mode)
        {
            throw new NotImplementedException();
        }

        public string getDocumentMode(Node document)
        {
            throw new NotImplementedException();
        }
    }
}
