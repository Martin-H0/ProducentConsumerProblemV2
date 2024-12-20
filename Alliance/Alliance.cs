using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProducentConsumerProblem
{
    public class Alliance
    {
        private int id;
        private string name;
        private string type;


        public int Id 
        {
            get => id; set => id = value; 
        }
        public string Name 
        {
            get => name; set => name = value; 
        }
        public string Type 
        {
            get => type; set => type = value; 
        }

        
        public Alliance(int id, string name, string type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }

        
        public Alliance(string name, string type)
        {
            this.id = 0;
            this.name = name;
            this.type = type;
        }

        public override string ToString()
        {
            return $"{id} {name} {type} ";
        }
    }
}