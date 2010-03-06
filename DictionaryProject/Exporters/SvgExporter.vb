Imports System.Xml

Public Class SvgExporter
    Inherits ExporterBase

    Public Overrides Sub Export(ByVal words As System.Collections.Generic.List(Of Context), ByVal filename As String)
        Dim xd As New XmlDocument()
        xd.PreserveWhitespace = True
        Dim svg As XmlElement = xd.CreateElement("svg")

        xd.AppendChild(svg)
        svg.SetAttribute("xmlns:dc", "http://purl.org/dc/elements/1.1/")
        svg.SetAttribute("xmlns:cc", "http://creativecommons.org/ns#")
        svg.SetAttribute("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#")
        svg.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg")
        svg.SetAttribute("xmlns", "http://www.w3.org/2000/svg")
        svg.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink")
        svg.SetAttribute("xmlns:sodipodi", "http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd")
        svg.SetAttribute("xmlns:inkscape", "http://www.inkscape.org/namespaces/inkscape")
        svg.SetAttribute("width", "744.09448819")
        svg.SetAttribute("height", "1052.3622047")
        svg.SetAttribute("id", "svg2")
        svg.SetAttribute("version", "http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd", "0.32")
        svg.SetAttribute("version", "http://www.inkscape.org/namespaces/inkscape", "0.46")
        svg.SetAttribute("docname", "names-test.svg")
        svg.SetAttribute("output_extension", "http://www.inkscape.org/namespaces/inkscape", "org.inkscape.output.svg.inkscape")

        Dim defs As XmlElement = xd.CreateElement("defs")
        svg.AppendChild(defs)
        defs.SetAttribute("id", "defs4")

        Dim perspective As XmlElement = xd.CreateElement("inkscape:perspective", "http://www.inkscape.org/namespaces/inkscape")
        defs.AppendChild(perspective)
        perspective.SetAttribute("type", "http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd", "inkscape:persp3d")
        perspective.SetAttribute("vp_x", "http://www.inkscape.org/namespaces/inkscape", "0 : 526.18109 : 1")
        perspective.SetAttribute("vp_y", "http://www.inkscape.org/namespaces/inkscape", "0 : 1000 : 0")
        perspective.SetAttribute("vp_z", "http://www.inkscape.org/namespaces/inkscape", "744.09448 : 526.18109 : 1")
        perspective.SetAttribute("persp3d-origin", "http://www.inkscape.org/namespaces/inkscape", "372.04724 : 350.78739 : 1")
        perspective.SetAttribute("id", "perspective10")

        Dim g As XmlElement = xd.CreateElement("g")
        svg.AppendChild(g)
        g.SetAttribute("label", "http://www.inkscape.org/namespaces/inkscape", "Layer 1")
        g.SetAttribute("groupmode", "http://www.inkscape.org/namespaces/inkscape", "layer")
        g.SetAttribute("id", "layer1")

        Dim i As Integer = 8000
        For Each ctx As Context In words
            Dim x As Double = 2 + Math.Floor((i - 8000) / 70) * 300
            Dim y As Double = 2 + ((i - 8000) Mod 70) * 14

            Dim text As XmlElement = xd.CreateElement("text")
            g.AppendChild(text)
            text.SetAttribute("xml:space", "preserve")
            text.SetAttribute("style", "font-size:12px;font-style:normal;font-weight:normal;fill:#000000;fill-opacity:1;stroke:none;stroke-width:1px;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1;font-family:Bitstream Vera Sans")
            text.SetAttribute("id", String.Format("text{0}", i))

            Dim textpath As XmlElement = xd.CreateElement("textPath")
            text.AppendChild(textpath)
            textpath.SetAttribute("href", "http://www.w3.org/1999/xlink", String.Format("#path{0}", i))
            textpath.SetAttribute("id", String.Format("textPath{0}", i))

            Dim tspan As XmlElement = xd.CreateElement("tspan")
            textpath.AppendChild(tspan)
            tspan.SetAttribute("id", String.Format("tspan{0}", i))

            Dim tspan2 As XmlElement = xd.CreateElement("tspan")
            tspan.AppendChild(tspan2)
            tspan2.SetAttribute("id", String.Format("tspani{0}", i))
            tspan2.SetAttribute("x", x.ToString())
            tspan2.SetAttribute("y", y.ToString())
            tspan2.InnerText = ctx.ToString()

            Dim path As XmlElement = xd.CreateElement("path")
            g.AppendChild(path)
            path.SetAttribute("style", "fill:none;fill-rule:evenodd;stroke:#000000;stroke-width:1px;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1")
            path.SetAttribute("d", String.Format("M {0},{1} C {0},{1} {2},{3} {4},{5}", x, y, x + 150, y - 12, x + 300, y))
            path.SetAttribute("id", String.Format("path{0}", i))

            i += 1
        Next

        xd.Save(filename)
    End Sub

    Public Overrides ReadOnly Property Type() As String
        Get
            Return "Svg image|*.svg"
        End Get
    End Property
End Class
