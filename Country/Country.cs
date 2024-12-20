using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProducentConsumerProblem
{
    public class Country
    {
        private int id;
        private int continent_id;
        private int alliance_id;
        private string name;
        private int population;
        private double area;
        private double gdp;


        public int Id 
        { 
            get => id; set => id = value; 
        }
        public int Continent_id 
        {
            get => continent_id; set => continent_id = value; 
        }
        public int Alliance_id 
        {
            get => alliance_id; set => alliance_id = value; 
        }
        public string Name 
        {
            get => name; set => name = value; 
        }
        public int Population 
        {
            get => population; set => population = value; 
        }
        public double Area 
        {
            get => area; set => area = value; 
        }
        public double Gdp 
        {
            get => gdp; set => gdp = value; 
        }

        
        public Country(int id, int continent_id, int alliance_id, string name, int population, double area, double gdp)
        {
            this.id = id;
            this.continent_id = continent_id;
            this.alliance_id = alliance_id;
            this.name = name;
            this.population = population;
            this.area = area;
            this.gdp = gdp;
        }

        
        public Country(int continent_id, int alliance_id, string name, int population, double area, double gdp)
        {
            this.id = 0;
            this.continent_id = continent_id;
            this.alliance_id = alliance_id;
            this.name = name;
            this.population = population;
            this.area = area;
            this.gdp = gdp;
        }

        public override string ToString()
        {
            return $"{id} {continent_id} {alliance_id} {name} {population} {area} {gdp} ";
        }
    }
}