using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
