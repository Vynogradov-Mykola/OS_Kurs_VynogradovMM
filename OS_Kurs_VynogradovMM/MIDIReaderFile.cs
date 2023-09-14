using System;
using System.Collections.Generic;
using System.IO;

namespace OS_Kurs_VynogradovMM
{
    public class MIDIReaderFile //чтение заголовка /есть только чтение параметра времени
    {
        public BinaryReader BinaryReaderMIDIFile;   // Создаем поток. На его основе будем работать с MIDI файлом.
        public MIDIReaderFile(Stream input) { BinaryReaderMIDIFile = new BinaryReader(input); }
        public UInt16 ReadUInt16BigEndian() // Считываем 2 байта в формате "от старшего к младшему" и располагаем их в переменной.
        {

            UInt16 bufferData = 0;  // Начальное значени = 0.
            UInt32 Sdvig = 0;

            // сдвиг на 12 байтов, до времени
            for (int IndexByte = 11; IndexByte >= 0; IndexByte--) Sdvig = (UInt32)((UInt32)BinaryReaderMIDIFile.ReadByte() << 8 * IndexByte);

            for (int IndexByte = 1; IndexByte >= 0; IndexByte--)    // Счетчик от старшего к младшему.
                bufferData |= (UInt16)((UInt16)BinaryReaderMIDIFile.ReadByte() << 8 * IndexByte);   // Располагаем значения. 
            return bufferData;
        }
        public byte ReadByte() { return BinaryReaderMIDIFile.ReadByte(); }
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
        public List<MidiEvent> ReadMidiFile(string filePath)
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
                            int eventDataLength;
                            switch (eventType)
                            {
                                case 0x8: // Note Off
                                case 0x9: // Note On
                                case 0xA: // Note Aftertouch
                                case 0xB: // Controller
                                case 0xE: // Pitch Bend
                                    eventDataLength = 2;
                                    eventData = binaryReader.ReadBytes(eventDataLength);
                                    break;
                                case 0xC: // Program Change
                                case 0xD: // Channel Aftertouch
                                    eventDataLength = 1;
                                    eventData = binaryReader.ReadBytes(eventDataLength);

                                    break;
                                default:
                                    // По умолчанию считаем, что нет дополнительных данных
                                    eventData = new byte[0];
                                    break;
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
    }


}
