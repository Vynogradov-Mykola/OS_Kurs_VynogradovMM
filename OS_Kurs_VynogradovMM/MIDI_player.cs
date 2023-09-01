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
                MIDI_FileInfo FileInformation = new MIDI_FileInfo(ListFile[Files.SelectedIndex]);
                FileInformation.Show();
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
        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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
                List<MidiEvent> midiEvents = ReadMidiFile(midiFilePath);
                PlayMidiEvents(midiEvents, 44100,label1,label2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        static List<MidiEvent> ReadMidiFile(string filePath)
        {
            List<MidiEvent> midiEvents = new List<MidiEvent>();

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    // Чтение заголовка MIDI файла и переход к первому треку

                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        int deltaTime = ReadVariableLengthValue(binaryReader);
                        byte statusByte = binaryReader.ReadByte();
                        byte[] eventData = null; // Здесь будут храниться дополнительные байты события

                        // Чтение дополнительных байт в зависимости от статусного байта
                        // и создание объекта MidiEvent
                        if (statusByte == 0xFF) // Мета-событие
                        {
                            byte metaEventType = binaryReader.ReadByte();
                            int metaEventLength = ReadVariableLengthValue(binaryReader);
                            eventData = binaryReader.ReadBytes(metaEventLength);
                        }
                        else // Событие канала
                        {
                            byte channel = (byte)(statusByte & 0x0F);
                            byte eventType = (byte)(statusByte >> 4);

                            switch (eventType)
                            {
                                case 0x8: // Note Off
                                case 0x9: // Note On
                                case 0xA: // Note Aftertouch
                                case 0xB: // Controller
                                case 0xC: // Program Change
                                case 0xD: // Channel Aftertouch
                                case 0xE: // Pitch Bend
                                          // В зависимости от типа события, читайте дополнительные байты
                                    int eventDataLength = GetEventDataLength(eventType);
                                    eventData = binaryReader.ReadBytes(eventDataLength);
                                    break;
                                    // ... добавьте другие случаи
                            }
                        }
                        midiEvents.Add(new MidiEvent
                        {
                            DeltaTime = deltaTime,
                            StatusByte = statusByte,
                            Data = eventData // Здесь data - массив дополнительных байт
                        });
                    }
                }
            }

            return midiEvents;
        }
        private static int GetEventDataLength(byte eventType)
        {
            switch (eventType)
            {
                case 0x8: // Note Off
                case 0x9: // Note On
                case 0xA: // Note Aftertouch
                case 0xB: // Controller
                case 0xE: // Pitch Bend
                    return 2;
                case 0xC: // Program Change
                case 0xD: // Channel Aftertouch
                    return 1;
                // Add more cases for other event types
                // For example:
                // case 0x2: // Start of Sequence
                // case 0x3: // End of Sequence
                // ...
                default:
                    return 0; // Unknown event type or no additional data
            }
        }
        [StructLayout(LayoutKind.Sequential)]
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
        private struct MMTIME
        {
            public int wType;
            public int u;
        }
        static int ReadVariableLengthValue(BinaryReader reader)
        {
            int value = 0;
            byte nextByte;

            do
            {
                nextByte = reader.ReadByte();
                value = (value << 7) | (nextByte & 0x7F);
            } while ((nextByte & 0x80) != 0);

            return value;
        }
        public int PlayChecker=0;
        public void PlayMidiEvents(List<MidiEvent> midiEvents, int sampleRate, Label label1, Label label2)
        {
            int ticksPerQuarterNote = 480; // Set this value based on the MIDI file's time division
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

                    int numSamples = (int)Math.Round(deltaTimeInSeconds * sampleRate);

                    float[] samples = synthesizer.GenerateAudioBuffer(numSamples);

                    audioBuffer.AddRange(samples);
                    samples = null;
                }
                PlayChecker = 1;
                synthesizer.em();
                synthesizer = null;
                PlayerBar.Maximum = audioBuffer.Count;
                shortBuffer = audioBuffer.Select(sample => (short)(sample * short.MaxValue)).ToArray();
              //  shortBuffer=shortBuffer.Skip(2).ToArray();
                PlayAudioBufferAsync(shortBuffer, sampleRate, 1, label1, label2);
                audioBuffer.Clear();
                midiEvents.Clear();
            }
        }
       public static short[] shortBuffer;

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
                dwBufferLength = shortBuffer.Length,// * sizeof(short),
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
            statusParms.dwItem = 7; // MCI_STATUS_POSITION
            waveOutGetPosition(hWaveOut, out statusParms, Marshal.SizeOf(statusParms));

            int currentPosition = statusParms.dwReturn + PausePosition;
            label2.Invoke((MethodInvoker)(() =>
            {
                label2.Text = (shortBuffer.Length + PausePosition).ToString();
            }));
            while (currentPosition < shortBuffer.Length-1 + PausePosition)
            {
                
                    waveOutGetPosition(hWaveOut, out statusParms, Marshal.SizeOf(statusParms));
                    currentPosition = statusParms.dwReturn + PausePosition;
                    PlayerBar.Value = currentPosition;
                    label1.Invoke((MethodInvoker)(() =>
                    {
                        label1.Text = (currentPosition).ToString();
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
        public int PausePosition = 0;
        public int PauseChecker=0;
        private void PauseBtn_Click(object sender, EventArgs e)
        {
            PauseChecker = 1;
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            PauseChecker = 0;
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
    public class MidiEvent
    {
        public int DeltaTime { get; set; }
        public byte StatusByte { get; set; }
        public byte[] Data { get; set; }
    }
    class MidiSynthesizer
    {
        private int sampleRate;
        private List<NoteState> noteStates;
      public int TicksPerQuarterNote { get; private set; }
     public void em()
        {
            noteStates.Clear();
        }
        
        
                public MidiSynthesizer(int sampleRate, int ticksPerQuarterNote)
                {
                    this.sampleRate = sampleRate;
                    TicksPerQuarterNote = ticksPerQuarterNote; // Set the TicksPerQuarterNote value

                    noteStates = new List<NoteState>();
                }
       
        public void ProcessEvent(MidiEvent midiEvent)
        {
            byte statusByte = midiEvent.StatusByte;

            if ((statusByte & 0xF0) == 0x90) // Note On
            {
                byte noteNumber = midiEvent.Data[0];
                byte velocity = midiEvent.Data[1];

                double frequency = CalculateFrequency(noteNumber);
                double amplitude = velocity / 127.0;

                // Проверка, есть ли уже состояние для этой ноты
                NoteState existingNote = noteStates.FirstOrDefault(ns => ns.Frequency == frequency);

                if (existingNote != null)
                {
                    existingNote.Amplitude = amplitude;
                    existingNote.IsPlaying = true;
                }
                else
                {
                    NoteState noteState = new NoteState
                    {
                        Frequency = frequency,
                        Amplitude = amplitude,
                        Phase = 0.0,
                        IsPlaying = true
                    };
                    noteStates.Add(noteState);
                }
            }
            else if ((statusByte & 0xF0) == 0x80) // Note Off
            {
                byte noteNumber = midiEvent.Data[0];

                // Остановка генерации для соответствующей ноты
                foreach (var noteState in noteStates)
                {
                    if (noteState.Frequency == CalculateFrequency(noteNumber))
                    {
                        noteState.IsPlaying = false;
                        break; // Найдена соответствующая нота, можно завершить поиск
                    }
                }

            }
            // Добавьте обработку других типов MIDI-событий по аналогии
        }
        
        public float[] GenerateAudioBuffer(int numSamples)
        {
            float[] audioBuffer = new float[numSamples];

            for (int i = 0; i < numSamples; i++)
            {
                double sample = 0.0;

                foreach (var noteState in noteStates)
                {
                    if (noteState.IsPlaying)
                    {
                        noteState.Phase += noteState.Frequency * 2 * Math.PI / sampleRate;
                        if (noteState.Phase > 2 * Math.PI)
                        {
                            noteState.Phase -= 2 * Math.PI;
                        }

                        sample += (float)(noteState.Amplitude * Math.Sin(noteState.Phase));
                    }
                }

                audioBuffer[i] = (float)sample;
            }

            return audioBuffer;
        }


        private double CalculateFrequency(byte noteNumber)
        {
            return 440.0 * Math.Pow(2.0, (noteNumber - 69.0) / 12.0);
        }

        private class NoteState
        {
            public double Frequency { get; set; }
            public double Amplitude { get; set; }
            public double Phase { get; set; }
            public bool IsPlaying { get; set; }
        }
    }


}