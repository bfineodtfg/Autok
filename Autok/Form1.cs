using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GenerateWelcomeScreen();
            Start();
        }
        public dbHandler mydb;
        int selectedID = -1;
        ListBox results = new ListBox();
        List<Button> buttons = new List<Button>();
        void Start()
        {
            bool euro = true;
            mydb = new dbHandler();
            Timer start = new Timer();
            start.Interval = 1500;
            start.Tick += (s, e) => {
                start.Stop();
                this.Controls.Clear();
                this.Height = 500;
                this.Width = 800;
                makeListbox();
                makeButton("Betölt");
                buttons[0].Click += (ss, ee) => {
                    LoadData();
                    buttons[1].Enabled = true;
                };
                makeButton("Hozzáad");
                buttons[1].Enabled = false;
                buttons[1].Click += (ss, ee) => {
                    editor newWindow = new editor(this);
                    this.Visible = false;
                };
                makeButton("Szerkeszt");
                buttons[2].Enabled = false;
                buttons[2].Click += (ss, ee) => {
                    editor newWindow = new editor(this, Autok.all[selectedID]);
                    this.Visible = false;
                };
                makeButton("Töröl");
                buttons[3].Enabled = false;
                buttons[3].Click += (ss, ee) =>
                {
                    mydb.deleteCar(Autok.all[results.SelectedIndex].Rendszam);
                    Autok.all.RemoveAt(results.SelectedIndex);
                    LoadData();
                };
                makeButton("Euró");
                buttons[4].Click += (ss, ee) =>
                {
                    euro = makeEuro(euro);
                    LoadData();
                };
                makeButton("Kilép");
                buttons[5].Click += (ss, ee) => {
                    Application.Exit();
                };
                mydb.ReadDb();
                
            };
            start.Start();
        }
        bool makeEuro(bool euro) {
            Autok.all.ForEach(x => x.isEuro = euro);
            return !euro;
        }
        public void LoadData() {
            results.Items.Clear();
            foreach (Autok item in Autok.all)
            {
                results.Items.Add(item.ToString());
            }
        }
        void makeListbox()
        {
            results.SelectedIndexChanged += (s, e) => {
                selectedID = results.SelectedIndex;
                if (selectedID < 0)
                {
                    buttons[2].Enabled = false;
                    buttons[3].Enabled = false;
                }
                else
                {
                    buttons[2].Enabled = true;
                    buttons[3].Enabled = true;
                }
            };
            Controls.Add(results);
            results.Location = new Point(50, 30);
            results.Width = 600;
            results.Height = 400;
        }
        void makeButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            Controls.Add(button);
            button.Location = new Point(680, 50 + buttons.Count * 40);
            buttons.Add(button);
        }



    }
}
