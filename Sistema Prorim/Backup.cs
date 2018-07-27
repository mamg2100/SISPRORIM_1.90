using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Sistema_prorim
{
    public partial class Backup : Form
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        private string path;

        public Backup()
        {
            InitializeComponent();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {            
            path = "c:\\"+ textBox2.Text+"\\";
        }

        private void Backup_Load(object sender, EventArgs e)
        {            
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                //string path;
                //path = "c:\\DUMP_RIM\\";
                //    + year + "-" + month + "-" + day + 
                // "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";

                StreamWriter file = new StreamWriter(path);

                // c:\\Servidor\\IPSERVIDOR.txt

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "prorim_rim2.sql";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;
                //"Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
