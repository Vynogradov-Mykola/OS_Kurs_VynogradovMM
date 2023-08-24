using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Kurs_VynogradovMM
{
    
    public partial class MIDI_player : Form
    {
        public List<FileList> ListFile = new List<FileList>();
        public MIDI_player()
        {
            InitializeComponent();
        }

        private void AddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Виберіть файл";
            openFileDialog.Filter = "MIDI файли(*.mid) | *.mid";
            openFileDialog.InitialDirectory = @"C:\YourDefaultFolderPath";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;  //path to file
                string FileName = selectedFilePath.Substring(selectedFilePath.LastIndexOf(@"\")+1); //only name of file
                FileName = FileName.Remove(FileName.Length - 4);    //remove .mid  text
                FileList FileNamePath=new FileList(selectedFilePath, FileName);
                ListFile.Add(FileNamePath);
                Files.Items.Add(FileName);
            }
            
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (Files.SelectedIndex != -1)
            {
                int remove = Files.SelectedIndex;
                ListFile.RemoveAt(remove);
                Files.Items.RemoveAt(remove);
            }
        }

        private void Files_DoubleClick(object sender, EventArgs e)
        {
            if (Files.SelectedIndex != -1)
            {
                MIDI_FileInfo FileInformation =new MIDI_FileInfo(ListFile[Files.SelectedIndex]);
                FileInformation.Show();
            }
        }
    }
    public class FileList
    {
        string Path;
        string Name;
        public FileList(string path, string name)
        {
            Path = path;
            Name = name;
        }
        public string getPath() { return Path; }
        public string getName() { return Name; }
    }
}
