using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class User
    {
        //Vars
        private string name;
        private string famName;
        private int nas;
        private int nbPersoFam;
        private DateTime dateNaiss;
        private char statut;
        private char sexe;
        private double revenue;

        public string Name { get { return name; } set { name = value; } }
        public string FamName { get { return famName; } set { famName = value; } }
        public int Nas { get { return nas; } set { nas = value; } }
        public int NbPersoFam { get { return nbPersoFam; } set { nbPersoFam = value; } }
        public DateTime DateNaiss { get { return dateNaiss; } set { dateNaiss = value; } }
        public char Statut { get { return statut; } set { statut = value; } }
        public char Sexe { get { return sexe; } set { sexe = value; } }
        public double Revenue { get { return revenue; } set { revenue = value; } }

        //Constructors
        public User()//Défaut
        {
            name = "bob";
            famName = "lemon";
            nas = 1337;
            nbPersoFam = 9;
            DateNaiss = DateTime.Today;
            statut = 'd';
            sexe = 'm';
            revenue = 420;
        }

        public User(string name, string famName, int nas, DateTime dateNaiss, char statut, char sexe, double revenue, int nbPersoFam)
        {

        }

        public User(User donnees) : this(donnees.Name, donnees.FamName, donnees.Nas, donnees.DateNaiss, donnees.Statut, donnees.Sexe, donnees.Revenue, donnees.NbPersoFam)
        {

        }
    }
}
