using System.ComponentModel.DataAnnotations;

namespace Models.Dtos;

public class CrearEmpleado
{

    [Required]
    public string Nombre {get; set;}

    [Required]
    public double Salario {get; set;}
}