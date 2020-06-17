using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Adresse
{
    public long Id;
    public int Numero;
    public string Rue;
    public string Ville;
    public int CodePostal;

    public Adresse()
    {
    }

    public Adresse(int numero, string rue, string ville, int codePostal)
    {
        Numero = numero;
        Rue = rue;
        Ville = ville;
        CodePostal = codePostal;
    }
}
