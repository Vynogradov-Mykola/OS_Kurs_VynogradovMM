using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Kurs_VynogradovMM
{
    public partial class MIDI_FileInfo : Form
    {
        public List<MidiEvent> Events;
        int Counter;
        public MIDI_FileInfo(FileList file,List<MidiEvent> midiEvents)
        {
            InitializeComponent();
            Events = midiEvents;
            Counter = 0;
            PathBox.Text = file.getPath();
            NameBox.Text = file.getName();
            MIDI.Items.Add("ID: 0" + " Time: " + Events[0].DeltaTime.ToString() + " StatusByte: " + Events[0].StatusByte);
            
            
        }

        private void RigthBtn_Click(object sender, EventArgs e)
        {
            Counter++;
            MIDI.Items.Clear();
            MIDI.Items.Add("ID: " + Counter + " Time: " + Events[Counter].DeltaTime.ToString() + " StatusByte: " + Events[Counter].StatusByte);

        }

        private void LeftBtn_Click(object sender, EventArgs e)
        {
            if (Counter > 0)
            {
                Counter--;
                MIDI.Items.Clear();
                MIDI.Items.Add("ID: " + Counter + " Time: " + Events[Counter].DeltaTime.ToString() + " StatusByte: " + Events[Counter].StatusByte);

            }
        }

        private void ChangeInfoBtn_Click(object sender, EventArgs e)
        { 
            Events[Counter].DeltaTime = Int32.Parse(DeltaTimeText.Text);
        }
    }
}
