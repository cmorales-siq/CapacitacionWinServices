using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMicrosoftWinServices
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {        
        // Especificar argumentos de línea de comandos
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string parameter = "MiFuente1\" \"MiArchivoRegistro1";
            Context.Parameters["assemblypath"] = "\"" +
            Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
            base.OnBeforeInstall(savedState);
        }
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
