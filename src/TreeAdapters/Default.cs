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
        public List<Attr> getAttrList(Node element)
        {
            throw new NotImplementedException();
        }

        public string getTagName(Node element)
        {
            throw new NotImplementedException();
        }

        public string getNamespaceURI(Node element)
        {
            throw new NotImplementedException();
        }

        public Document createDocument()
        {
            throw new NotImplementedException();
        }

        public Element createElement(string tagName, string namespaceURI, List<Attr> attrs)
        {
            throw new NotImplementedException();
        }

        public Element getFirstChild(Element node)
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

        public void insertTextBefore(object parentNode, object text, Element referenceNode)
        {
            throw new NotImplementedException();
        }

        public void appendChild(Node parentNode, Node newNode)
        {
            throw new NotImplementedException();
        }

        public void insertBefore(object parentNode, Element newNode, Element referenceNode)
        {
            throw new NotImplementedException();
        }

        public Node getParentNode(Node node)
        {
            throw new NotImplementedException();
        }

        public Element getTemplateContent(Element templateElement)
        {
            throw new NotImplementedException();
        }

        public Comment createCommentNode(string data)
        {
            throw new NotImplementedException();
        }

        public void detachNode(object node)
        {
            throw new NotImplementedException();
        }

        public void setTemplateContent(object templateElement, object contentElement)
        {
            throw new NotImplementedException();
        }

        public void adoptAttributes(object recipient, List<Attr> attrs)
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
