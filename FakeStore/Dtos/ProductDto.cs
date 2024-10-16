using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStore.Dtos
{
    public class ProductDto
    {
        public int id { get; set; } // ID del producto (proporcionado por la API)
        public string title { get; set; } // Título del producto
        public decimal price { get; set; } // Precio del producto
        public string description { get; set; } // Descripción del producto
        public string category { get; set; } // Categoría del producto
        public string image { get; set; } // URL de la imagen del producto
     

        // Este es un objeto que contiene la calificación y el número de calificaciones
        public Rating rating { get; set; }

        // Clase para manejar la estructura de rating
        public class Rating
        {
            public double rate { get; set; } // Calificación del producto
            public int count { get; set; } // Número de calificaciones
        }
    }
}
