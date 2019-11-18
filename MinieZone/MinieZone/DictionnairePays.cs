using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class DictionnairePays
    {
        public Dictionary<string, int> LivraisonPays { get; set; }

        public DictionnairePays()
        {
            LivraisonPays = new Dictionary<string, int>
            {
                {"France" , 1},
                {"Allemagne" , 2 },
                {"Autriche" , 2 },
                {"Belgique" , 2 },
                {"Bulgarie" , 2 },
                {"Chypre" , 2 },
                {"Croatie" , 2 },
                {"Danemark" , 2 },
                {"Espagne" , 2 },
                {"Estonie" , 2 },
                {"Finlande" , 2 },
                {"Grèce" , 2 },
                {"Hongrie" , 2 },
                {"Irlande" , 2 },
                {"Italie" , 2 },
                {"Lettonie" , 2 },
                {"Lituanie" , 2 },
                {"Luxembourg" , 2 }
            };
        }
    }
}
