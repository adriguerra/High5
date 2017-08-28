namespace ParseFive
{
    using Extensions;

    public interface TreeAdapter
    {
        List<Attr> getAttrList(Node element);
        string getTagName(Node element);
        string getNamespaceURI(Node element);
        Document createDocument();
        Element createElement(string tagName, string namespaceURI, List<Attr> attrs);
        Element getFirstChild(Element node);
        DocumentFragment createDocumentFragment();
        void setDocumentType(object document, string name, string publicId, string systemId);
        void insertText(object parent, string chars);
        void insertTextBefore(object parentNode, object text, Element referenceNode);
        void appendChild(Node parentNode, Node newNode);
        void insertBefore(object parentNode, Element newNode, Element referenceNode);
        Node getParentNode(Node node);
        Element getTemplateContent(Element templateElement);
        Comment createCommentNode(string data);
        void detachNode(object node);
        void setTemplateContent(object templateElement, object contentElement);
        void adoptAttributes(object recipient, List<Attr> attrs);
        void setDocumentMode(Node document, string mode);
        string getDocumentMode(Node document);
    }
}