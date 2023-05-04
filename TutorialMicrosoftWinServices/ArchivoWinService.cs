using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers; // Para sondeo sencillo del sistema

namespace TutorialMicrosoftWinServices
{
    public partial class MiServicioWS : ServiceBase
    {
        public MiServicioWS()
        {
            InitializeComponent();
            // Crear Registro de Eventos (EventLog)
            RegistroEventos1 = new System.Diagnostics.EventLog();
            // Crear Fuente si aún no existe
            if (!System.Diagnostics.EventLog.SourceExists("MiFuente"))
            {
                System.Diagnostics.EventLog.CreateEventSource("MiFuente", "MiNuevoRegistro");
            }
            RegistroEventos1.Source = "MiFuente";
            RegistroEventos1.Log = "MiNuevoRegsitro";

        }
        // Identificador de eventos
        private int ID_evento = 1;
        // Método OnTimer: Actividades que se ejecutan al sondear con TempSondeo (cada 60s)
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // Aquí se insertan las actividades de monitoreo
            RegistroEventos1.WriteEntry("Monitoreando el sistema: ", EventLogEntryType.Information, ID_evento++);
        }

        protected override void OnStart(string[] args)
        {
            // Registro del estado OnStart
            RegistroEventos1.WriteEntry("Estado: OnStart.");

            // Sondeo del sistema
            // Temporizador
            Timer TempSondeo = new Timer();
            TempSondeo.Interval = 60000; //60 segundos
            TempSondeo.Elapsed += new ElapsedEventHandler(this.OnTimer);
            TempSondeo.Start();
        }

        protected override void OnStop()
        {
            // Registro del estado OnStop
            RegistroEventos1.WriteEntry("Estado: OnStop.");
        }
        protected override void OnContinue()
        {
            // Registro del estado OnContinue
            RegistroEventos1.WriteEntry("Estado: OnContinue.");
        }
        protected override void OnPause()
        {
            // Registro del estado OnPause
            RegistroEventos1.WriteEntry("Estado: OnPause.");
        }
        protected override void OnShutdown()
        {
            // Registro del estado OnShutdown
            RegistroEventos1.WriteEntry("Estado: OnShutdown.");
        }
    }
}
