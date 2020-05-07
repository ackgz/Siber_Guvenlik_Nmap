using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(String[] args)
        {
            List<NMAP> nmap = new List<NMAP>();
            nmap.Add(new NMAP("http-sql-injection"));
            nmap.Add(new NMAP("ssl-ccs-injection"));
            nmap.Add(new NMAP("http-csrf"));
            StreamWriter yazdır = new StreamWriter("result.xml");
            nmap.ForEach(x => yazdır.WriteLine(x.StandartCikis));
            yazdır.Flush();
            yazdır.Close();
        }

        class NMAP : IDisposable
        {
            private ProcessStartInfo processS = new ProcessStartInfo();
            private Process process = new Process();
            private string cikti;
            private string script;
            public NMAP(string script)
            {
                this.script = script;
                processS.Arguments = "-p 80 --script " + this.script + " testphp.vulnweb.com -oX -";
                processS.RedirectStandardOutput = true;
                processS.FileName = "nmap";
                process.StartInfo = processS;
            }

            public void Sonuc()
            {
                process.Sonuc();
            }

            private void CiktiAl()
            {
                if (string.IsNullOrEmpty(cikti))
                {
                    process.Start();
                    cikti = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    process.Close();
                }
            }
            public string StandartCikis
            {
                get
                {
                    CiktiAl();
                    return cikti;
                }
            }
        }
    }
}
