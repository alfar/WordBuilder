Imports System.Xml
Imports System.IO
Imports Ionic.Zip


Public Class DrawPlusExporter
    Inherits ExporterBase

    Public Overrides Sub Export(ByVal words As System.Collections.Generic.List(Of Context), ByVal filename As String)
        Dim tmpDir As String = IO.Path.GetTempFileName()
        File.Delete(tmpDir)
        Directory.CreateDirectory(tmpDir)

        File.WriteAllText(Path.Combine(tmpDir, "Root.xml"), "<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0""><_item _value=""summary""/><_item _value=""printinfo""/><_item _value=""story""/><_item _value=""stylesheet""/><_item _value=""colours""/><_item _value=""document""/><_item _value=""Spreads""/></SerifXML>")
        File.WriteAllText(Path.Combine(tmpDir, "stylesheet.xml"), "<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0""><_object _class=""Story::CStyleSheet"" _id=""3"" defaultGlyphName=""Default Paragraph Font"" defaultParaName=""Normal"" placeholderName=""Placeholder""><styles><_item _class=""Story::CStyle"" followingName=""Default Paragraph Font"" name=""Default Paragraph Font"" nextLevelName=""Default Paragraph Font"" recommendedLevel=""10""><GlyphDelta _ref=""5""/></_item><_item _class=""Story::CStyle"" followingName=""Normal"" name=""Normal"" nextLevelName=""Normal"" recommendedLevel=""10""><GlyphDelta _ref=""7""/><ParaDelta _ref=""12""/></_item></styles><noteApps><_item _class=""Story::CNoteAtts"" bodyStyleName=""Footnote Text"" initialGap=""0"" interNoteGap=""0"" referenceStyleName=""Footnote Reference"" startAt=""1""/><_item _class=""Story::CNoteAtts"" bodyStyleName=""Endnote Text"" initialGap=""0"" interNoteGap=""0"" numberFormat=""RomanLower"" position=""StoryEnd"" referenceStyleName=""Endnote Reference"" restart=""Story"" startAt=""1"" type=""Endnote""/></noteApps><fontMap _class=""SCFontMap""><fonts/></fontMap><paraAttsDelta _ref=""16""/><glyphAttsDelta _ref=""17""/></_object><_object SpellStatus=""0"" _class=""Story::CGlyphAttsDelta"" _id=""5""/><_object AbsLetterSpace=""0"" AbsWidth=""0"" Advance=""0"" CharSet=""1"" ClipPrecision=""0"" Escapement=""0"" FontFace=""Times New Roman"" Height=""12000"" IsAllCaps=""0"" IsDoubleStrikeOut=""0"" IsEmboss=""0"" IsEngrave=""0"" IsHidden=""0"" IsItalic=""0"" IsLayAnchorDescription=""0"" IsLayDropCap=""0"" IsLayHyph=""0"" IsLayIndexDescription=""0"" IsLayList=""0"" IsLayoutOnly=""0"" IsLetterSpaceRelative=""0"" IsOutline=""0"" IsShadow=""0"" IsSmallCaps=""0"" IsStrikeOut=""0"" LanguageCode=""1030"" MinKerningHeight=""16000"" Orientation=""0"" OutPrecision=""4"" PitchAndFamily=""16"" Quality=""0"" RelLetterSpace=""0"" RelOffsetX=""0"" RelOffsetY=""0"" RelWidth=""1"" ResetGlyph=""1"" SpellStatus=""0"" Style=""Default Paragraph Font"" SuperSubType=""0"" UnderlineType=""0"" Weight=""400"" WidthType=""0"" _GlyphBool0=""0"" _GlyphBool1=""0"" _GlyphBool2=""0"" _GlyphBool3=""0"" _GlyphBool4=""0"" _GlyphBool5=""0"" _GlyphFloat0=""0"" _GlyphFloat1=""0"" _GlyphFloat2=""0"" _GlyphFloat3=""0"" _GlyphFloat4=""0"" _GlyphFloat5=""0"" _GlyphInt0=""0"" _GlyphInt1=""0"" _GlyphInt2=""0"" _GlyphInt3=""0"" _GlyphInt4=""0"" _GlyphInt5=""0"" _class=""Story::CGlyphAttsDelta"" _id=""7""><_item _first=""HyperLink""><_second _class=""Story::CHyperLinkAtt""/></_item><_item _first=""ForeColor""><_second _class=""CTextFillAtt""><Fill _class=""CFillSolid""><colour _class=""CRGBColour""/></Fill></_second></_item><_item _first=""BackColor""/><_item _first=""_GlyphAtt0""/><_item _first=""_GlyphAtt1""/><_item _first=""_GlyphAtt2""/><_item _first=""_GlyphAtt3""/><_item _first=""_GlyphAtt4""/><_item _first=""_GlyphAtt5""/></_object><_object AbsLeading=""0"" AlignType=""0"" DefaultTabStops=""36000"" DropCapIndent=""0"" DropCapLineCount=""3"" DropCapType=""0"" FirstIndent=""0"" InitialBaselineAtHeight=""1"" IsAutoHyphenation=""1"" IsClearOldFormatting=""1"" IsDropCapHangingIndent=""0"" IsKeepTogether=""0"" IsKeepWithNext=""0"" IsPageBreakBefore=""0"" IsRestartList=""0"" IsWidowControl=""1"" LeadingIncludesAdvances=""1"" LeadingType=""0"" LeftIndent=""0"" MaxConsecutiveHyphens=""2"" MinAutoHyphenPrefixLength=""2"" MinAutoHyphenSuffixLength=""2"" MinAutoHyphenWordLength=""4"" RelHyphenationZone=""6"" RelLeading=""1"" RelMaxSpace=""1.8"" RelMinSpace=""0.8"" RelMinSpaceLastLine=""0.6"" RelOptSpace=""1"" RightIndent=""0"" SpaceAfter=""0"" SpaceBefore=""0"" Style=""Normal"" _ParaBool0=""0"" _ParaBool1=""0"" _ParaBool2=""0"" _ParaBool3=""0"" _ParaBool4=""0"" _ParaBool5=""0"" _ParaFloat0=""0"" _ParaFloat1=""0"" _ParaFloat2=""0"" _ParaFloat3=""0"" _ParaFloat4=""0"" _ParaFloat5=""0"" _ParaInt0=""0"" _ParaInt1=""0"" _ParaInt2=""0"" _ParaInt3=""0"" _ParaInt4=""0"" _ParaInt5=""0"" _class=""Story::CParaAttsDelta"" _id=""12""><_item _first=""Lists""/><_item _first=""DropCap""/><_item _first=""unused1""/><_item _first=""LineAbove""/><_item _first=""LineBelow""/><_item _first=""_ParaAtt0""/><_item _first=""_ParaAtt1""/><_item _first=""_ParaAtt2""/><_item _first=""_ParaAtt3""/><_item _first=""_ParaAtt4""/><_item _first=""_ParaAtt5""/></_object><_object IsClearOldFormatting=""1"" Style=""Normal"" _class=""Story::CParaAttsDelta"" _id=""16""/><_object ResetGlyph=""1"" Style=""Default Paragraph Font"" _class=""Story::CGlyphAttsDelta"" _id=""17""/><_object AbsLeading=""0"" AlignType=""0"" DefaultTabStops=""36000"" DoesHeightUseNonMarkingGlyphs=""0"" DropCapIndent=""0"" DropCapLineCount=""3"" DropCapType=""0"" FirstIndent=""0"" IndentLevel=""0"" InitialBaselineAtHeight=""1"" IsAutoHyphenation=""1"" IsClearOldFormatting=""1"" IsDontSpellCheck=""0"" IsDropCapHangingIndent=""0"" IsKeepTogether=""0"" IsKeepWithNext=""0"" IsPageBreakBefore=""0"" IsRestartList=""0"" IsWidowControl=""1"" LeadingIncludesAdvances=""1"" LeadingType=""0"" LeftIndent=""0"" MaxConsecutiveHyphens=""2"" MinAutoHyphenPrefixLength=""2"" MinAutoHyphenSuffixLength=""2"" MinAutoHyphenWordLength=""4"" PlaceHoldersBreakType=""0"" RelHyphenationZone=""6"" RelLeading=""1"" RelMaxSpace=""1.8"" RelMinSpace=""0.8"" RelMinSpaceLastLine=""0.6"" RelOptSpace=""1"" RightIndent=""0"" SpaceAfter=""0"" SpaceBefore=""0"" Style=""Normal"" TargetPrecision=""1"" _ParaBool0=""0"" _ParaBool1=""0"" _ParaBool2=""0"" _ParaBool3=""0"" _ParaBool4=""0"" _ParaBool5=""0"" _ParaFloat0=""0"" _ParaFloat1=""0"" _ParaFloat2=""0"" _ParaFloat3=""0"" _ParaFloat4=""0"" _ParaFloat5=""0"" _ParaInt0=""0"" _ParaInt1=""0"" _ParaInt2=""0"" _ParaInt3=""0"" _ParaInt4=""0"" _ParaInt5=""0"" _class=""Story::CParaAttsDelta"" _id=""19""><_item _first=""Lists""/><_item _first=""DropCap""/><_item _first=""unused1""/><_item _first=""LineAbove""/><_item _first=""LineBelow""/><_item _first=""_ParaAtt0""/><_item _first=""_ParaAtt1""/><_item _first=""_ParaAtt2""/><_item _first=""_ParaAtt3""/><_item _first=""_ParaAtt4""/><_item _first=""_ParaAtt5""/></_object><_object AbsLetterSpace=""0"" AbsWidth=""0"" Advance=""0"" CharSet=""1"" ClipPrecision=""0"" Escapement=""0"" FontFace=""Times New Roman"" HasBackColor=""0"" Height=""12000"" IsAllCaps=""0"" IsDoubleStrikeOut=""0"" IsEmboss=""0"" IsEngrave=""0"" IsGlyphPlaceHolderForWeb=""0"" IsGlyphPlaceHolderHideContent=""0"" IsGlyphPlaceHolderShowWrapAnchor=""0"" IsHidden=""0"" IsItalic=""0"" IsLayAnchorDescription=""0"" IsLayDropCap=""0"" IsLayHyph=""0"" IsLayHyphHidden=""0"" IsLayIndexDescription=""0"" IsLayList=""0"" IsLayoutOnly=""0"" IsLetterSpaceRelative=""0"" IsOutline=""0"" IsShadow=""0"" IsSmallCaps=""0"" IsStrikeOut=""0"" IsTabsAsSpaces=""0"" LanguageCode=""1030"" MinKerningHeight=""16000"" NeverUnused1=""0"" OpticalJustify=""0"" Orientation=""0"" OutPrecision=""4"" PitchAndFamily=""16"" Quality=""0"" RelLetterSpace=""0"" RelOffsetX=""0"" RelOffsetY=""0"" RelWidth=""1"" ResetGlyph=""1"" ShearX=""0"" SpellStatus=""1"" Style=""Default Paragraph Font"" SuperSubType=""0"" TargetPrecision=""1"" UnderlineType=""0"" Weight=""400"" WidthType=""0"" _GlyphBool0=""0"" _GlyphBool1=""0"" _GlyphBool2=""0"" _GlyphBool3=""0"" _GlyphBool4=""0"" _GlyphBool5=""0"" _GlyphFloat0=""0"" _GlyphFloat1=""0"" _GlyphFloat2=""0"" _GlyphFloat3=""0"" _GlyphFloat4=""0"" _GlyphFloat5=""0"" _GlyphInt0=""0"" _GlyphInt1=""0"" _GlyphInt2=""0"" _GlyphInt3=""0"" _GlyphInt4=""0"" _GlyphInt5=""0"" _class=""Story::CGlyphAttsDelta"" _id=""20""><_item _first=""HyperLink""><_second _class=""Story::CHyperLinkAtt""/></_item><_item _first=""ForeColor""><_second _class=""CTextFillAtt""><Fill _class=""CFillSolid""><colour _class=""CRGBColour""/></Fill></_second></_item><_item _first=""BackColor""/><_item _first=""Panose""/><_item _first=""OpticalJustify""/><_item _first=""_GlyphAtt0""/><_item _first=""_GlyphAtt1""/><_item _first=""_GlyphAtt2""/><_item _first=""_GlyphAtt3""/><_item _first=""_GlyphAtt4""/><_item _first=""_GlyphAtt5""/></_object></SerifXML>")
        File.WriteAllText(Path.Combine(tmpDir, "summary.xml"), String.Format("<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0"" _class=""CSummaryInformation"" AppName=""Serif DrawPlus SE 1, 0, 0, 5 (Jun  1, 11:54:18)"" Title=""WordBuilder export"" Author=""WordBuilder"" LastAuthor=""WordBuilder"" Revision=""1"" Created=""{0:dd\/MM\/yyyy hh:mm:ss}"" Saved=""{0:dd\/MM\/yyyy hh:mm:ss}""><DPSummary ObjectCount=""{1}""/></SerifXML>", DateTime.Now, words.Count + 4))

        Dim story As New System.Text.StringBuilder("<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0"" _class=""Story::CRoot"" unit=""72000""><StyleSheet _ref=""3""/><stories>")

        Dim idx As Integer = 0
        For Each c As Context In words
            story.AppendFormat("<_item _class=""Story::CStory"" _id=""{1}"" xml:space=""preserve""><StyleSheet _ref=""3""/><p><paraDelta _ref=""{2}""/><delta _ref=""{3}""/>{0}</p></_item>", c.ToString(), idx * 2 + 26, 19, 20)
            idx += 1
        Next

        story.Append("</stories></SerifXML>")

        File.WriteAllText(Path.Combine(tmpDir, "story.xml"), story.ToString())
        File.WriteAllText(Path.Combine(tmpDir, "printinfo.xml"), "<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0""><PrintInfo BleedDistance=""5669"" LeftMargin=""0"" OutputImage=""Normal"" TopMargin=""0"" TransparentRes=""150""/></SerifXML>")
        File.WriteAllText(Path.Combine(tmpDir, "document.xml"), "<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0"" Pages=""1"" FileFilterType=""-1"" FileFilterBpp=""24"" Margin=""6000"" JumpLength=""6000"" JumpSize=""6000""><DocModel Margins=""36000,36000,36000,36000"" MirroredMargins=""0"" Pages=""1"" Type=""1"" _class=""CDpDocModel""><SheetSize Name=""A4"" Size=""595275,841889""/></DocModel><Scale PageUnit=""0"" WorldDistance=""28346.4571186345"" WorldUnit=""0""/><Grid Colour=""12498100""><Horizontal Gap=""5669.29150390625"" Origin=""0"" Subdivisions=""5""/><Vertical Gap=""5669.29150390625"" Origin=""0"" Subdivisions=""5""/></Grid></SerifXML>")
        File.WriteAllText(Path.Combine(tmpDir, "colours.xml"), "<?xml version=""1.0"" encoding=""UTF-8""?><SerifXML version=""1.0""><colours><_item _class=""CRegistrationColour""/><_item _class=""CRGBColour"" name=""White"" rgb=""16777215""/><_item _class=""CRGBColour"" name=""Black""/><_item _class=""CRGBColour"" name=""Red"" rgb=""255""/><_item _class=""CRGBColour"" name=""Yellow"" rgb=""65535""/><_item _class=""CRGBColour"" name=""Green"" rgb=""65280""/><_item _class=""CRGBColour"" name=""Cyan"" rgb=""16776960""/><_item _class=""CRGBColour"" name=""Blue"" rgb=""16711680""/><_item _class=""CRGBColour"" name=""Magenta"" rgb=""16711935""/><_item _class=""CHSLColour"" hue=""22.1052631578947"" lum=""0.288235294117647"" sat=""0.387755102040816""/><_item _class=""CHSLColour"" hue=""357.703349282297"" lum=""0.519607843137255"" sat=""0.853061224489796""/><_item _class=""CHSLColour"" hue=""15.8048780487805"" lum=""0.543137254901961"" sat=""0.879828326180257""/><_item _class=""CHSLColour"" hue=""32.3502304147465"" lum=""0.543137254901961"" sat=""0.931330472103004""/><_item _class=""CHSLColour"" hue=""36.5625"" lum=""0.607843137254902"" sat=""0.96""/><_item _class=""CHSLColour"" hue=""56.1643835616438"" lum=""0.558823529411765"" sat=""0.973333333333333""/><_item _class=""CHSLColour"" hue=""62.1989528795811"" lum=""0.503921568627451"" sat=""0.754940711462451""/><_item _class=""CHSLColour"" hue=""85.7777777777778"" lum=""0.511764705882353"" sat=""0.542168674698795""/><_item _class=""CHSLColour"" hue=""128.225806451613"" lum=""0.466666666666667"" sat=""0.521008403361345""/><_item _class=""CHSLColour"" hue=""148.356164383562"" lum=""0.286274509803922"" sat=""1""/><_item _class=""CHSLColour"" hue=""151.730769230769"" lum=""0.203921568627451"" sat=""1""/><_item _class=""CHSLColour"" hue=""153.061224489796"" lum=""0.42156862745098"" sat=""0.683720930232558""/><_item _class=""CHSLColour"" hue=""175.739644970414"" lum=""0.331372549019608"" sat=""1""/><_item _class=""CHSLColour"" hue=""197.837837837838"" lum=""0.523529411764706"" sat=""0.761316872427984""/><_item _class=""CHSLColour"" hue=""203.936170212766"" lum=""0.368627450980392"" sat=""1""/><_item _class=""CHSLColour"" hue=""238.2"" lum=""0.376470588235294"" sat=""0.520833333333333""/><_item _class=""CHSLColour"" hue=""245.25"" lum=""0.235294117647059"" sat=""0.666666666666667""/><_item _class=""CHSLColour"" hue=""274.2"" lum=""0.372549019607843"" sat=""0.526315789473684""/><_item _class=""CHSLColour"" hue=""302.222222222222"" lum=""0.364705882352941"" sat=""0.580645161290323""/><_item _class=""CHSLColour"" hue=""324.683544303797"" lum=""0.309803921568627"" sat=""1""/><_item _class=""CHSLColour"" hue=""338.125"" lum=""0.454901960784314"" sat=""0.827586206896552""/><_item _class=""CHSLColour"" hue=""333.623188405797"" lum=""0.523529411764706"" sat=""0.851851851851852""/><_item _class=""CHSLColour"" hue=""32.6086956521739"" lum=""0.690196078431373"" sat=""0.291139240506329""/><_item _class=""CHSLColour"" hue=""28.3333333333334"" lum=""0.529411764705882"" sat=""0.15""/><_item _class=""CHSLColour"" hue=""25.7142857142857"" lum=""0.396078431372549"" sat=""0.138613861386139""/><_item _class=""CHSLColour"" hue=""20"" lum=""0.290196078431373"" sat=""0.121621621621622""/><_item _class=""CHSLColour"" hue=""31.685393258427"" lum=""0.601960784313725"" sat=""0.438423645320197""/><_item _class=""CHSLColour"" hue=""30"" lum=""0.486274509803922"" sat=""0.338709677419355""/><_item _class=""CHSLColour"" hue=""29.6385542168675"" lum=""0.386274509803922"" sat=""0.421319796954315""/><_item _class=""CHSLColour"" hue=""29.6296296296296"" lum=""0.3"" sat=""0.529411764705882""/><_item _class=""CHSLColour"" hue=""28.8311688311688"" lum=""0.225490196078431"" sat=""0.669565217391304""/><_item _class=""CHSLColour"" hue=""24"" lum=""0.150980392156863"" sat=""0.714285714285714""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.125490196078431"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.250980392156863"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.376470588235294"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.501960784313725"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.627450980392157"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.752941176470588"" sat=""0""/><_item _class=""CHSLColour"" hue=""0"" lum=""0.87843137254902"" sat=""0""/></colours></SerifXML>")

        Directory.CreateDirectory(Path.Combine(tmpDir, "Spreads"))

        Dim Pages As XElement = <Pages>
                                    <_item Margins="36000,36000,36000,36000" Rect="0,0,595275,841889" _class="CPage">
                                        <HGuides Data="_arr_i:0:" Rect="0,0,595275,841889" Vertical="0"/>
                                        <VGuides Data="_arr_i:0:" Rect="0,0,595275,841889" Vertical="1"/>
                                    </_item>
                                </Pages>

        Dim WebLayer As XElement = <WebLayer Flags="6" Name="__Draw_Plus_Web_Layer__" _class="CWebLayer"/>
        Dim DesignLayer As XElement = <DesignLayer Colour="16711855" Flags="4" Name="Overlay Layer" _class="CDesignGuideLayer"/>
        Dim VGuides As XElement = <VGuides Data="_arr_i:0:" Rect="-1073741824,-1073741824,1073741823,1073741823" Vertical="1" _class="CGuides"/>
        Dim HGuides As XElement = <HGuides Data="_arr_i:0:" Rect="-1073741824,-1073741824,1073741823,1073741823" Vertical="0" _class="CGuides"/>
        Dim FrameMarker As XElement = <FrameMarker Name="" Type="0" _class="CFrameMarker"/>

        Dim offset As Integer = 26 + words.Count * 2

        Dim spreadsXd As New XDocument(New XDeclaration("1.0", "UTF-8", ""), _
                                       New XElement("SerifXML", _
                                                    New XAttribute("version", "1.0"), _
                                                    New XElement("_item", _
                                                                 New XAttribute("Duration", "10"), _
                                                                 New XAttribute("_class", "CSpread"), _
                                                                 Pages, _
                                                                 New XElement("Layers", _
                                                                              New XElement("_item", _
                                                                                           New XAttribute("Flags", "6"), _
                                                                                           New XAttribute("Name", "Layer 1"), _
                                                                                           New XAttribute("_class", "CLayer"), _
                                                                                           New XElement("Objects", _
                                                                                                        words.Select(Of XElement)(Function(c As Context, i As Integer) _
                                                                                                               New XElement("_item", _
                                                                                                                            New XAttribute("AnimationID", (i + 1).ToString()), _
                                                                                                                            New XAttribute("HideCurve", "1"), _
                                                                                                                            New XAttribute("ID", (i + 1).ToString()), _
                                                                                                                            New XAttribute("Transform", String.Format("_arr_d:6:1;0;{0};0;1;{1}", 150000 + Math.Floor(i / 65) * 200000, 350000 + (i Mod 65) * 12000)), _
                                                                                                                            New XAttribute("_class", "CCurveTextDo"), _
                                                                                                                            New XAttribute("_id", (i + offset).ToString()), _
                                                                                                                            New XElement("Effects"), _
                                                                                                                            New XElement("PenFill", _
                                                                                                                                         New XAttribute("_class", "CFillSolid"), _
                                                                                                                                         New XElement("colour", _
                                                                                                                                                      New XAttribute("_class", "CRGBColour") _
                                                                                                                                         ) _
                                                                                                                            ), _
                                                                                                                            New XElement("TextAtts", _
                                                                                                                                         New XAttribute("_class", "CTextAtt"), _
                                                                                                                                         New XElement("Story", _
                                                                                                                                            New XAttribute("_ref", (26 + i * 2).ToString()) _
                                                                                                                                         ), _
                                                                                                                                         New XElement("Layout", _
                                                                                                                                                      New XAttribute("ArtisticHeight", "1"), _
                                                                                                                                                      New XAttribute("Wrapping", "None"), _
                                                                                                                                                      New XAttribute("_class", "Story::CLayout"), _
                                                                                                                                                      New XElement("Story", _
                                                                                                                                                                   New XAttribute("_ref", (26 + i * 2).ToString()) _
                                                                                                                                                      ), _
                                                                                                                                                      New XElement("LayoutAtts") _
                                                                                                                                         ), _
                                                                                                                                         New XElement("Column", _
                                                                                                                                                      New XAttribute("ArtisticOrigin", "0,0"), _
                                                                                                                                                      New XAttribute("FlowBox", "0,-36994,289116,9569"), _
                                                                                                                                                      New XAttribute("IsSingleLineOnly", "1"), _
                                                                                                                                                      New XAttribute("_class", "CColumn") _
                                                                                                                                         ), _
                                                                                                                                         New XElement("Atts", _
                                                                                                                                                      New XAttribute("ScaleWithObject", "1"), _
                                                                                                                                                      New XAttribute("Wrapping", "2") _
                                                                                                                                         ), _
                                                                                                                                         New XElement("Owner", _
                                                                                                                                                      New XAttribute("_ref", (i + offset).ToString()) _
                                                                                                                                         ) _
                                                                                                                            ), _
                                                                                                                            New XElement("_item", _
                                                                                                                                         New XAttribute("_class", "S2DCurve"), _
                                                                                                                                         New XAttribute("points", "_arr_p:4:-123360,-308196;-73637,-363904;98383,-361154;143353,-299439"), _
                                                                                                                                         New XAttribute("types", "_arr_W:4:16;33;49;16") _
                                                                                                                            ), _
                                                                                                                            New XElement("CurveLineStyle", _
                                                                                                                                         New XAttribute("_class", "CLineStyleNone"), _
                                                                                                                                         New XAttribute("width", "0") _
                                                                                                                            ), _
                                                                                                                            New XElement("CurvePenFill", _
                                                                                                                                         New XAttribute("_class", "CFillSolid"), _
                                                                                                                                         New XElement("colour", _
                                                                                                                                                      New XAttribute("_class", "CRGBColour") _
                                                                                                                                         ) _
                                                                                                                            ), _
                                                                                                                            New XElement("CurveTransparency", _
                                                                                                                                         New XAttribute("_class", "CTransparencyNone") _
                                                                                                                            ) _
                                                                                                               ) _
                                                                                           ) _
                                                                              ) _
                                                                 ) _
                                                    ), _
                                                    WebLayer, _
                                                    DesignLayer, _
                                                    VGuides, _
                                                    HGuides, _
                                                    FrameMarker _
                                       ) _
                         ) _
                         )

        spreadsXd.Save(IO.Path.Combine(tmpDir, "Spreads\1.xml"), SaveOptions.DisableFormatting)

        File.Delete(filename)
        Using zip As New ZipFile(filename)
            zip.AddFile(Path.Combine(tmpDir, "Root.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "printinfo.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "stylesheet.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "colours.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "document.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "story.xml"), "")
            zip.AddFile(Path.Combine(tmpDir, "summary.xml"), "")
            zip.AddEntry("preview.jpg", "", "x")
            zip.AddFile(Path.Combine(tmpDir, "Spreads\1.xml"), "Spreads")

            zip.Save()
        End Using

        Directory.Delete(tmpDir, True)
    End Sub

    Public Overrides ReadOnly Property Type() As String
        Get
            Return "DrawPlus file|*.dpp"
        End Get
    End Property
End Class
