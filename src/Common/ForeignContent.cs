using System;
using System.Collections.Generic;
using ParseFive.Extensions;
using Attrs = ParseFive.Extensions.List<Attr>;
//Aliases
using ɑ = HTML.TAG_NAMES;
using NS = HTML.NAMESPACES;
using ATTRS = HTML.ATTRS;

public class Token
{
    public Attrs attrs = new Attrs();
    public string tagName;
    public string type;

    public Token(string type)
    {
        this.type = type;
    }
}
public class Attr
{
    public string prefix;
    public string name;
    public string @namespace;
    public string value;

    public Attr(string name, string value)
    {
        this.prefix = ""; //TODO check
        this.name = name;
        this.@namespace = "";
        this.value = value;
    }
}

namespace ParseFive.Common
{
    using Extensions;

    class ForeignContent
    {
        //var Tokenizer = require("../tokenizer"),
        //var HTML = require("./html");

        //MIME types
        static class MIME_TYPES
        {
            public const string TEXT_HTML = "text/html";
            public const string APPLICATION_XML = "application/xhtml+xml";
        }

        //Attributes
        public const string DEFINITION_URL_ATTR = "definitionurl";
        public const string ADJUSTED_DEFINITION_URL_ATTR = "definitionURL";
        public IDictionary<string, string> SVG_ATTRS_ADJUSTMENT_MAP = new Dictionary<string, string>
        {
            {"attributename", "attributeName"},
            {"attributetype", "attributeType"},
            {"basefrequency", "baseFrequency"},
            {"baseprofile", "baseProfile"},
            {"calcmode", "calcMode"},
            {"clippathunits", "clipPathUnits"},
            {"diffuseconstant", "diffuseConstant"},
            {"edgemode", "edgeMode"},
            {"filterunits", "filterUnits"},
            {"glyphref", "glyphRef"},
            {"gradienttransform", "gradientTransform"},
            {"gradientunits", "gradientUnits"},
            {"kernelmatrix", "kernelMatrix"},
            {"kernelunitlength", "kernelUnitLength"},
            {"keypoints", "keyPoints"},
            {"keysplines", "keySplines"},
            {"keytimes", "keyTimes"},
            {"lengthadjust", "lengthAdjust"},
            {"limitingconeangle", "limitingConeAngle"},
            {"markerheight", "markerHeight"},
            {"markerunits", "markerUnits"},
            {"markerwidth", "markerWidth"},
            {"maskcontentunits", "maskContentUnits"},
            {"maskunits", "maskUnits"},
            {"numoctaves", "numOctaves"},
            {"pathlength", "pathLength"},
            {"patterncontentunits", "patternContentUnits"},
            {"patterntransform", "patternTransform"},
            {"patternunits", "patternUnits"},
            {"pointsatx", "pointsAtX"},
            {"pointsaty", "pointsAtY"},
            {"pointsatz", "pointsAtZ"},
            {"preservealpha", "preserveAlpha"},
            {"preserveaspectratio", "preserveAspectRatio"},
            {"primitiveunits", "primitiveUnits"},
            {"refx", "refX"},
            {"refy", "refY"},
            {"repeatcount", "repeatCount"},
            {"repeatdur", "repeatDur"},
            {"requiredextensions", "requiredExtensions"},
            {"requiredfeatures", "requiredFeatures"},
            {"specularconstant", "specularConstant"},
            {"specularexponent", "specularExponent"},
            {"spreadmethod", "spreadMethod"},
            {"startoffset", "startOffset"},
            {"stddeviation", "stdDeviation"},
            {"stitchtiles", "stitchTiles"},
            {"surfacescale", "surfaceScale"},
            {"systemlanguage", "systemLanguage"},
            {"tablevalues", "tableValues"},
            {"targetx", "targetX"},
            {"targety", "targetY"},
            {"textlength", "textLength"},
            {"viewbox", "viewBox"},
            {"viewtarget", "viewTarget"},
            {"xchannelselector", "xChannelSelector"},
            {"ychannelselector", "yChannelSelector"},
            {"zoomandpan", "zoomAndPan" },
        };

        public IDictionary<string, XmlAdjustment> XML_ATTRS_ADJUSTMENT_MAP = new Dictionary<string, XmlAdjustment>
        {
            ["xlink:actuate"] = new XmlAdjustment("xlink", "actuate", NS.XLINK),
            ["xlink:arcrole"] = new XmlAdjustment("xlink", "arcrole", NS.XLINK),
            ["xlink:href"] = new XmlAdjustment("xlink", "href", NS.XLINK),
            ["xlink:role"] = new XmlAdjustment("xlink", "role", NS.XLINK),
            ["xlink:show"] = new XmlAdjustment("xlink", "show", NS.XLINK),
            ["xlink:title"] = new XmlAdjustment("xlink", "title", NS.XLINK),
            ["xlink:type"] = new XmlAdjustment("xlink", "type", NS.XLINK),
            ["xml:base"] = new XmlAdjustment("xml", "base", NS.XML),
            ["xml:lang"] = new XmlAdjustment("xml", "lang", NS.XML),
            ["xml:space"] = new XmlAdjustment("xml", "space", NS.XML),
            ["xmlns"] = new XmlAdjustment("", "xmlns", NS.XMLNS),
            ["xmlns:xlink"] = new XmlAdjustment("xmlns", "xlink", NS.XMLNS)
        };



        //SVG tag names adjustment map
        public IDictionary<string, string> SVG_TAG_NAMES_ADJUSTMENT_MAP = new Dictionary<string, string>
        {
            ["altglyph"] = "altGlyph",
            ["altglyphdef"] = "altGlyphDef",
            ["altglyphitem"] = "altGlyphItem",
            ["animatecolor"] = "animateColor",
            ["animatemotion"] = "animateMotion",
            ["animatetransform"] = "animateTransform",
            ["clippath"] = "clipPath",
            ["feblend"] = "feBlend",
            ["fecolormatrix"] = "feColorMatrix",
            ["fecomponenttransfer"] = "feComponentTransfer",
            ["fecomposite"] = "feComposite",
            ["feconvolvematrix"] = "feConvolveMatrix",
            ["fediffuselighting"] = "feDiffuseLighting",
            ["fedisplacementmap"] = "feDisplacementMap",
            ["fedistantlight"] = "feDistantLight",
            ["feflood"] = "feFlood",
            ["fefunca"] = "feFuncA",
            ["fefuncb"] = "feFuncB",
            ["fefuncg"] = "feFuncG",
            ["fefuncr"] = "feFuncR",
            ["fegaussianblur"] = "feGaussianBlur",
            ["feimage"] = "feImage",
            ["femerge"] = "feMerge",
            ["femergenode"] = "feMergeNode",
            ["femorphology"] = "feMorphology",
            ["feoffset"] = "feOffset",
            ["fepointlight"] = "fePointLight",
            ["fespecularlighting"] = "feSpecularLighting",
            ["fespotlight"] = "feSpotLight",
            ["fetile"] = "feTile",
            ["feturbulence"] = "feTurbulence",
            ["foreignobject"] = "foreignObject",
            ["glyphref"] = "glyphRef",
            ["lineargradient"] = "linearGradient",
            ["radialgradient"] = "radialGradient",
            ["textpath"] = "textPath",
        };

        //Tags that causes exit from foreign content
        //public static IDictionary<string, IDictionary<string, bool>> SPECIAL_ELEMENTS = new Dictionary<string, IDictionary<string, bool>>
        public IDictionary<string, bool> EXITS_FOREIGN_CONTENT = new Dictionary<string, bool>
        {
            [ɑ.B] = true,
            [ɑ.BIG] = true,
            [ɑ.BLOCKQUOTE] = true,
            [ɑ.BODY] = true,
            [ɑ.BR] = true,
            [ɑ.CENTER] = true,
            [ɑ.CODE] = true,
            [ɑ.DD] = true,
            [ɑ.DIV] = true,
            [ɑ.DL] = true,
            [ɑ.DT] = true,
            [ɑ.EM] = true,
            [ɑ.EMBED] = true,
            [ɑ.H1] = true,
            [ɑ.H2] = true,
            [ɑ.H3] = true,
            [ɑ.H4] = true,
            [ɑ.H5] = true,
            [ɑ.H6] = true,
            [ɑ.HEAD] = true,
            [ɑ.HR] = true,
            [ɑ.I] = true,
            [ɑ.IMG] = true,
            [ɑ.LI] = true,
            [ɑ.LISTING] = true,
            [ɑ.MENU] = true,
            [ɑ.META] = true,
            [ɑ.NOBR] = true,
            [ɑ.OL] = true,
            [ɑ.P] = true,
            [ɑ.PRE] = true,
            [ɑ.RUBY] = true,
            [ɑ.S] = true,
            [ɑ.SMALL] = true,
            [ɑ.SPAN] = true,
            [ɑ.STRONG] = true,
            [ɑ.STRIKE] = true,
            [ɑ.SUB] = true,
            [ɑ.SUP] = true,
            [ɑ.TABLE] = true,
            [ɑ.TT] = true,
            [ɑ.U] = true,
            [ɑ.UL] = true,
            [ɑ.VAR] = true,
        };



        //Check exit from foreign content
        public bool causesExit(Token startTagToken)
        {
            var tn = startTagToken.tagName;
            var isFontWithAttrs = tn == ɑ.FONT && (Tokenizer.getTokenAttr(startTagToken, ATTRS.COLOR) != null ||
                                                    Tokenizer.getTokenAttr(startTagToken, ATTRS.SIZE) != null ||
                                                    Tokenizer.getTokenAttr(startTagToken, ATTRS.FACE) != null);

            return isFontWithAttrs ? true : EXITS_FOREIGN_CONTENT[tn];
        }

        //Token adjustments
        public void adjustTokenMathMLAttrs(Token token)
        {
            for (var i = 0; i < token.attrs.length; i++)
            {
                if (token.attrs[i].name == DEFINITION_URL_ATTR)
                {
                    token.attrs[i].name = ADJUSTED_DEFINITION_URL_ATTR;
                    break;
                }
            }
        }

        public void adjustTokenSVGAttrs(Token token)
        {
            for (var i = 0; i < token.attrs.length; i++)
            {
                var adjustedAttrName = SVG_ATTRS_ADJUSTMENT_MAP[token.attrs[i].name];

                if (adjustedAttrName)
                    token.attrs[i].name = adjustedAttrName;
            }
        }

        public void adjustTokenXMLAttrs(Token token)
        {
            for (var i = 0; i < token.attrs.length; i++)
            {
                var adjustedAttrEntry = XML_ATTRS_ADJUSTMENT_MAP[token.attrs[i].name];

                if (adjustedAttrEntry)
                {
                    token.attrs[i].prefix = adjustedAttrEntry.prefix;
                    token.attrs[i].name = adjustedAttrEntry.name;
                    token.attrs[i].@namespace = adjustedAttrEntry.@namespace;
                }
            }
        }


        public void adjustTokenSVGTagName(Token token)
        {
            var adjustedTagName = SVG_TAG_NAMES_ADJUSTMENT_MAP[token.tagName];

            if (adjustedTagName)
                token.tagName = adjustedTagName;
        }

        //Integration points
        public bool isMathMLTextIntegrationPoint(string tn, string ns)
        {
            return ns == NS.MATHML && (tn == ɑ.MI || tn == ɑ.MO || tn == ɑ.MN || tn == ɑ.MS || tn == ɑ.MTEXT);
        }

        public bool isHtmlIntegrationPoint(string tn, string ns, List<Attr> attrs)
        {
            if (ns == NS.MATHML && tn == ɑ.ANNOTATION_XML)
            {
                for (var i = 0; i < attrs.length; i++)
                {
                    if (attrs[i].name == ATTRS.ENCODING)
                    {
                        var value = attrs[i].value.toLowerCase();

                        return value == MIME_TYPES.TEXT_HTML || value == MIME_TYPES.APPLICATION_XML;
                    }
                }
            }

            return ns == NS.SVG && (tn == ɑ.FOREIGN_OBJECT || tn == ɑ.DESC || tn == ɑ.TITLE);
        }

        public bool isIntegrationPoint(string tn, string ns, List<Attr> attrs, string foreignNS)
        {
            if ((!foreignNS || foreignNS == NS.HTML) && isHtmlIntegrationPoint(tn, ns, attrs))
                return true;

            if ((!foreignNS || foreignNS == NS.MATHML) && isMathMLTextIntegrationPoint(tn, ns))
                return true;

            return false;
        }
    }
}
