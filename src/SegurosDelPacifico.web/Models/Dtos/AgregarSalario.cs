using System.ComponentModel.DataAnnotations;

namespace Models.Dtos;

public class AgregarSalario
{
   [Required]
  public DateTime FechaSalario {get; set;}

  [Required]
  public Guid EmpleadoId {get; set;}

  [Required]
  public double Horas {get; set;}

  [Required]
  public double HorasExtra {get; set;}
  
}