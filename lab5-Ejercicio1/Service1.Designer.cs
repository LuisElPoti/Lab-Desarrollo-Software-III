﻿namespace lab5_Ejercicio1
{
    partial class Service1
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
            this.fswMonitor = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fswMonitor)).BeginInit();
            // 
            // fswMonitor
            // 
            this.fswMonitor.EnableRaisingEvents = true;
            this.fswMonitor.Path = "C:\\Monitor";
            this.fswMonitor.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fswMonitor.Created += new System.IO.FileSystemEventHandler(this.fswMonitor_Created);
            this.fswMonitor.Deleted += new System.IO.FileSystemEventHandler(this.fswMonitor_Deleted);
            this.fswMonitor.Renamed += new System.IO.RenamedEventHandler(this.fswMonitor_Renamed);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.fswMonitor)).EndInit();

        }

        #endregion

        private System.IO.FileSystemWatcher fswMonitor;
    }
}
