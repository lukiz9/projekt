using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string plik = ""; // zapamiętanie nazwy pliku
        Font new1, old1;

        private DialogResult czyzapisac()
        {
            DialogResult odp = MessageBox.Show("Czy chcesz zapisać zmiany w pliku?", "Notepad #",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (odp == DialogResult.Yes)
                zapiszToolStripMenuItem_Click(null, null); // funkcja wywoływana po wybraniu opcji zapisz
            return odp;
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nowy();
        }

        private void nowy()
        {
            if (richTextBox1.Text != "")
            {
                DialogResult odp = czyzapisac();
                if (odp == DialogResult.Cancel)
                    return;
                plik = "";
                richTextBox1.Clear();
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otwórz();
        }

        private void otwórz()
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Dokument tekstowy(*.txt)|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            plik = ofd.FileName;
                            Task<string> text = sr.ReadToEndAsync();
                            richTextBox1.Text = text.Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await zapisz();
        }

        private async Task zapisz()
        {
            if (string.IsNullOrEmpty(plik))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Dokument tekstowy(*.txt)|*.txt|Dokument tekstowy(*.doc)|*.doc|Dokument tekstowy(*.docx)|*.docx|All Files(*.*)|*.*", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            plik = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(richTextBox1.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(plik))
                    {
                        await sw.WriteLineAsync(richTextBox1.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Dokument tekstowy(*.txt)|*.txt|Dokument tekstowy(*.doc)|*.doc|Dokument tekstowy(*.docx)|*.docx|All Files(*.*)|*.*";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                plik = dialog.FileName;
                StreamWriter f = new StreamWriter(plik);
                f.Write(richTextBox1.Text);
                f.Close();
            }
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drukuj();
        }

        private void drukuj()
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 150, 125);
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zakończ();
        }

        private void zakończ()
        {
            if ((richTextBox1.Text == "") || (richTextBox1.Text != ""))
            {
                Application.Exit();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cofnij();
        }

        private void cofnij()
        {
            richTextBox1.Undo();
        }

        private void ponówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ponów();
        }

        private void ponów()
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wytnij();
        }

        private void wytnij()
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kopiuj();
        }

        private void kopiuj()
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void wyczyśćWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wyczyśćwszystko();
        }

        private void wyczyśćwszystko()
        {
            richTextBox1.Clear();
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength == 0)
                richTextBox1.SelectAll();
        }

        private void wstawDatęIGodzinęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wstawdatęigodzinę();
        }

        private void wstawdatęigodzinę()
        {
            richTextBox1.Text = DateTime.Now.ToString();
        }

        private void kolorTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cr = new ColorDialog();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = cr.Color;
            }
        }

        private void czcionkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = richTextBox1.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
            }
        }

        private void notepadInformacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            informacje();
        }

        private static void informacje()
        {
            using (informacje frm = new informacje())
            {
                frm.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                DialogResult odp = czyzapisac();
                if (odp == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            nowy();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            otwórz();
        }

        private async void toolStripButton3_Click(object sender, EventArgs e)
        {
            await zapisz();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            drukuj();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            zakończ();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            cofnij();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ponów();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            wytnij();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            kopiuj();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            wyczyśćwszystko();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            wstawdatęigodzinę();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            informacje();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            old1 = richTextBox1.SelectionFont;
            if (old1.Bold)
                new1 = new Font(old1, old1.Style & ~FontStyle.Bold);
            else
                new1 = new Font(old1, old1.Style | FontStyle.Bold);
            richTextBox1.SelectionFont = new1;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            old1 = richTextBox1.SelectionFont;
            if (old1.Italic)
                new1 = new Font(old1, old1.Style & ~FontStyle.Italic);
            else
                new1 = new Font(old1, old1.Style | FontStyle.Italic);
            richTextBox1.SelectionFont = new1;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            old1 = richTextBox1.SelectionFont;
            if (old1.Underline)
                new1 = new Font(old1, old1.Style & ~FontStyle.Underline);
            else
                new1 = new Font(old1, old1.Style | FontStyle.Underline);
            richTextBox1.SelectionFont = new1;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Center)
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            else
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Right)
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            else
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Left)
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            else
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily Font in FontFamily.Families)
            {
                zmienczcionke.Items.Add(Font.Name.ToString());
            }
        }

        private void zmienrozmiar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, float.Parse(zmienrozmiar.SelectedItem.ToString()));
            }
            catch
            {
            }
        }

        private void zmienczcionke_TextChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Font = new Font(zmienczcionke.Text, richTextBox1.Font.Size);
            }
            catch
            {
            }
        }
    }
}