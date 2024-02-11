using System.Windows.Forms;
using System.Drawing;

namespace Autok
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.Text = "Autók feladat. Készítette: Zsár Dániel - 2024. Január";
        }

        void GenerateWelcomeScreen()
        {
            Label welcome = new Label();
            welcome.Location = new Point(20, 100);
            welcome.Text = "Üdvözöllek az autók adatbázis kezelő alkalmazásban.\nPillanatokon belül megnyílik az alkalmazás";
            welcome.TextAlign = ContentAlignment.MiddleCenter;
            welcome.AutoSize = true;
            welcome.Font = new Font("Times New Roman", 25);
            this.Controls.Add(welcome);
        }

        #endregion
    }
}

