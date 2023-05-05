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
using System.Runtime.InteropServices; // Para estados pendientes

public enum ServiceState // Valores del Estado del Servicio
{
    SERVICE_STOPPED = 0x00000001,
    SERVICE_START_PENDING = 0x00000002,
    SERVICE_STOP_PENDING = 0x00000003,
    SERVICE_RUNNING = 0x00000004,
    SERVICE_CONTINUE_PENDING = 0x00000005,
    SERVICE_PAUSE_PENDING = 0x00000006,
    SERVICE_PAUSED = 0x00000007,
}
[StructLayout(LayoutKind.Sequential)]
public struct ServiceStatus // Estructura utilizada en una llamada de invocación de plataforma
{
    public int dwServiceType;
    public ServiceState dwCurrentState;
    public int dwControlsAccepted;
    public int dwWin32ExitCode;
    public int dwServiceSpecificExitCode;
    public int dwCheckPoint; // Utilizado por Administrador de servicios
    public int dwWaitHint;   // Utilizado por Administrador de servicios
};


namespace TutorialMicrosoftWinServices
{
    public partial class MiServicioWS : ServiceBase
    {
        public MiServicioWS(string[] args) // args: parámetros de entrada
        {
            InitializeComponent();

            // Parámetros predeterminados
            string eventSourceName = "MiFuenteDefault";
            string logName = "MiNuevoRegistroDefault";

            // Asiganción de parámetros de entrada (si existen)
            if (args.Length > 0)
            {
                eventSourceName = args[0];
            }
            if (args.Length > 1)
            {
                logName = args[1];
            }

            // Crear Registro de Eventos (EventLog)
            RegistroEventos1 = new System.Diagnostics.EventLog();

            // Crear Fuente si aún no existe
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            RegistroEventos1.Source = eventSourceName;
            RegistroEventos1.Log = logName;
        }
        // Identificador de eventos
        private int ID_evento = 1;
        // Método OnTimer: Actividades que se ejecutan al sondear con TempSondeo (cada 60s)
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // Aquí se insertan las actividades de monitoreo
            RegistroEventos1.WriteEntry("Monitoreando el sistema: ", EventLogEntryType.Information, ID_evento++);
        }

        // Función SetServiceStatus para invocación de plataforma
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        protected override void OnStart(string[] args)
        {
            // Actualizar estado del estado a "pendiente corriendo"
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Registro del estado OnStart
            RegistroEventos1.WriteEntry("Estado: OnStart.");

            // Sondeo del sistema
            // Temporizador
            Timer TempSondeo = new Timer();
            TempSondeo.Interval = 60000; //60 segundos
            TempSondeo.Elapsed += new ElapsedEventHandler(this.OnTimer);
            TempSondeo.Start();

            // Actualizar estado del servicio a "corriendo"
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            // Actualizar estado del estado a "pendiente parado"
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Registro del estado OnStop
            RegistroEventos1.WriteEntry("Estado: OnStop.");

            // Actualizar estado del servicio a "parado"
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
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
