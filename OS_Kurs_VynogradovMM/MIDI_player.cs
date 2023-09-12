using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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
                string FileName = selectedFilePath.Substring(selectedFilePath.LastIndexOf(@"\") + 1); //only name of file
                FileName = FileName.Remove(FileName.Length - 4);    //remove .mid  text
                FileList FileNamePath = new FileList(selectedFilePath, FileName);
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
               
                string midiFilePath = ListFile[Files.SelectedIndex].getPath();
                FileStream fileStream = new FileStream(ListFile[Files.SelectedIndex].getPath(), FileMode.Open, FileAccess.Read);
                MIDIReaderFile MIDIFile = new MIDIReaderFile(fileStream);
                fileStream.Close();
                midiEvents = MIDIFile.ReadMidiFile(ListFile[Files.SelectedIndex].getPath());
                
                try
                {
                    MIDI_FileInfo FileInformation = new MIDI_FileInfo(ListFile[Files.SelectedIndex],midiEvents);
                    FileInformation.ShowDialog();
                    midiEvents = FileInformation.Events;

                  //for working check, because we don`t save to file
                  //  Console.WriteLine(midiEvents[1].DeltaTime.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
               
            }
            
        }
        [DllImport("winmm.dll")]
        private static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, ref WaveFormat lpFormat, IntPtr dwCallback, IntPtr dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        private static extern int waveOutPrepareHeader(IntPtr hWaveOut, IntPtr lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern int waveOutWrite(IntPtr hWaveOut, IntPtr lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern int waveOutUnprepareHeader(IntPtr hWaveOut, IntPtr lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern int waveOutClose(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        private static extern int waveOutGetPosition(IntPtr hWaveOut, out MCI_STATUS_PARMS lpStatus, int uSize);
        [DllImport("winmm.dll")]
        private static extern int waveOutReset(IntPtr hWaveOut);
 
        private struct MCI_STATUS_PARMS
        {
            public IntPtr dwCallback;
            public int dwReturn;
            public int dwItem;
            public int dwTrack;
        }
        private const int CALLBACK_NULL = 0;
        private const int WAVE_MAPPER = -1;
        private const int MMSYSERR_NOERROR = 0;
        
        private struct WaveFormat
        {
            public short wFormatTag;
            public short nChannels;
            public int nSamplesPerSec;
            public int nAvgBytesPerSec;
            public short nBlockAlign;
            public short wBitsPerSample;
            public short cbSize;
        }
        
        private static WaveFormat CreateWaveFormat(int sampleRate, int channels)
        {
            return new WaveFormat
            {
                wFormatTag = 1, // WAVE_FORMAT_PCM
                nChannels = (short)channels,
                nSamplesPerSec = sampleRate,
                nAvgBytesPerSec = sampleRate * channels * sizeof(short),
                nBlockAlign = (short)(channels * sizeof(short)),
                wBitsPerSample = sizeof(short) * 8,
                cbSize = 0
            };
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            string midiFilePath = ListFile[Files.SelectedIndex].getPath();

            try
            {
                FileStream fileStream = new FileStream(ListFile[Files.SelectedIndex].getPath(), FileMode.Open, FileAccess.Read);
                MIDIReaderFile MIDIFile = new MIDIReaderFile(fileStream);
                fileStream.Close();
                midiEvents = MIDIFile.ReadMidiFile(midiFilePath);
                
                PlayMidiEvents(44100,label1,label2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        
      
        struct WAVEHDR
        {
            public IntPtr lpData;
            public int dwBufferLength;
            public int dwBytesRecorded;
            public IntPtr dwUser;
            public int dwFlags;
            public int dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        }
       
       
        public int PlayChecker=0;
        public static short[] shortBuffer;
        public int PausePosition = 0;
        public int PauseChecker = 0;
        public double Speed = 1;
        List<MidiEvent> midiEvents;
    
        public void PlayMidiEvents(int sampleRate, Label label1, Label label2)
        {
            FileStream fileStream = new FileStream(ListFile[Files.SelectedIndex].getPath(), FileMode.Open, FileAccess.Read); 
            MIDIReaderFile MIDIFile = new MIDIReaderFile(fileStream);
            int ticksPerQuarterNote = MIDIFile.ReadUInt16BigEndian();
            Console.WriteLine("Параметры времени: " + ticksPerQuarterNote.ToString() + "\n");
  

            
            fileStream.Close();
            Speed = double.Parse(SpeedLabChange.Text);
            MidiSynthesizer synthesizer = new MidiSynthesizer(sampleRate, ticksPerQuarterNote);
            List<float> audioBuffer = new List<float>();
            PauseChecker = 0;
            if (PlayChecker == 0)
            {
                foreach (MidiEvent midiEvent in midiEvents)
                {
                    // Обработка MIDI-события вашим синтезатором
                    synthesizer.ProcessEvent(midiEvent);

                    // Генерация аудио сигнала и добавление в буфер
                    // Calculate the duration in seconds based on the event's delta time
                    double deltaTimeInSeconds = (double)midiEvent.DeltaTime / synthesizer.TicksPerQuarterNote;
                    int numSamples = (int)Math.Round(((deltaTimeInSeconds * sampleRate)/(Speed)/4));
                   
                    float[] samples = synthesizer.GenerateAudioBuffer(numSamples);
                    audioBuffer.AddRange(samples);
                    samples = null;
                }
                PlayChecker = 1;
                synthesizer.em();
                synthesizer = null;
                PlayerBar.Maximum = audioBuffer.Count;
                shortBuffer = audioBuffer.Select(sample => (short)(sample * short.MaxValue)).ToArray();
                PlayAudioBufferAsync(shortBuffer, sampleRate, 1, label1, label2);
                shortBuffer = new short[1];
                audioBuffer.Clear();
                midiEvents.Clear();
               
            }
        }
       
        public async Task PlayAudioBufferAsync(short[] shortBuffer, int sampleRate, int channels,Label label1, Label label2)
        {
            shortBuffer =shortBuffer.Skip(PausePosition).ToArray();
            IntPtr hWaveOut = IntPtr.Zero;
            WaveFormat waveFormat = CreateWaveFormat(sampleRate, channels);
           
            int result = waveOutOpen(out hWaveOut, WAVE_MAPPER, ref waveFormat, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL);
            if (result != MMSYSERR_NOERROR)
            {
                Console.WriteLine($"Ошибка при открытии устройства воспроизведения: {result}");
                return;
            }

            GCHandle hBuffer = GCHandle.Alloc(shortBuffer, GCHandleType.Pinned);
            IntPtr pBuffer = hBuffer.AddrOfPinnedObject();
            
            IntPtr hWaveOutHdr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WAVEHDR)));
            WAVEHDR waveHdr = new WAVEHDR
            {
                lpData = pBuffer,
                dwBufferLength = shortBuffer.Length,
                dwFlags = 0,
                dwUser = IntPtr.Zero,
                dwLoops = 0,
                lpNext = IntPtr.Zero,
                reserved = IntPtr.Zero
            };

            Marshal.StructureToPtr(waveHdr, hWaveOutHdr, false);
            result = waveOutPrepareHeader(hWaveOut, hWaveOutHdr, Marshal.SizeOf(typeof(WAVEHDR)));
            if (result != MMSYSERR_NOERROR)
            {
                Console.WriteLine($"Ошибка при подготовке буфера воспроизведения: {result}");
                waveOutClose(hWaveOut);
                Marshal.FreeHGlobal(hWaveOutHdr);
                hBuffer.Free();
                return;
            }

            result = waveOutWrite(hWaveOut, hWaveOutHdr, Marshal.SizeOf(typeof(WAVEHDR)));
            if (result != MMSYSERR_NOERROR)
            {
                Console.WriteLine($"Ошибка при начале воспроизведения: {result}");
                waveOutUnprepareHeader(hWaveOut, hWaveOutHdr, Marshal.SizeOf(typeof(WAVEHDR)));
                waveOutClose(hWaveOut);
                Marshal.FreeHGlobal(hWaveOutHdr);
                hBuffer.Free();
                return;
            }

            MCI_STATUS_PARMS statusParms = new MCI_STATUS_PARMS();
            statusParms.dwItem = 7; 
            waveOutGetPosition(hWaveOut, out statusParms, Marshal.SizeOf(statusParms));

            int currentPosition = statusParms.dwReturn + PausePosition;
            label2.Invoke((MethodInvoker)(() =>
            {
                label2.Text = (shortBuffer.Length + PausePosition).ToString();  //for easier debug
            }));
            while (currentPosition < shortBuffer.Length-1 + PausePosition)
            {
                
                    waveOutGetPosition(hWaveOut, out statusParms, Marshal.SizeOf(statusParms));
                    currentPosition = statusParms.dwReturn + PausePosition;
                    PlayerBar.Value = currentPosition;
                    label1.Invoke((MethodInvoker)(() =>
                    {
                        label1.Text = (currentPosition).ToString();  //for easier debug
                    }));
                    if (PauseChecker == 1)
                    {
                        PausePosition = currentPosition;
                        result = waveOutReset(hWaveOut);
                        if (result != MMSYSERR_NOERROR)
                        {
                            Console.WriteLine($"Ошибка при остановке воспроизведения: {result}");
                        }
                        break;
                    }
                    if (currentPosition < (shortBuffer.Length + PausePosition) - 1)
                    {
                        await Task.Delay(10);
                    }
            }
               
            

            result = waveOutUnprepareHeader(hWaveOut, hWaveOutHdr, Marshal.SizeOf(typeof(WAVEHDR)));
            hWaveOut = IntPtr.Zero;
            Marshal.FreeHGlobal(hWaveOutHdr);
       
            if (result != MMSYSERR_NOERROR)
            {
                Console.WriteLine($"Ошибка при освобождении буфера воспроизведения: {result}");
            }
          
            waveOutClose(hWaveOut);
            hBuffer.Free();
            hBuffer = default(GCHandle);
            PlayChecker = 0;
           if(PauseChecker==0) PausePosition = 0;
            shortBuffer = null;
         
        }
        
        private void PauseBtn_Click(object sender, EventArgs e)
        {
            PauseChecker = 1;
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            PauseChecker = 0;
            PausePosition = 0;
        }

        private void AddSpeed_Click(object sender, EventArgs e)
        {
            Speed += 0.5;
            SpeedLabChange.Text = Speed.ToString();
        }

        private void SlowSpeed_Click(object sender, EventArgs e)
        {
            if (Speed > 0.5) Speed -= 0.5;
            SpeedLabChange.Text = Speed.ToString();

        }
    }

   


}