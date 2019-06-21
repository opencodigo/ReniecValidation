using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Week4Day5ReniecApp
{
    class Program
    {
        //Inicio funcion
        static string ConsultarReniec(string dni)
        {
            string html = string.Empty;
            string url = "http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI=" + dni;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            //Console.WriteLine(html);

            return html;
        }
       

        static Boolean BuscarAlumno(string dni)
        {
            string[] alumnos = { "72117500", "74119617", "77657476", "71736721", "70652084", "71521063", "75211693", "43474669", "73131641", "43470033", "70748476", "72815887", "74690111", "47862719", "72313903", "72406945" };
            foreach (var item in alumnos)
            {
                if (item == dni)
                {
                    return true;
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            string[] alumnos = { "72117500", "74119617", "77657476", "71736721", "70652084", "71521063", "75211693", "43474669", "73131641", "43470033", "70748476", "72815887", "74690111", "47862719", "72313903", "72406945" };


            //incio test
            //fin test

            //menu
            string dni = "";
            int opc = 0;
            Boolean esAlumno = false;
            string salir = "";
            string respuestaReniec = "";
            string[] nombres = new string[4];
            Console.WriteLine("\t\tMENU");
            Console.WriteLine("\t1. INGRESAR DNI");
            Console.WriteLine("\t2. LISTAR ALUMNOS");
            Console.WriteLine("Ingrese la opcion:");
            opc = int.Parse(Console.ReadLine());

            switch (opc)
            {
                case 1:
                    do
                    {
                        do
                        {
                            //VALIDAR QUE DNI TENGA 8 DIGITOS
                            Console.Write("Ingrese número de DNI: ");
                            dni = Console.ReadLine();
                        } while (dni.Length != 8);
                        esAlumno = BuscarAlumno(dni);
                        if (esAlumno == false)
                        {
                            Console.WriteLine(">>Lo sentimos no es alumno de CODIGO");
                            Console.WriteLine(">>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<<<");
                        }
                        else
                        {
                            Console.WriteLine(">>> Estamos Validando en RENIEC... <<<");
                            respuestaReniec = ConsultarReniec(dni);
                            nombres = DividirNombre(respuestaReniec);
                            Console.WriteLine("=======Bienvenido=======");
                            Console.WriteLine("Nombres: "+nombres[0]+"- #" + nombres[1] + " Nombres");
                            Console.WriteLine("Apellidos: " + nombres[2] + "- #" + nombres[3] + " Apellidos");
                            Console.Write("Ingrese 'n/N' para salir del programa: ");
                            salir = Console.ReadLine();
                            salir = salir.ToString().ToUpper();
                        }
                    } while (salir != "N");

                    break;
                default:
                    alumnosCodigo(alumnos);
                    break;
            }


            Console.WriteLine("\t\tVERIFICACIÓN DE ALUMNOS CODIGO");
            

        }

        static void alumnosCodigo(string [] alumnos)
        {
             string [] lista = new string[30];
            string nombre = "";


            for (int i = 0; i < alumnos.Length; i++)
            {
                nombre = ConsultarReniec(alumnos[i]);
                Console.WriteLine(nombre.Replace('|', ' '));
            }

            Console.ReadKey();
        }
        static string[] DividirNombre(string str)
        {

            string[] names = str.Split('|');

            string nombres = names[names.Length - 1];
            string[] nombreRaro = nombres.Split(' ');
            int contNomb = 1;
            int contApell = names.Length - 1;
            for (int i = 0; i < nombreRaro.Length - 1; i++)
            {
                if ((nombreRaro[i] == "de" && nombreRaro[i + 1] == "las") || (nombreRaro[i] == "de" && nombreRaro[i + 1] == "los"))
                {
                    contNomb--;
                    i++;
                }
                else if (nombreRaro[i] == "de" || nombreRaro[i] == "del")
                {

                }
                else if (nombreRaro[i] == "la" || nombreRaro[i] == "las")
                {

                }
                else { contNomb++; }
            }
                       
            string apellidos = "";
            for (int i = 0; i < names.Length - 1; i++)
            {
                apellidos = apellidos + names[i] + " ";
            }
            string[] rpta = { names[names.Length - 1], "" + contNomb, apellidos, "" + contApell };
            Console.Write("--------rpta-----------");
            
            return rpta; 
        }

    }
}
