using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autok
{
    public class Autok
    {
        public static List<Autok> all = new List<Autok>();
        private string rendszam;
        private string marka;
        private string modell;
        private int gyartasiev;
        private string forgalmiErvenyesseg;
        private int vetelar;
        private int kmallas;
        private int hengerurtartalom;
        private int tomeg;
        private int teljesitmeny;
        public bool isEuro { get; set; }
        private int euro = 380;
        public string Rendszam { get { return rendszam; } }
        public string Make { get { return marka; } }
        public string Model { get { return modell; } }
        public int Year { get { return gyartasiev; } }
        public string Registration { get { return forgalmiErvenyesseg; } }
        public int Price { get {
                if (isEuro)
                {
                    return vetelar;
                }
                return vetelar * euro; 
            } }
        public int KM { get { return kmallas; } }
        public int Displacement { get { return hengerurtartalom; } }
        public int Weight { get { return tomeg; } }
        public int Power { get { return teljesitmeny; } }
        public static void Error(Autok error)
        {
            all.Remove(error);
        }
        public void Update(string rendszam, string marka, string modell, int gyartasiev, string forgalmiErvenyesseg, int vetelar, int kmallas, int hengerurtartalom, int tomeg, int teljesitmeny) {
            this.rendszam = rendszam;
            this.marka = marka;
            this.modell = modell;
            this.gyartasiev = gyartasiev;
            this.forgalmiErvenyesseg = forgalmiErvenyesseg;
            this.vetelar = vetelar;
            this.kmallas = kmallas;
            this.hengerurtartalom = hengerurtartalom;
            this.tomeg = tomeg;
            this.teljesitmeny = teljesitmeny;
        }
        public Autok(string rendszam, string marka, string modell, int gyartasiev, string forgalmiErvenyesseg, int vetelar, int kmallas, int hengerurtartalom, int tomeg, int teljesitmeny)
        {
            this.rendszam = rendszam;
            this.marka = marka;
            this.modell = modell;
            this.gyartasiev = gyartasiev;
            this.forgalmiErvenyesseg = forgalmiErvenyesseg;
            this.vetelar = vetelar;
            this.kmallas = kmallas;
            this.hengerurtartalom = hengerurtartalom;
            this.tomeg = tomeg;
            this.teljesitmeny = teljesitmeny;
            this.isEuro = true;
            all.Add(this);
        }
        public Autok() {
            this.isEuro = true;
            all.Add(this);
        }
        public Autok(MySqlDataReader data)
        {
            rendszam = data.GetString(data.GetOrdinal("rendszam"));
            marka = data.GetString(data.GetOrdinal("marka"));
            modell = data.GetString(data.GetOrdinal("modell"));
            gyartasiev = data.GetInt32(data.GetOrdinal("gyartasiev"));
            forgalmiErvenyesseg = data.GetDateTime(data.GetOrdinal("forgalmiErvenyesseg")).ToString();
            vetelar = data.GetInt32(data.GetOrdinal("vetelar"));
            kmallas = data.GetInt32(data.GetOrdinal("kmallas"));
            hengerurtartalom = data.GetInt32(data.GetOrdinal("hengerurtartalom"));
            tomeg = data.GetInt32(data.GetOrdinal("tomeg"));
            teljesitmeny = data.GetInt32(data.GetOrdinal("teljesitmeny"));
            this.isEuro = true;
            all.Add(this);
        }
        public override string ToString()
        {
            return $"{rendszam} - {marka} - {modell} - {gyartasiev} - {forgalmiErvenyesseg} - {Price} - {kmallas} - {hengerurtartalom} - {tomeg} - {teljesitmeny}";
        }

    }
}
