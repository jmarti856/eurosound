
namespace CustomStatusBar
{
    partial class StatusBarToolTips
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.StatusPanel = new System.Windows.Forms.StatusBarPanel();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusPanel
            // 
            this.StatusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.StatusPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Text = "Ready";
            // 
            // StatusBarToolTips
            // 
            this.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusPanel});
            this.ShowPanels = true;
            ((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBarPanel StatusPanel;
    }
}
