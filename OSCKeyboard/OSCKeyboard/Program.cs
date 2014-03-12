using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSCKeyboard
{
    class Program
    {
        static void Main(string[] args)
        {
            OSCKeyboard k = new OSCKeyboard(IPAddress.Parse("169.254.136.244"), 8080);
          

            int bend = 0;
            int vib = 0;
            DateTime last = DateTime.UtcNow;
            bool noteOn = false;

            while (true)
            {
                DateTime now = DateTime.UtcNow;
                TimeSpan elapsed = now - last;

                if (elapsed.Milliseconds > 500)
                {
                    last = now; 

                    if (noteOn)
                    {
                        k.KeyUp(60);
                    }
                    else
                    {
                        k.KeyDown(60, 100);
                    }

                    noteOn = !noteOn;

                    k.PitchBend(bend);
                    bend = (bend + 10) % 127;
                }

                vib = (vib + 1) % 40;
                Thread.Sleep(1);
                //k.Vibrato(110 - vib);
            }
        }
    }
}
