﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ModelAbstractApi
    {
        public abstract class ModelAbstractApi
        {
            public abstract Kulka tworzKule();
            public abstract void Move(object state);
            public abstract void Start();
            public abstract void Stop();


            public static ModelAbstractApi CreateApi()
            {
                return new ModelApi();
            }
        }

        internal class ModelApi : ModelAbstractApi, IDisposable
        {
            private LogikaAbstractApi LogikaApi;
            private List<Kulka> kulki;
            private Timer timer;

            public ModelApi()
            {
                this.LogikaApi = LogikaAbstractApi.CreateApi();
                this.kulki = new List<Kulka>();
            }

            public void Dispose()
            {
                timer.Dispose();
            }

            public override void Start()
            {
                System.Diagnostics.Trace.WriteLine("Jestem w start");
                timer = new Timer(Move, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
            }

            public override void Stop()
            {
                this.Dispose();
            }

            public override void Move(object state)
            {
                LogikaApi.poruszajKulkami();
                System.Diagnostics.Trace.WriteLine("Kulki sie ruszaja");
                for (int i = 0; i < kulki.Count; i++)
                {
                    kulki[i].ruszKulka(LogikaApi.zwrocXkulki(i), LogikaApi.zwrocYkulki(i));
                }
            }

            public override Kulka tworzKule()
            {
                LogikaApi.tworzKule();
                kulki.Add(new Kulka(10, LogikaApi.zwrocXkulki(LogikaApi.ileKulek() - 1), LogikaApi.zwrocYkulki(LogikaApi.ileKulek() - 1)));
                if (kulki.Count > 0)
                {
                    return kulki[kulki.Count - 1];
                }
                return kulki[0];
            }
        }
    }
}
