using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProducentConsumerProblem
{
    public class Continent
    {
        private int id;
        private string name;


        public int Id 
        {
            get => id; set => id = value; 
        }
        public string Name 
        {
            get => name; set => name = value; 
        }

        
        public Continent(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        
        public Continent(string name)
        {
            this.id = 0;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{id} {name} ";
        }
    }
}