using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    public class editor : Form
    {
        List<int> previousValues = new List<int>();
        List<TextBox> inputs = new List<TextBox>();
        Form1 data;
        string originalLicensplate;
        Autok auto = null; 
        public editor(Form1 data, Autok auto = null)
        {
            this.data = data;
            this.auto = auto;
            Start();
            LoadData();
            this.Height = 500;
            this.FormClosed += (s, e) => {
                data.Visible = true;
            };
            this.Show();
        }
        void Start()
        {
            makeTextBox("Rendszám");
            addConstraint(inputs[0], 7, false);

            makeTextBox("Márka");
            addConstraint(inputs[1], 50, false);

            makeTextBox("Modell");
            addConstraint(inputs[2], 50, false);

            makeTextBox("Gyártási év");
            addConstraint(inputs[3], 4, true, true);

            makeTextBox("Forgalmi érvényessége");

            makeTextBox("Vételár");
            addConstraint(inputs[5], 11);

            makeTextBox("km óra állás");
            addConstraint(inputs[6], 11);

            makeTextBox("Hengerűrtartalom");
            addConstraint(inputs[7], 11);

            makeTextBox("Tömeg");
            addConstraint(inputs[8], 11);

            makeTextBox("Teljesítmény");
            addConstraint(inputs[9], 11);

            makeAddButton();
        }
        void LoadData()
        {
            if (auto != null)
            {
                /*originalLicensplate = auto.Rendszam;
                inputs[0].Text = originalLicensplate;
                inputs[1].Text = auto.Make;
                inputs[2].Text = auto.Model;
                inputs[3].Text = auto.Year.ToString();
                if (auto.Registration.Length > 12)
                {
                    inputs[4].Text = auto.Registration.ToString().Substring(0, 12).Replace(" ", "");
                }
                else
                {
                    inputs[4].Text = auto.Registration;
                }
                inputs[5].Text = auto.Price.ToString();
                inputs[6].Text = auto.KM.ToString();
                inputs[7].Text = auto.Displacement.ToString();
                inputs[8].Text = auto.Weight.ToString();
                inputs[9].Text = auto.Power.ToString();*/
                ADD.Text = "Módosítás";
                ADD.Click += (s, e) => {
                    updateAuto();
                    data.mydb.updateCar(auto, originalLicensplate);
                };
            }
        }
        void updateAuto() {
            auto.Update(inputs[0].Text, inputs[1].Text, inputs[2].Text, int.Parse(inputs[3].Text.Trim()), inputs[4].Text, int.Parse(inputs[5].Text.Trim()), int.Parse(inputs[6].Text), int.Parse(inputs[7].Text.Trim()), int.Parse(inputs[8].Text.Trim()), Convert.ToInt32(inputs[9].Text.Trim()));
            data.LoadData();
        }
        void addConstraint(TextBox input, int length = -1, bool number = true, bool year = false)
        {
            int num = previousValues.Count;
            previousValues.Add(0);
            input.TextChanged += (s, e) => {
                int power = 0;
                if (length > 0 && input.Text.Length > length)
                {
                    input.Text = input.Text.Substring(0, length);
                }
                if (number)
                {
                    if (!int.TryParse(input.Text, out power))
                    {
                        input.Text = previousValues[num].ToString();
                    }
                    else
                    {
                        previousValues[num] = power;
                    }
                }
                if (year && input.Text.Length > 4 && (power > DateTime.UtcNow.Year || power < 1888))
                {
                    MessageBox.Show("Hibás év");
                }
            };

        }
        void makeTextBox(string text)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Text = text;
            label.Location = new Point(20, 23 + inputs.Count * 40);


            TextBox box = new TextBox();
            box.Location = new Point(label.Width + 35, 20 + inputs.Count * 40);

            inputs.Add(box);

            Controls.Add(box);
            Controls.Add(label);

        }
        Button ADD = new Button();
        void makeNewAuto() {
            auto = new Autok();
        }
        void makeAddButton()
        {
            
            ADD.Text = "Hozzáadás";
            Controls.Add(ADD);
            ADD.Location = new Point(90, 20 + inputs.Count * 40);
            ADD.Click += (s, e) => {
                makeNewAuto();
                updateAuto();
                data.mydb.InsertCar(auto);
                this.Close();
            };
        }
    }
}
