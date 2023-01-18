using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

namespace La_Mia_Pizzeria_1.Models
{
    public class Pizze
    {
        private string foto;
        private string nome;
        private string descrezione;
        private double prezzo;

        public Pizze(string foto, string nome, string descrezione, double prezzo)
        {
            this.foto = foto;
            this.nome = nome;
            this.descrezione = descrezione;
            this.prezzo = prezzo;
        }
    }
}
