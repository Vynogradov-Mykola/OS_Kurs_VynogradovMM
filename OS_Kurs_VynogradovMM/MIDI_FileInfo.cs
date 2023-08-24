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
    public partial class MIDI_FileInfo : Form
    {
        public MIDI_FileInfo(FileList file)
        {
            InitializeComponent();
            PathBox.Text = file.getPath();
            NameBox.Text = file.getName();

        }
    }
}
