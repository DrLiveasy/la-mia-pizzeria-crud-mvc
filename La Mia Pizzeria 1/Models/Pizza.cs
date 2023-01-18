﻿using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

namespace La_Mia_Pizzeria_1.Models
{
    public class Pizza
    {
        public int Id { get; set; } 
        public string Foto { get; set; }
        public string Nome { get ; set ; }
        public string Descrezione { get ; set ; }
        public double Prezzo { get; set; }

        
        public Pizza(string foto, string nome, string descrezione, double prezzo)
        {
            this.Foto = foto;
            this.Nome = nome;
            this.Descrezione = descrezione;
            this.Prezzo = prezzo;
        }

    }
}
