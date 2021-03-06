﻿

using fNbt;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using MCGT_SignTranslator.GTaylor.Serialization;

namespace MCGT_SignTranslator.GTaylor.Data
{
    public class SignNode : TreeNode
    {
        public NbtCompound SignData;
        private int LoadedLine = 0;
        private LineInfo[] Lines = new LineInfo[4];
        public LineInfo CurrentLine { get { return LoadedLine>0? Lines[LoadedLine-1]:null; } }
        public LineInfo Line1 { get { return Lines[0]; } }
        public LineInfo Line2 { get { return Lines[1]; } }
        public LineInfo Line3 { get { return Lines[2]; } }
        public LineInfo Line4 { get { return Lines[3]; } }
        public static readonly Dictionary<string, Color> ColorDictionary = new Dictionary<string, Color>
        {
            {"black", Color.Black},
             {"dark_blue", Color.DarkBlue},
              {"dark_green", Color.DarkGreen},
              {"dark_aqua", Color.Aqua},
              {"dark_red", Color.DarkRed},
              {"dark_purple", Color.Violet},
              {"dark_grey", Color.DarkGray},
              {"gold", Color.Gold},
               {"grey", Color.Gray},
                {"blue", Color.Blue},
                 {"aqua", Color.Aqua},
                  {"red", Color.Red},
                  {"green", Color.Green},
                   {"yellow", Color.Yellow},
                   {"light_purple", Color.MediumPurple},
                    {"white", Color.White}
        };

        public SignNode(NbtCompound data, string text, ResourcePack resource)
        {
            SignData = data;
            this.Text = text;
            this.BackColor= Color.Green;
            CreateLineInfo();
            CheckTranslationStatus(resource);
        }

        private void CheckTranslationStatus(ResourcePack resource)
        {
            this.BackColor = Color.Green;
            foreach (LineInfo info in Lines)
            {
                if (info.Type == LineInfo.LineType.Text&&!string.IsNullOrWhiteSpace(info.TypeValue1))
                {
                    BackColor = Color.Red;
                    return;
                }
                if (info.Type == LineInfo.LineType.Translate)
                {
                    foreach(KeyValuePair<string,Dictionary<string,string>> Language in resource.Languages)
                    {
                        if(!Language.Value.ContainsKey(info.TypeValue1)|| Language.Value[info.TypeValue1].Equals(ResourcePack.TAG_UNTRANSLATED))
                        {
                            BackColor = Color.Orange;
                        }
                    }
                }
            }
        }

        private void CreateLineInfo()
        {
            string t1 = SignData.Get<NbtString>("Text1").StringValue;
            string t2 = SignData.Get<NbtString>("Text2").StringValue;
            string t3 = SignData.Get<NbtString>("Text3").StringValue;
            string t4 = SignData.Get<NbtString>("Text4").StringValue;
            Lines[0] = new LineInfo(t1);
            Lines[1] = new LineInfo(t2);
            Lines[2] = new LineInfo(t3);
            Lines[3] = new LineInfo(t4);
        }
        public void OnClick(MainForm form)
        {
           
            /*Hashtable table = (Hashtable)Procurios.Public.JSON.JsonDecode(t2);
            foreach (string key in table.Keys)
            {
                Console.WriteLine(String.Format("{0}:{1}", key, table[key]));
            }*/
           
            Lines[0].SetLabel(form.Sign_Text1, form);
            
            Lines[1].SetLabel(form.Sign_Text2, form);
            
            Lines[2].SetLabel(form.Sign_Text3, form);
            
            Lines[3].SetLabel(form.Sign_Text4, form);

            form.Sign_RawText.Text = SignData.Get<NbtString>("Text1").StringValue + SignData.Get<NbtString>("Text2").StringValue + SignData.Get<NbtString>("Text3").StringValue + SignData.Get<NbtString>("Text4").StringValue;

            form.CurrentNode = this;
        }
        public void OnClick(int lineNum, MainForm form)
        {
            LoadedLine = 0;
            int index = lineNum - 1;
            Lines[index].SetForm(form);
            LoadedLine = lineNum;
            //Lines[index].Print();
        }
        public void LineChanged(MainForm form)
        {
            SignData.Get<NbtString>("Text1").Value = Lines[0].ToJSON();
            SignData.Get<NbtString>("Text2").Value = Lines[1].ToJSON();
            SignData.Get<NbtString>("Text3").Value = Lines[2].ToJSON();
            SignData.Get<NbtString>("Text4").Value = Lines[3].ToJSON();
            Lines[0].SetLabel(form.Sign_Text1, form);
            Lines[1].SetLabel(form.Sign_Text2, form);
            Lines[2].SetLabel(form.Sign_Text3, form);
            Lines[3].SetLabel(form.Sign_Text4, form);
            if(CurrentLine!=null)
                CurrentLine.SetForm(form);
            form.MarkUnsaved();
            form.Sign_RawText.Text = SignData.Get<NbtString>("Text1").StringValue+ SignData.Get<NbtString>("Text2").StringValue+ SignData.Get<NbtString>("Text3").StringValue+ SignData.Get<NbtString>("Text4").StringValue;
            CheckTranslationStatus(form.Resource);
        }
        internal string RemoveTextWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        public void LocaliseAllLines(MainForm mainForm)
        {
            bool changed = false;
            foreach (LineInfo info in Lines)
            {
                if (info.Type == LineInfo.LineType.Text) {
                    if (!string.IsNullOrWhiteSpace(info.TypeValue1))
                    {
                        LoadedLine = 0;
                        info.Type = LineInfo.LineType.Translate;
                        string txt = info.TypeValue1;
                        // Console.WriteLine(txt+":"+info.Text+":"+ RemoveTextWhitespace(txt.Substring(0, Math.Min(txt.Length, 10))));

                        info.TypeValue1 = "sign." + TextUtil.StripNonKeyCharacters(RemoveTextWhitespace(txt.Substring(0, Math.Min(txt.Length, 10))));
                        changed = true;
                        //info.TypeValue1 = txt;
                        mainForm.Resource.SetKey(info.TypeValue1, txt, mainForm.CurrentLanguage);
                    }
                    else if (!string.IsNullOrWhiteSpace(info.Text)&&!info.Text.Equals("null"))
                    {
                        LoadedLine = 0;
                        info.Type = LineInfo.LineType.Translate;
                        string txt = info.Text;
                        // Console.WriteLine(txt+":"+info.Text+":"+ RemoveTextWhitespace(txt.Substring(0, Math.Min(txt.Length, 10))));

                        info.TypeValue1 = "sign." + TextUtil.StripNonKeyCharacters(RemoveTextWhitespace(txt.Substring(0, Math.Min(txt.Length, 10))));
                        changed = true;
                        //info.TypeValue1 = txt;
                        mainForm.Resource.SetKey(info.TypeValue1, txt, mainForm.CurrentLanguage);
                    }
                }
            }
            if (changed)
                LineChanged(mainForm);
        }
        
    }

    public class LineInfo
    {
        public Color TextColor = Color.Black;
        public string Text = "";
        public LineType Type = LineType.Text;
        private string m_typeValue1= "";
        private string m_typeValue2="";
        public string TypeValue1
        {
            set
            {
                m_typeValue1 = value;
                Text = m_typeValue1;
            }
            get {return m_typeValue1; }
        }
        public string TypeValue2
        {
            set{ m_typeValue2 = value;}
        }
        //private string m_typeValue2="";
        //public string 
        public bool Bold = false;
        public bool Italic = false;
        public bool Underlined = false;
        public bool Strikethrough = false;
        public bool Obfuscated = false;
        public bool HasEvent = false;
        public string Action="";
        public string ActionValue="";

        public enum LineType
        {
            Text, Score, Selector, Translate
        }
        public LineInfo(string json)
        {
            if (json.StartsWith("{"))
                FromJSON(json);
            else
            {
                if (json.StartsWith("\"") && json.EndsWith("\""))
                    if (json.Length == 2)
                        json = "";
                    else
                        json = json.Substring(1, json.Length - 2);
                Text = json;
            }
                
        }
       
        public void SetLabel(Label lbl, MainForm form)
        {
            if (Type == LineType.Translate)
            {
                if (string.IsNullOrWhiteSpace(m_typeValue1))
                    lbl.Text = "";
                else
                {
                    lbl.Text = form.Resource.Localise(m_typeValue1, form.CurrentLanguage);
                    m_typeValue2 = lbl.Text;
                }
            }
            else {
                if (Text.ToLower().Equals("null") && string.IsNullOrWhiteSpace(m_typeValue1))
                    lbl.Text = "";
                else
                    lbl.Text = Text;
            }
            FontStyle formatting = (Bold ? FontStyle.Bold : FontStyle.Regular) | (Italic ? FontStyle.Italic : FontStyle.Regular) | (Underlined ? FontStyle.Underline : FontStyle.Regular) | (Strikethrough ? FontStyle.Strikeout: FontStyle.Regular);
            lbl.Font = new Font(lbl.Font, formatting);
            lbl.ForeColor = TextColor;
        }
        public void SetForm(MainForm form)
        {
            //Type
            form.Sign_TypeChooser.SelectedIndex = form.Sign_TypeChooser.FindStringExact(Type.ToString());
            form.Sign_TypeValue1.Text = m_typeValue1;
            form.Sign_TypeValue2.Text = m_typeValue2;
            //Color
            form.Sign_ColorChooser.SelectedIndex = form.Sign_ColorChooser.FindStringExact(SignNode.ColorDictionary.FirstOrDefault(x => x.Value == (TextColor)).Key);
            //Event
            form.Sign_EventButton.Checked = HasEvent;
            if (HasEvent)
            {
                form.Sign_Action.SelectedIndex = form.Sign_Action.FindStringExact(Action);
                form.Sign_ActionValue.Text = ActionValue;
            }
            form.SetFormatting(Bold, Italic, Underlined, Strikethrough, Obfuscated);
            
        }
        public void FromJSON(string json)
        {
            /*
             foreach (string key in table.Keys)
                {
                        Console.WriteLine(String.Format("{0}: {1}", key, table[key]));
                }
            */
            Hashtable table = (Hashtable)Procurios.Public.JSON.JsonDecode(json);
            if (table != null)
            {
                if (table.ContainsKey("text"))
                {
                    Type = LineType.Text;
                    m_typeValue1 = (string)table["text"];
                }
                if (table.ContainsKey("selector"))
                {
                    Type = LineType.Selector;
                    m_typeValue1 = (string)table["selector"];
                }
                if (table.ContainsKey("translate"))
                {
                    Type = LineType.Translate;
                    m_typeValue1 = (string)table["translate"];
                }
                if (table.ContainsKey("score"))
                {
                    Type = LineType.Score;
                    Hashtable scoreTable = (Hashtable)table["score"];
                    if (scoreTable.ContainsKey("name"))
                        m_typeValue1 = (string)scoreTable["name"];
                    if (scoreTable.ContainsKey("objective"))
                        m_typeValue2 = (string)scoreTable["objective"];
                }
                Text = m_typeValue1;
                if (table.ContainsKey("color"))
                {
                    TextColor = SignNode.ColorDictionary[(string)table["color"]];
                }
                if (table.ContainsKey("bold"))
                    Bold = (bool)table["bold"];
                if (table.ContainsKey("italic"))
                    Italic = (bool)table["italic"];
                if (table.ContainsKey("underlined"))
                    Underlined = (bool)table["underlined"];
                if (table.ContainsKey("obfuscated"))
                    Obfuscated = (bool)table["obfuscated"];
                if (table.ContainsKey("strikethrough"))
                    Strikethrough = (bool)table["strikethrough"];
                if (table.ContainsKey("clickEvent"))
                {
                    HasEvent = true;
                    Hashtable eventTable = (Hashtable)table["clickEvent"];
                    if (eventTable.ContainsKey("action"))
                        Action = (string)eventTable["action"];
                    if (eventTable.ContainsKey("value"))
                        ActionValue = (string)eventTable["value"];
                }

            }

        }
        public string ToJSON()
        {
            if (Text.ToLower().Equals("null") && string.IsNullOrWhiteSpace(m_typeValue1))
                return "null";
            string json = "{";
            switch (Type)
            {
                case LineType.Score:
                    json += "\"score\":{\"name\":\""+m_typeValue1+"\",\"objective\":\""+m_typeValue2+"\"}";
                    break;
                case LineType.Text:
                    json += "\"text\":\""+ m_typeValue1 + "\"";
                    break;
                case LineType.Selector:
                    json += "\"selector\":\"" + m_typeValue1 + "\"";
                    break;
                case LineType.Translate:
                    json += "\"translate\":\"" + m_typeValue1 + "\"";
                    break;
            }
            string colorString = SignNode.ColorDictionary.FirstOrDefault(x => x.Value==(TextColor)).Key;
            json += ",\"color\":\""+colorString+"\"";
            //INSERT COLOR HERE
            if (Bold)
                json += ",\"bold\":true";
            if (Italic)
                json += ",\"italic\":true";
            if (Underlined)
                json += ",\"underlined\":true";
            if (Strikethrough)
                json += ",\"strikethrough\":true";
            if (Obfuscated)
                json += ",\"obfuscated\":true";
            if (!string.IsNullOrWhiteSpace(Action))
            {
                json += ",\"clickEvent\":{\"action\":\""+Action+"\",\"value\":\""+ActionValue+"\"}";
 
            }
            json += "}";
            return json;

        }
        private string StripEnclosingSMarks(string str)
        {
            if (str.StartsWith("\""))
            {
                return str.Substring(1, str.Length - 2);
            }
            return str;
        }
        public void Print()
        {

            Console.WriteLine("Type:          " + Type.ToString());
            Console.WriteLine("TypeVal1:      "+m_typeValue1.ToString());
            Console.WriteLine("TypeVal2:      " + m_typeValue2.ToString());
            Console.WriteLine("Color:         " + TextColor.ToString());
            Console.WriteLine("Has Event:     " + HasEvent.ToString());
            Console.WriteLine("Action:        " + Action.ToString());
            Console.WriteLine("ActionVal:     " + ActionValue.ToString());

            Console.WriteLine("Bold:          " + Bold.ToString());
            Console.WriteLine("Italic:        " + Italic.ToString());
            Console.WriteLine("Underlined:    " + Underlined.ToString());
            Console.WriteLine("Strikethrough: " + Strikethrough.ToString());
            Console.WriteLine("Obfuscated:    " + Obfuscated.ToString());
        }
        internal static LineType FromString(string v)
        {
            switch (v)
            {
                case "Text":
                    return LineType.Text;
                case "Selector":
                    return LineType.Selector;
                case "Score":
                    return LineType.Score;
                case "Translate":
                    return LineType.Translate;
            }
            return LineType.Text;
        }

        internal void Clear()
        {
            m_typeValue1 = "";
            m_typeValue2 = "";
        }
    }
}
