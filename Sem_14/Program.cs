using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sem_14
{
    internal class Program
    {
        static string[] codigos = { "OC-101", "OC-102", "OC-103", "OC-104", "OC-105", "OC-106" };
        static string[] proveedores = { "OP", "LO36856", "MINU", "OKO", "PLA", "OL" };
        static double[,] datos = {{150,10}, {420,2}, {80,50}, {1200, 1}, {25,100}, {600, 3}};
        static double[] montos = new double[6];
        static double total = 0;
        static int contador = 0; 

        static public bool Validaciones(double precio, double cantidad)
        {
            if (precio>0 && cantidad >0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error el precio unitario y la cantidad debe ser mayor a 0");
                return false;
            }
        }

        static public void monto_acumula_cuenta()
        {
            for (int i =0; i< codigos.Length; i++)
            {
                Validaciones(datos[i, 0], datos[i, 1]);
                montos[i] = datos[i, 0] * datos[i, 1];
                total = total + montos[i];
                if (montos[i] > 1000)
                {
                    contador++;
                }
            }
        }

        static public void orden_desendente()
        {
            int n = codigos.Length;
            for(int i=0; i< n-1; i++)
            {
                for(int j = 0; j<n-1-i; j++)
                {
                    if (montos[j] < montos[j + 1])
                    {
                        double temp_monto = montos[j];
                        montos[j] = montos[j + 1];
                        montos[j + 1] = temp_monto;

                        string temp_cod = codigos[j];
                        codigos[j] = codigos[j + 1];
                        codigos[j + 1] = temp_cod;

                        string temp_prov = proveedores[j];
                        proveedores[j] = proveedores[j + 1];
                        proveedores[j + 1] = temp_prov;

                        double temp_precio = datos[j, 0];
                        datos[j, 0] = datos[j + 1, 0];
                        datos[j + 1, 0] = temp_precio;

                        double temp_cant = datos[j, 1];
                        datos[j, 1] = datos[j + 1, 1];
                        datos[j + 1, 1] = temp_cant;
                    }
                }
            }
        }

        static public void buscar(string cod_buscar)
        {
            bool encontrado = false;
            if (string.IsNullOrWhiteSpace(cod_buscar))
            {
                Console.WriteLine("Valor vacío");
                return;
            }
            for (int i = 0; i < codigos.Length; i++)
            {
                if (codigos[i] == cod_buscar)
                {
                    Console.WriteLine("Código encontrado: ");
                    Console.WriteLine("Proveedor: " + proveedores[i]);
                    Console.WriteLine("Monto: $" + montos[i]);
                    encontrado = true;
                    break;
                }
            }
            if (!encontrado)
                Console.WriteLine("Código no encontrado...!!!!!!!!!");

        }

        static public void imprimir()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("\nCONTROL DE ÓRDENES DE COMPRA");
            Console.WriteLine("=================================================");

            for (int i=0; i<codigos.Length; i++)
            {
                Console.WriteLine("Código" + codigos[i]+"| Prov: " + proveedores[i]+ "| Montos: $."+ montos[i]);
            }

        }


        static void Main(string[] args)
        {
            monto_acumula_cuenta();
            orden_desendente();
            imprimir();
            Console.WriteLine("==================================================================================");
            Console.WriteLine("MÉTRICAS RESUME: ");
            Console.WriteLine("* Gran Total Acuomuldo       : $" + total);
            Console.WriteLine("* Compras Crítcas (> $ 1,000): " + contador + "órdenes");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("\nPRUEBA DE BÚSQUEDA");
            buscar("OC-101");
            Console.WriteLine("==================================================================================");

        }
    }
}
