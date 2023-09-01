using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Kurs_VynogradovMM
{
    public class MidiEvent
    {
        public int DeltaTime { get; set; }
        public byte StatusByte { get; set; }
        public byte[] Data { get; set; }
    }
}
