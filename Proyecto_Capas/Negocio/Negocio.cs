using System;
using Datos; // Se usa el namespace del proyecto Datos

namespace Negocio
{
    public class SeguroBL
    {
        private SeguroData data = new SeguroData();

        //Registra un seguro llamando a la capa de datos
        public void RegistrarSeguro(string codigo, string tipo, decimal valor, decimal porcentajeIncremento, decimal valorBeneficio)
        {
            data.RegistrarSeguro(codigo, tipo, valor, porcentajeIncremento, valorBeneficio);
        }

        // Obtiene la información de un seguro y la retorna como objeto Seguro
        public Seguro ObtenerSeguro(string codigo)
        {
            string linea = data.BuscarSeguro(codigo);
            if (linea == null)
                return null;

            //línea del formato: codigo;tipo;valor;porcentajeIncremento;valorBeneficio
            string[] datos = linea.Split(';');
            if (datos.Length < 5)
                throw new FormatException("La línea encontrada no tiene el formato correcto.");

            // Conversión de valores numéricos
            if (!decimal.TryParse(datos[2], out decimal valor))
                throw new FormatException("El valor ingresado no es un número válido.");
            if (!decimal.TryParse(datos[3], out decimal porcentajeIncremento))
                throw new FormatException("El porcentaje de incremento no es un número válido.");
            if (!decimal.TryParse(datos[4], out decimal valorBeneficio))
                throw new FormatException("El valor del beneficio no es un número válido.");

            Seguro seguro = new Seguro
            {
                Codigo = datos[0],
                Tipo = datos[1],
                Valor = valor,
                PorcentajeIncremento = porcentajeIncremento,
                ValorBeneficio = valorBeneficio
            };
            return seguro;
        }

        // Método para calcular el precio final de venta
        // Se incrementa el valor del seguro en un 10% por cada beneficiario
        public decimal CalcularVenta(Seguro seguro, int cantidadBeneficiarios)
        {
            decimal incrementoBeneficiarios = seguro.Valor * 0.10m * cantidadBeneficiarios;
            // Se incluye el incremento establecido para el tipo de seguro
            decimal incrementoTipo = seguro.Valor * seguro.PorcentajeIncremento / 100;
            decimal precioFinal = seguro.Valor + incrementoBeneficiarios + incrementoTipo;
            return precioFinal;
        }
    }

    // Clase para representar el seguro
    public class Seguro
    {
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal PorcentajeIncremento { get; set; }
        public decimal ValorBeneficio { get; set; }
    }
}
