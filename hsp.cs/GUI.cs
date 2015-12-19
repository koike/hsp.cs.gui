﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hsp.cs
{
    public class HSPGUI : Form
    {
        public HSPGUI()
        {
            //
        }

        static void Screen(int id)
        {
            if (Program.Window.Count > id)
            {
                //Activate
            }
            else
            {
                Program.Window.Add(new Form());
            }
        }

        public static string Print(string strings)
        {
            strings = Analyzer.StringUnEscape(strings);
            Program.AddFunction[1] += "g.DrawString(" + strings + ".ToString(), font, brush, CurrentPosX, CurrentPosY);\n" +
                                      "CurrentPosY += FontSize;\n";
            return "//Print(" + strings + ");";
        }

        public static string Pos(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (strings.Equals(string.Empty))
            {
                Program.AddFunction[1] += "CurrentPosX = CurrentPosX;\n" +
                                          "CurrentPosY = CurrentPosY;\n";
                return "//SetCurrentPos(CurrentPosX, CurrentPosY);";
            }
            else if (p.Count() == 2)
            {
                Program.AddFunction[1] += "CurrentPosX = " + p[0] + ";\n" +
                                          "CurrentPosY = " + p[1] + ";\n";
                return "//SetCurrentPos(" + p[0] + ", " + p[1] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Wait(string strings)
        {
            Program.UsingCheck("using System.Threading");

            Program.AddFunction[1] += "Thread.Sleep(" + strings + " * 10);\n" +
                                      "Application.DoEvents();\n";
            return "//wait " + strings + " * 10";
        }


        public static string Screen(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            //Program.Window.Add();

            if (!Program.AddFunction[0].Contains("public void screen"))
            {
                Program.AddFunction[0] += "public void screen(Form form, int width, int height)\n{\n" +
                                          "form.ClientSize = new Size(width, height);\n}\n\n";
            }

            return "program.screen(form" + p[0] + ", " + p[1] + ", " + p[2] + ")";
        }

        public static string Title(string strings)
        {
            if (!Program.AddFunction[0].Contains("public void Title"))
            {
                Program.AddFunction[0] += "public void Title(Form form, string strings)\n{\n" +
                                          "form.Text = strings;\n}\n";

            }
            
            return "program.Title(CurrentScreenID, " + strings + ")";
        }

        public static string Circle(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            /*if (int.Parse(p[0].ToString()) > int.Parse(p[2].ToString()))
            {
                var temp = p[0];
                p[0] = p[2];
                p[2] = temp;
            }
            if (int.Parse(p[1].ToString()) > int.Parse(p[3].ToString()))
            {
                var temp = p[1];
                p[1] = p[3];
                p[3] = temp;
            }*/

            if (p.Count() == 4)
            {
                Program.AddFunction[1] += "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + 
                                          p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
            }
            else if (p.Count() == 5)
            {
                if (p[4].Equals("0"))
                {
                    Program.AddFunction[1] += "g.DrawEllipse(pen, " + p[0] + ", " + p[1] + ", " + 
                                            p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
                }
                else
                {
                    Program.AddFunction[1] += "g.FillEllipse(brush, " + p[0] + ", " + p[1] + ", " + 
                                              p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
                }
            }
            else
            {
                //
            }

            return "//Circle(" + p[0] + ", " + p[1] + ", " + p[2] + ", " + p[3] + ");";
        }

        public static string Boxf(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 1)
            {
                Program.AddFunction[1] += "g.FillRectangle(brush, 0, 0, " +
                                          "CurrentScreenID.Width, " + "CurrentScreenID.Height);\n";
                return "//Boxf(0, 0, CurrentScreenID.Width, " + "CurrentScreenID.Height);";
            }
            else if (p.Count() == 2)
            {
                Program.AddFunction[1] += "g.FillRectangle(brush, " + p[0] + ", " + p[1] + ", " +
                                          "CurrentScreenID.Width, " + "CurrentScreenID.Height);\n";
                return "//Boxf(" + p[0] + ", " + p[1] + ", CurrentScreenID.Width, " + "CurrentScreenID.Height);";
            }
            else if (p.Count() == 4)
            {
                /*if (int.Parse(p[0].ToString()) > int.Parse(p[2].ToString()))
                {
                    var temp = p[0];
                    p[0] = p[2];
                    p[2] = temp;
                }
                if (int.Parse(p[1].ToString()) > int.Parse(p[3].ToString()))
                {
                    var temp = p[1];
                    p[1] = p[3];
                    p[3] = temp;
                }左上と右下じゃなくても動かすための*/

                if (p[0].Equals(string.Empty))
                {
                    p[0] = "0";
                }
                if (p[1].Equals(string.Empty))
                {
                    p[1] = "0";
                }
                if (p[2].Equals(string.Empty))
                {
                    p[2] = "CurrentScreenID.Width";
                }
                if (p[3].Equals(string.Empty))
                {
                    p[3] = "CurrentScreenID.Height";
                }

                Program.AddFunction[1] += "g.FillRectangle(brush, " + p[0] + ", " + p[1] + ", " + 
                                          p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");\n";
                return "//Boxf(" + p[0] + ", " + p[1] + ", " + p[2] + " - " + p[0] + ", " + p[3] + " - " + p[1] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Line(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 2)
            {
                Program.AddFunction[1] += "g.DrawLine(pen, CurrentPosX, CurrentPosY, " + p[0] + ", " + p[1] + ");\n" +
                                          "CurrentPosX = " + p[0] + ";\nCurrentPosY = " + p[1] + ";\n";
                return "//Line(CurrentPosX, CurrentPosY, " + p[0] + ", " + p[1] + ");";
            }
            else if (p.Count() == 4)
            {
                Program.AddFunction[1] += "g.DrawLine(pen, " + p[2] + ", " + p[3] + ", " + p[0] + ", " + p[1] + ");\n" +
                                          "CurrentPosX = " + p[0] + ";\nCurrentPosY = " + p[1] + ";\n";
                return "//Line(" + p[2] + ", " + p[3] + ", " + p[0] + ", " + p[1] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Color(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            if (p.Count() == 3)
            {
                if (p[0].Equals(string.Empty))
                {
                    p[0] = "0";
                }
                if (p[1].Equals(string.Empty))
                {
                    p[1] = "0";
                }
                if (p[2].Equals(string.Empty))
                {
                    p[2] = "0";
                }

                Program.AddFunction[1] += "brush = new SolidBrush(Color.FromArgb(" + p[0] + ", " + p[1] + ", " + p[2] + "));\n" +
                                          "pen = new Pen(Color.FromArgb(" + p[0] + ", " + p[1] + ", " + p[2] + "));\n";
                return "//Color(" + p[0] + ", " + p[1] + ", " + p[2] + ");";
            }
            else
            {
                return "error";
            }
        }

        public static string Getkey(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            //変数名として正しいか
            if (Program.VariableNameRule.Contains(p[0][0]))
            {
                //変数名ではない
            }
            else
            {
                //変数リストに含まれていない場合
                if (!Program.VariableList.Contains(p[0]))
                {
                    p[0] = "public static dynamic " + p[0];
                    //変数リストに追加
                    Program.VariableList.Add(p[0]);
                }
            }

            if (p.Count() == 1)
            {
                Program.ProgramField += p[0] + " = e.MouseButtons.Left;\n";
                return "//getkey()";
            }
            else if (p.Count() == 2)
            {
                switch (p[1])
                {
                    case "1":
                        Program.ProgramField += p[0] + " = Keys.MouseButtons.Left;\n";
                        break;
                    case "2":
                        Program.ProgramField += p[0] + " = Keys.MouseButtons.Right;\n";
                        break;
                    case "3":
                        Program.ProgramField += "CTRL +scrollLock;\n";
                        break;
                    case "4":
                        Program.ProgramField += p[0] + " = Keys.MouseButtons.Middle;\n";
                        break;
                    case "5":
                        Program.ProgramField += p[0] + " = Keys.MouseButtons.XButton1;\n";
                        break;
                    case "6":
                        Program.ProgramField += p[0] + " = Keys.MouseButtons.XButton2;\n";
                        break;
                    case "8":
                        Program.ProgramField += p[0] + " = Keys.Back;\n";
                        break;
                    case "9":
                        Program.ProgramField += p[0] + " = Keys.Tab;\n";
                        break;
                    case "12":
                        Program.ProgramField += "clear;\n";
                        break;
                    case "13":
                        Program.ProgramField += p[0] + " = Keys.Enter;\n";
                        break;
                    case "16":
                        Program.ProgramField += p[0] + " = Keys.Shift;\n";
                        break;
                    case "17":
                        Program.ProgramField += p[0] + " = Keys.Control;\n";
                        break;
                    case "18":
                        Program.ProgramField += p[0] + " = Keys.Alt;\n";
                        break;
                    case "19":
                        Program.ProgramField += p[0] + " = Keys.Pause;\n";
                        break;
                    case "20":
                        Program.ProgramField += p[0] + " = Keys.CapsLock;\n";
                        break;
                    case "21":
                        Program.ProgramField += "IME かな;\n";
                        break;
                    case "23":
                        Program.ProgramField += "IME Junja;\n";
                        break;
                    case "24":
                        Program.ProgramField += "IME ファイナル;\n";
                        break;
                    case "25":
                        Program.ProgramField += "IME 漢字;\n";
                        break;
                    case "27":
                        Program.ProgramField += p[0] + " = Keys.Escape;\n";
                        break;
                    case "28":
                        Program.ProgramField += "変換;\n";
                        break;
                    case "29":
                        Program.ProgramField += "無変換;\n";
                        break;
                    case "30":
                        Program.ProgramField += "IME 使用可能;\n";
                        break;
                    case "31":
                        Program.ProgramField += "IME モード変更要求;\n";
                        break;
                    case "32":
                        Program.ProgramField += p[0] + " = Keys.Space;\n";
                        break;
                    case "33":
                        Program.ProgramField += p[0] + " = Keys.PageUp;\n";
                        break;
                    case "34":
                        Program.ProgramField += p[0] + " = Keys.PageDown;\n";
                        break;
                    case "35":
                        Program.ProgramField += p[0] + " = Keys.End;\n";
                        break;
                    case "36":
                        Program.ProgramField += p[0] + " = Keys.Home;\n";
                        break;
                    case "37":
                        Program.ProgramField += p[0] + " = Keys.Left;\n";
                        break;
                    case "38":
                        Program.ProgramField += p[0] + " = Keys.Up;\n";
                        break;
                    case "39":
                        Program.ProgramField += p[0] + " = Keys.Right;\n";
                        break;
                    case "40":
                        Program.ProgramField += p[0] + " = Keys.Down;\n";
                        break;
                    case "41":
                        Program.ProgramField += "Select;\n";
                        break;
                    case "42":
                        Program.ProgramField += "Print;\n";
                        break;
                    case "43":
                        Program.ProgramField += "Execute;\n";
                        break;
                    case "44":
                        Program.ProgramField += p[0] + " = Keys.PrintScreen;\n";
                        break;
                    case "45":
                        Program.ProgramField += p[0] + " = Keys.Insert;\n";
                        break;
                    case "46":
                        Program.ProgramField += p[0] + " = Keys.Delete;\n";
                        break;
                    case "47":
                        Program.ProgramField += "Help;\n";
                        break;
                    case "48":
                        Program.ProgramField += p[0] + " = Keys.D0;\n";
                        break;
                    case "49":
                        Program.ProgramField += p[0] + " = Keys.D1;\n";
                        break;
                    case "50":
                        Program.ProgramField += p[0] + " = Keys.D2;\n";
                        break;
                    case "51":
                        Program.ProgramField += p[0] + " = Keys.D3;\n";
                        break;
                    case "52":
                        Program.ProgramField += p[0] + " = Keys.D4;\n";
                        break;
                    case "53":
                        Program.ProgramField += p[0] + " = Keys.D5;\n";
                        break;
                    case "54":
                        Program.ProgramField += p[0] + " = Keys.D6;\n";
                        break;
                    case "55":
                        Program.ProgramField += p[0] + " = Keys.D7;\n";
                        break;
                    case "56":
                        Program.ProgramField += p[0] + " = Keys.D8;\n";
                        break;
                    case "57":
                        Program.ProgramField += p[0] + " = Keys.D9;\n";
                        break;
                    case "65":
                        Program.ProgramField += p[0] + " = Keys.A;\n";
                        break;
                    case "66":
                        Program.ProgramField += p[0] + " = Keys.B;\n";
                        break;
                    case "67":
                        Program.ProgramField += p[0] + " = Keys.C;\n";
                        break;
                    case "68":
                        Program.ProgramField += p[0] + " = Keys.D;\n";
                        break;
                    case "69":
                        Program.ProgramField += p[0] + " = Keys.E;\n";
                        break;
                    case "70":
                        Program.ProgramField += p[0] + " = Keys.F;\n";
                        break;
                    case "71":
                        Program.ProgramField += p[0] + " = Keys.G;\n";
                        break;
                    case "72":
                        Program.ProgramField += p[0] + " = Keys.H;\n";
                        break;
                    case "73":
                        Program.ProgramField += p[0] + " = Keys.I;\n";
                        break;
                    case "74":
                        Program.ProgramField += p[0] + " = Keys.J;\n";
                        break;
                    case "75":
                        Program.ProgramField += p[0] + " = Keys.K;\n";
                        break;
                    case "76":
                        Program.ProgramField += p[0] + " = Keys.L;\n";
                        break;
                    case "77":
                        Program.ProgramField += p[0] + " = Keys.M;\n";
                        break;
                    case "78":
                        Program.ProgramField += p[0] + " = Keys.N;\n";
                        break;
                    case "79":
                        Program.ProgramField += p[0] + " = Keys.O;\n";
                        break;
                    case "80":
                        Program.ProgramField += p[0] + " = Keys.P;\n";
                        break;
                    case "81":
                        Program.ProgramField += p[0] + " = Keys.Q;\n";
                        break;
                    case "82":
                        Program.ProgramField += p[0] + " = Keys.R;\n";
                        break;
                    case "83":
                        Program.ProgramField += p[0] + " = Keys.S;\n";
                        break;
                    case "84":
                        Program.ProgramField += p[0] + " = Keys.T;\n";
                        break;
                    case "85":
                        Program.ProgramField += p[0] + " = Keys.U;\n";
                        break;
                    case "86":
                        Program.ProgramField += p[0] + " = Keys.V;\n";
                        break;
                    case "87":
                        Program.ProgramField += p[0] + " = Keys.W;\n";
                        break;
                    case "88":
                        Program.ProgramField += p[0] + " = Keys.X;\n";
                        break;
                    case "89":
                        Program.ProgramField += p[0] + " = Keys.Y;\n";
                        break;
                    case "90":
                        Program.ProgramField += p[0] + " = Keys.Z;\n";
                        break;
                    case "91":
                        Program.ProgramField += "左 win;\n";
                        break;
                    case "92":
                        Program.ProgramField += "右 win;\n";
                        break;
                    case "93":
                        Program.ProgramField += "アプリケーションキー;\n";
                        break;
                    case "94":
                        Program.ProgramField += "予約済み;\n";
                        break;
                    case "95":
                        Program.ProgramField += p[0] + " = Keys.Sleep;\n";
                        break;
                    case "96":
                        Program.ProgramField += p[0] + " = Keys.NumPad0;\n";
                        break;
                    case "97":
                        Program.ProgramField += p[0] + " = Keys.NumPad1;\n";
                        break;
                    case "98":
                        Program.ProgramField += p[0] + " = Keys.NumPad2;\n";
                        break;
                    case "99":
                        Program.ProgramField += p[0] + " = Keys.NumPad3;\n";
                        break;
                    case "100":
                        Program.ProgramField += p[0] + " = Keys.NumPad4;\n";
                        break;
                    case "101":
                        Program.ProgramField += p[0] + " = Keys.NumPad5;\n";
                        break;
                    case "102":
                        Program.ProgramField += p[0] + " = Keys.NumPad6;\n";
                        break;
                    case "103":
                        Program.ProgramField += p[0] + " = Keys.NumPad7;\n";
                        break;
                    case "104":
                        Program.ProgramField += p[0] + " = Keys.NumPad8;\n";
                        break;
                    case "105":
                        Program.ProgramField += p[0] + " = Keys.NumPad9;\n";
                        break;
                    case "106":
                        Program.ProgramField += p[0] + " = Keys.Multiply;\n";
                        break;
                    case "107":
                        Program.ProgramField += p[0] + " = Keys.Add;\n";
                        break;
                    case "108":
                        Program.ProgramField += p[0] + " = Keys.Separator;\n";
                        break;
                    case "109":
                        Program.ProgramField += p[0] + " = Keys.Subtract;\n";
                        break;
                    case "110":
                        Program.ProgramField += p[0] + " = Keys.Decimal;\n";
                        break;
                    case "111":
                        Program.ProgramField += p[0] + " = Keys.Divide;\n";
                        break;
                    case "112":
                        Program.ProgramField += p[0] + " = Keys.F1;\n";
                        break;
                    case "113":
                        Program.ProgramField += p[0] + " = Keys.F2;\n";
                        break;
                    case "114":
                        Program.ProgramField += p[0] + " = Keys.F3;\n";
                        break;
                    case "115":
                        Program.ProgramField += p[0] + " = Keys.F4;\n";
                        break;
                    case "116":
                        Program.ProgramField += p[0] + " = Keys.F5;\n";
                        break;
                    case "117":
                        Program.ProgramField += p[0] + " = Keys.F6;\n";
                        break;
                    case "118":
                        Program.ProgramField += p[0] + " = Keys.F7;\n";
                        break;
                    case "119":
                        Program.ProgramField += p[0] + " = Keys.F8;\n";
                        break;
                    case "120":
                        Program.ProgramField += p[0] + " = Keys.F9;\n";
                        break;
                    case "121":
                        Program.ProgramField += p[0] + " = Keys.F10;\n";
                        break;
                    case "122":
                        Program.ProgramField += p[0] + " = Keys.F11;\n";
                        break;
                    case "123":
                        Program.ProgramField += p[0] + " = Keys.F12;\n";
                        break;
                }
                return "//getkey()";
            }
            else
            {
                return "error";
            }
        }

        public static string Objsize(string strings)
        {
            var p = strings.Split(',');

            for (var i = 0; i < p.Count(); i++)
            {
                p[i] = p[i].Trim();
            }

            Program.ProgramField += "objsizeX = " + p[0] + ";\n" +
                                    "objsizeY = " + p[1] + ";\n" +
                                    "objSpace = " + p[2] + ";\n";
            return "boxSize(" + p[0] + ", " + p[1] + ")";
        }

        public static void Mousex(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.PointToClient(Cursor.Position).X";
        }

        public static void Mousey(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.PointToClient(Cursor.Position).Y";
        }

        public static void Ginfo_sizeX(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Width";
        }

        public static void Ginfo_sizeY(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Height";
        }

        public static void Ginfo_r(List<string> sentence, int i)
        {
            sentence[i] = "pen.Color.R";
        }

        public static void Ginfo_g(List<string> sentence, int i)
        {
            sentence[i] = "pen.Color.G";
        }

        public static void Ginfo_b(List<string> sentence, int i)
        {
            sentence[i] = "pen.Color.B";
        }

        public static void Ginfo_cx(List<string> sentence, int i)
        {
            sentence[i] = "CurrentPosX";
        }

        public static void Ginfo_cy(List<string> sentence, int i)
        {
            sentence[i] = "CurrentPosY";
        }

        public static void Ginfo_dispx(List<string> sentence, int i)
        {
            sentence[i] = "Screen.PrimaryScreen.Bounds.Width";
        }

        public static void Ginfo_dispy(List<string> sentence, int i)
        {
            sentence[i] = "Screen.PrimaryScreen.Bounds.Height";
        }

        public static void Ginfo_wx1(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Left";
        }

        public static void Ginfo_wx2(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Right";
        }

        public static void Ginfo_wy1(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Top";
        }

        public static void Ginfo_wy2(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Bottom";
        }

        public static void Hwnd(List<string> sentence, int i)
        {
            sentence[i] = "CurrentScreenID.Handle";
        }


        public static void __date__(List<string> sentence, int i)
        {
            sentence[i] = "DateTime.Now.ToString(\"d\")";
        }

        public static void __time__(List<string> sentence, int i)
        {
            sentence[i] = "DateTime.Now.ToString(\"T\")";
        }
    }
}
