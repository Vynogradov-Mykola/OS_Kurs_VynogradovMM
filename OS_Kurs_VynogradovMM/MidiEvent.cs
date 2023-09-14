namespace OS_Kurs_VynogradovMM
{
    public class MidiEvent
    {
        public int DeltaTime { get; set; }
        public byte StatusByte { get; set; }
        public byte[] Data { get; set; }
    }
}
