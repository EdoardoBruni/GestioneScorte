using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GestioneScorte
{
    //CLASSE DATI INPUT
    class Dati_Input
    {
        public double A1, A2, H1, H2, P, D, rapporto;
        public int x;
        public void inserimento()
        {
            //A1= costo di setup per lotto di produzione [$/setup]-->costo del Vendor
            Console.WriteLine("Inserisci il valore di A1.");
            //A1 = Double.Parse(Console.ReadLine());
            A1 = 400;
            //A2= costo di emissione ordine[$/ordine]-->costo Buyer
            Console.WriteLine("Inserisci il valore di A2.");
            //A2 = Double.Parse(Console.ReadLine());
            A2 = 25;
            //H1= costo di mantenimento in giacenza del Vendor per unità e per periodo di tempo[$/unità*udt]
            Console.WriteLine("Inserisci il valore di H1.");
            //H1 = Double.Parse(Console.ReadLine());
            H1=4;
            //H2= costo di mantenimento in giacenza del Buyer
            Console.WriteLine("Inserisci il valore di H2.");
            //H2 = Double.Parse(Console.ReadLine());
            H2 = 5;
            //P= tasso di produzione
            Console.WriteLine("Inserisci il valore di P.");
            //P = Double.Parse(Console.ReadLine());
            P = 3200;
            //D= tasso di richiesta,o domanda 
            Console.WriteLine("Inserisci il valore di D.");
            //D = Double.Parse(Console.ReadLine());
            D = 1000;
            //chiedo all'utente il numero massimo di spediozioni che vuole calcolare
            Console.WriteLine("Inserisci il valore massimo di spedizioni da calcolare");
            x = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("Inserisci il valore di rapporto.");
            // rapporto = Int32.Parse(Console.ReadLine());
        }
    }

    //CLASSE METODO LU
    public class Lu
    {
        //dichiaro le variabili
        double q, q2, Q, C;
        int n;

        //metodo per calcolo q,Q,C
        public void Calcolo_q()
        {
            //chiamo il metodo della classe inserimento per inserire i dati
            //qui ho un problema che ti devo spiegar a voce
            Dati_Input d = new Dati_Input();
            d.inserimento();

            //faccio il ciclo for per calcolarmii valori di q,Q,C al variare del numero di spedizioni 
            for (n = 1; n <= 10; n++)
            {
                //calcolo q^2
                q2 = ((d.A1 + n * d.A2) * (d.D / n) / (d.H1 * ((d.D / d.P) + ((d.P - d.D) / (2 * d.P)) * n) + ((d.H2 - d.H1) / (2))));

                //calcolo la radice quadrata di q^2 
                q = Math.Round(Math.Sqrt(q2));


                //calcolo Q
                Q = n * q;


                //calcolo C
                C = (d.D / (n * q)) * (d.A1 + n * d.A2) + (d.H1 * (q * (d.D / d.P) + ((d.P - d.D) / (2 * d.P)) * n * q)) + (d.H2 - d.H1) * q / 2;

                //stampo i risultati trovati
                Console.WriteLine("q = {0}, Q = {1} , C={2}", q, Q, C);
            }
        }
    }
    //CLASSE METODO GOYAL
    class Goyal
    {
        double C, q1, q2, lamda, Q;

        List<int> n = new List<int>();
        List<double> q = new List<double>();
       
        int numsped,i,t ;
       

        public void Calcolo_q()
        {
            //chiamo il metodo inserimento per chiedere i dati di input all'utente
            Dati_Input h = new Dati_Input();
            h.inserimento();

            
            //creo il vettore con il numero di spedizioni massimo
            for (int i = 1; i < (h.x+1); i++)
             {
                n.Add(i);
             }
            
            //calcolo il rapporto P/D
            lamda = h.P / h.D;

            
            //ciclo foreach per il calcolo dei q e di C e Q
            foreach (int numsped in n)
            {
                //q2 è il quadrato di q
                q2 = ((2 * (h.A1 + numsped * h.A2)) * h.D * lamda * ((Math.Pow(lamda, 2)) - 1)) / ((h.H1 + (lamda) * h.H2) * (Math.Pow(lamda, 2 * numsped)));
                //q1 è la quantità della prima spedizione
                q1 = Math.Round(Math.Sqrt(q2));
                //calcolo C
                C = (h.A1 + numsped * h.A2) * (h.D / q1) * ((lamda - 1) / (Math.Pow(lamda, numsped) - 1)) + (q1 * (h.H1 + lamda * h.H2) * (Math.Pow(lamda, numsped) + 1)) / (2 * lamda * (lamda + 1));
               
                    
                    //calcola q
                    for (int t = 0; t < numsped; t++)
                    {
                        q.Insert(t , q1 * Math.Pow(lamda, t));
                    }

                    //calcolo Q tramite una sommatoria
                    Q = 0;
                    for (int t = 0; t < numsped; t++)
                    {
                          Q += q[t];
                    }

                    //Goyal g = new Goyal();
                    // g.Stampa_risultati();

                    //stampo risultati
                    Console.WriteLine("numero di spedizioni={0}", numsped);

                    //stampo valori quantità spedizioni
                    for (int t = 0; t < numsped; t++)
                    {
                        Console.WriteLine("q[{0}]= {1}", t, q[t]);
                    }

                    //stampo C e Q
                    Console.WriteLine(" C={0} , Q={1} ", C, Q);
                    Console.ReadLine();
            }
        }


        /*
        //chiamo il metodo per stampare i risultati
                public void Stampa_risultati()
                {
                    Console.WriteLine("numero di spedizioni={0}", numsped);
                    

                    //stampo valori quantità spedizioni
                    for (int t = 0; t < numsped; t++)
                    {
                        Console.WriteLine("q[{0}]= {1}", t, q[t]);
                    }
                    //stampo C e Q
                    Console.WriteLine(" C={0} , Q={1} ", C, Q);
                    Console.ReadLine();
                }
                
            */    
              
        }

    class Program
    {
        static void Main(string[] args)
        {
            int scelta;
        label:
            //faccio scegliere il metodo dall'utente
            Console.WriteLine("Inserisci 1 per metodo Lu, 2 per il metodo Goyal, 3 per il metodo Hill");
            Console.WriteLine("Inserisci il numero che vuoi per scegliere il metodo da utilizzare:");
            scelta = Int32.Parse(Console.ReadLine());

            switch (scelta)
            {
                //chiama il metodo Lu
                case 1:
                    Console.WriteLine("Hai scelto il metodo Lu, inserisci i dati");
                    Lu l = new Lu();
                    l.Calcolo_q();
                    break;

                //chiama il metodo Goyal
                case 2:
                    Console.WriteLine("Hai scelto il metodo Goyal, inserisci i dati");
                    Goyal g = new Goyal();
                    g.Calcolo_q();
                    
                    break;

                //chiama il metodo Hill
                case 3:
                    Console.WriteLine("Hai scelto il metodo Hill, inserisci i dati");
                    break;

                //altra scelta
                default:
                    Console.WriteLine("DEVI SCEGLIERE UN NUMERO TRA 1 E 3!!!");
                    goto label;
            }
            Console.ReadLine();
        }
    }
}
