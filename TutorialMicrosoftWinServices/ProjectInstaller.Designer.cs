namespace TutorialMicrosoftWinServices
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InstaladorProceso1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.Instalador1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // InstaladorProceso1
            // 
            this.InstaladorProceso1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.InstaladorProceso1.Password = null;
            this.InstaladorProceso1.Username = null;
            // 
            // Instalador1
            // 
            this.Instalador1.Description = "Servicio de Windows para practicar";
            this.Instalador1.DisplayName = "Nombre del WinService por Mostrar";
            this.Instalador1.ServiceName = "NombreServicioWS";
            this.Instalador1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.InstaladorProceso1,
            this.Instalador1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller InstaladorProceso1;
        private System.ServiceProcess.ServiceInstaller Instalador1;
    }
}