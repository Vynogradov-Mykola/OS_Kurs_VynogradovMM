using System;
using System.Collections.Generic;
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
            byte statusByte = Events[0].StatusByte;
            if ((statusByte & 0xF0) == 0x90) MIDI.Items.Add("Note Number: " + Events[0].Data[0]);


        }

        private void RigthBtn_Click(object sender, EventArgs e)
        {
            Counter++;
            MIDI.Items.Clear();
            MIDI.Items.Add("ID: " + Counter + " Time: " + Events[Counter].DeltaTime.ToString() + " StatusByte: " + Events[Counter].StatusByte);
            byte statusByte = Events[Counter].StatusByte;
            if ((statusByte & 0xF0) == 0x90) MIDI.Items.Add("Note Number: " + Events[Counter].Data[0]);

        }

        private void LeftBtn_Click(object sender, EventArgs e)
        {
            if (Counter > 0)
            {
                Counter--;
                MIDI.Items.Clear();
                MIDI.Items.Add("ID: " + Counter + " Time: " + Events[Counter].DeltaTime.ToString() + " StatusByte: " + Events[Counter].StatusByte);
                byte statusByte = Events[Counter].StatusByte;
                if ((statusByte & 0xF0) == 0x90) MIDI.Items.Add("Note Number: " + Events[Counter].Data[0]);

            }
        }

        private void ChangeInfoBtn_Click(object sender, EventArgs e)
        {
            Events[Counter].DeltaTime = Int32.Parse(DeltaTimeText.Text);
            byte statusByte = Events[Counter].StatusByte;
            if ((statusByte & 0xF0) == 0x90)
            {
                string g = NoteChange.Text;
                if (byte.TryParse(g, out byte byteValue))
                {
                    Events[Counter].Data[0] = byteValue;
                }
                else
                {
                    MessageBox.Show("Can`t convert into byte.");
                }
             
            }
        }
    }
}
