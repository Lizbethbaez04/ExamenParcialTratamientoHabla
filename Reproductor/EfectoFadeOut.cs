using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace Reproductor
{
    class EfectoFadeOut : ISampleProvider
    {
        private ISampleProvider fuente;
        private int muestrasLeidas = 0;
        private float segundosTranscurridos = 0;
        private float duracion;

        public EfectoFadeOut(ISampleProvider fuente,    
            float duracion)
        {
            this.fuente = fuente;
            this.duracion = duracion;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return fuente.WaveFormat;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int read = fuente.Read(buffer, offset, count);


            muestrasLeidas += read;
            segundosTranscurridos =
                (float)muestrasLeidas /
                (float)fuente.WaveFormat.SampleRate /
                (float)fuente.WaveFormat.Channels;

            if (//segundosTranscurridos >=  &&
                segundosTranscurridos <= duracion)
            {
                //Aplicar el efecto
                float factorEscala =
                    1 - (segundosTranscurridos /
                        duracion);
                for (int i = 0; i < read; i++)
                {
                    buffer[i + offset] *=
                        factorEscala;
                }
            } else if (segundosTranscurridos >=  duracion)
            {
                for (int i = 0; i < read; i++)
                {
                    buffer[i + offset] = 0.0f;
                }
            }
                return read;
        }
    }
}
