namespace ParseFive
{
    using Extensions;

    public interface TreeAdapter
    {
        List<Attr> getAttrList(Node o);
        string getTagName(Node e);
        string getNamespaceURI(Node o);
        Document createDocument();
        Element createElement(string tEMPLATE, string hTML, List<Attr> p);
        Element getFirstChild(Element documentMock);
        DocumentFragment createDocumentFragment();
        void setDocumentType(object document, string name, string publicId, string systemId);
        void insertText(object parent, string chars);
        void insertTextBefore(object parent, object chars, Element beforeElement);
        void appendChild(Node parent, Node element);
        void insertBefore(object parent, Element element, Element beforeElement);
        Node getParentNode(Node openElement);
        Element getTemplateContent(Element openElement);
        Comment createCommentNode(string data);
        void detachNode(object child);
        void setTemplateContent(object tmpl, object content);
        void adoptAttributes(object p, List<Attr> attrs);
        void setDocumentMode(Node document, string mode);
        string getDocumentMode(Node document);
    }
}