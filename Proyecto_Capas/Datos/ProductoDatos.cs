using System;
using System.Collections.Generic;
using System.IO;

namespace Datos
{
    public class SeguroData
    {
        private string archivo = "seguros.txt";

        // Registrar un seguro
        public void RegistrarSeguro(string codigo, string tipo, decimal valor, decimal porcentajeIncremento, decimal valorBeneficio)
        {
            // se asegura de que el archivo exista
            if (!File.Exists(archivo))
            {
                // Si no existe, lo crea vacío
                File.WriteAllText(archivo, string.Empty);
            }
            // Formato: codigo;tipo;valor;porcentajeIncremento;valorBeneficio
            string linea = $"{codigo};{tipo};{valor};{porcentajeIncremento};{valorBeneficio}";
            File.AppendAllLines(archivo, new List<string> { linea });
        }

        // Obtiene todos los seguros
        public List<string> ObtenerSeguros()
        {
            // Verifica si el archivo existe; si no, lo crea
            if (!File.Exists(archivo))
            {
                File.WriteAllText(archivo, string.Empty);
            }
            return new List<string>(File.ReadAllLines(archivo));
        }

        // Busca un seguro por código
        public string BuscarSeguro(string codigo)
        {
            List<string> lineas = ObtenerSeguros();
            foreach (var linea in lineas)
            {
                string[] datos = linea.Split(';');
                if (datos.Length >= 5 && datos[0] == codigo)
                    return linea;
            }
            return null;
        }
    }
}
