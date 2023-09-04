using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Kurs_VynogradovMM
{
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
            TicksPerQuarterNote = ticksPerQuarterNote; 

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
            else if ((statusByte & 0xF0) == 0xA0) // Note Aftertouch
            {
                byte noteNumber = midiEvent.Data[0];
                byte aftertouchValue = midiEvent.Data[1];
            }
            else if ((statusByte & 0xF0) == 0xB0) // Controller
            {
                byte controllerNumber = midiEvent.Data[0];
                byte controllerValue = midiEvent.Data[1];
            }
            else if ((statusByte & 0xF0) == 0xC0) // Program Change
            {
                byte programNumber = midiEvent.Data[0];
            }
            else if ((statusByte & 0xF0) == 0xD0) // Channel Aftertouch
            {
                byte aftertouchValue = midiEvent.Data[0];
            }
            else if ((statusByte & 0xF0) == 0xE0) // Pitch Bend
            {
                byte lsb = midiEvent.Data[0];
                byte msb = midiEvent.Data[1];
                int pitchBendValue = (msb << 7) | lsb;
            }
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
            //A4 (440 Гц) имеет номер 69.
            //Math.Pow(2.0, (noteNumber - 69.0) / 12.0): степень 2, так как 
            //каждый полутон в музыкальной октаве в два раза выше предыдущего. 
            //Деление на 12 - количество полутонов между эталонной A4 и noteNumber.
            //440.0 * xxx: Масштабирование результата на частоту эталонной ноты A4 (440 Гц).
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
