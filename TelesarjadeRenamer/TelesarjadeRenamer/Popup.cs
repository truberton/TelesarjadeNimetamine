﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TelesarjadeRenamer
{
    public partial class Popup : Form
    {
        public string failiTüüp;
        public string nimi;
        public int EsimeneOsa;
        public string LisaTekst;
        public string eraldaja;

        public Popup()
        {
            InitializeComponent();
        }

        private void failiTüüp_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            failiTüüp = objTextBox.Text;
        }

        private void nimi_textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            nimi = objTextBox.Text;
        }

        private void EsimeneOsa_textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            EsimeneOsa = Int32.Parse(objTextBox.Text);
        }

        private void EsimeneOsa_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
            string path = ((Form1)f).text;

            if (string.IsNullOrWhiteSpace(nimi))
            {
                MessageBox.Show("Palun täida kõik textboxid");
            }

            else
            {
                if (!checkBox2.Checked)
                {
                    string[] AllFiles = Directory.GetFiles(path);
                    if (checkBox1.Checked)
                    {
                        if (!nimi.EndsWith(" "))
                        {
                            nimi += " ";
                        }
                    }
                    foreach (string file in AllFiles)
                    {
                        //Siin on if, sest kui sisestad S01E*, siis see näeb välja S01E1 kuni S01E10, kuid nii on see S01E01 kuni S01E10 (palju ilusam)
                        failiTüüp = Path.GetExtension(path + file);
                        if (checkBox3.Checked)
                        {
                            using (EraldiNimetamine Eraldi = new EraldiNimetamine())
                            {
                                Eraldi.LabelText = "Mida tahad lisada " + EsimeneOsa + ". osa lõppu";
                                Eraldi.ShowDialog();
                                LisaTekst = Eraldi.LisaTekst;
                            }
                            if (EsimeneOsa < 10 && !checkBox1.Checked)
                            {
                                File.Move(file, path + nimi + "0" + Convert.ToString(EsimeneOsa) + eraldaja + LisaTekst + failiTüüp);
                            }
                            else
                            {
                                File.Move(file, path + nimi + Convert.ToString(EsimeneOsa) + eraldaja + LisaTekst + failiTüüp);
                            } 
                        }
                        else
                        {
                            if (EsimeneOsa < 10 && !checkBox1.Checked)
                            {
                                File.Move(file, path + nimi + "0" + Convert.ToString(EsimeneOsa) + failiTüüp);
                            }
                            else
                            {
                                File.Move(file, path + nimi + Convert.ToString(EsimeneOsa) + failiTüüp);
                            }
                        }
                        EsimeneOsa++;
                    } 
                }
                else
                {
                    string[] AllFiles = Directory.GetFiles(path, "*" + failiTüüp);
                    if (checkBox1.Checked)
                    {
                        if (!nimi.EndsWith(" "))
                        {
                            nimi += " ";
                        }
                    }
                    foreach (string file in AllFiles)
                    {
                        //Siin on if, sest kui sisestad S01E*, siis see näeb välja S01E1 kuni S01E10, kuid nii on see S01E01 kuni S01E10 (palju ilusam)
                        if (EsimeneOsa < 10 && !checkBox1.Checked)
                        {
                            File.Move(file, path + nimi + "0" + Convert.ToString(EsimeneOsa) + failiTüüp);
                        }
                        else
                        {
                            File.Move(file, path + nimi + Convert.ToString(EsimeneOsa) + failiTüüp);
                        }
                        EsimeneOsa++;
                    }
                }
                MessageBox.Show("Telesarjade nimed on vahetatud");
            }
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                using (Eraldaja Eraldaja = new Eraldaja())
                {
                    Eraldaja.ShowDialog();
                    eraldaja = Eraldaja.eraldaja;
                }
            }
        }
    }
}
