using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Clinica.Shared.Dtos
{
    public class RepresentanteDto
    {
        public int Id_Representante { get; set; }
        public string? Nombres { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono_Celular { get; set; }
        public string? Telefono_Fijo { get; set; }
        public string? Persona_Contacto { get; set; }
        public string? Telefono_Contacto { get; set; }
        public string? Email { get; set; }
        public string? Tipo_Identificacion { get; set; }
        public string? Identificacion { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public string? Usuario_Crea { get; set; }
        public string? Equipo_Crea { get; set; }
        public DateTime Fecha_Modifica { get; set; }
        public string? Usuario_Modifica { get; set; }
        public string? Equipo_Modifica { get; set; }
        public string? Estado { get; set; }
    }
}
