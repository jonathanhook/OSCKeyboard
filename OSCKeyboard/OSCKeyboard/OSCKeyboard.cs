using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Common.Osc;

namespace OSCKeyboard
{
    public class OSCKeyboard
    {
        private const int SOURCE_PORT = 8080;
        private const string MESSAGE_FORMAT = "/osck/{0}";
        private const string NOTE = "note";
        private const string BEND = "bend";
        private const string VIBRATO = "vibrato";

        private IPEndPoint source;
        private IPEndPoint dest;

        public OSCKeyboard(IPAddress address, int port)
        {
            source = new IPEndPoint(IPAddress.Loopback, SOURCE_PORT);
            dest = new IPEndPoint(address, port);
        }

        public void KeyDown(int note, int velocity)
        {
            int[] values = new int[2];
            values[0] = note;
            values[1] = velocity;

            SendMessage(NOTE, values);
        }

        public void KeyUp(int note)
        {
            KeyDown(note, 0);
        }

        public void PitchBend(int value)
        {
            int[] values = new int[1];
            values[0] = value;

            SendMessage(BEND, values);
        }

        public void Vibrato(int value)
        {
            int[] values = new int[1];
            values[0] = value;

            SendMessage(VIBRATO, values);
        }

        private void SendMessage(string address, int[] values = null)
        {
            OscMessage msg = new OscMessage(source, string.Format(MESSAGE_FORMAT, address));

            if (values != null)
            {
                foreach (object v in values)
                {
                    msg.Append(v);
                }
            }

            msg.Send(dest);
        }
    }
}
