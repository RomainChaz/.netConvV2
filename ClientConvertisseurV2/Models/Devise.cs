using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientConvertisseurV2.Models
{
    public class Devise
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Taux { get; set; }

        public Devise (int id, string nom, double taux)
        {
            this.Id = id;
            this.Nom = nom;
            this.Taux = taux;
        }

        public Devise()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}