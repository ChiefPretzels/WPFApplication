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
        string name;
        string famName;
        double expense;
        public string Name { get { return name; } set { name = value; } }
        public string FamName { get { return famName; } set { famName = value; } }
        public double Expense { get { return expense; } set { expense = value; } }
        //Constructors
        public User()//Défaut
        {
            name = "name";
            famName = "famname";
            expense = 0;
        }

        public User(User donnees)//Copie
        {
            new User();
        }
    }
}
