using System.ComponentModel.DataAnnotations;

namespace Models.Domain;


public class Empleado 
{
  public Guid Id {get; set;}
  public string Nombre {get; set;}
  [DisplayFormat(DataFormatString = "{0:C}")]
  public double SalarioBase {get; set;}
  public List<Salario>? Salarios {get; set;}
 

}

public class Salario
{
  public Guid Id{set; get;}
  public DateTime FechaSalario {get; set;}
  public Guid EmpleadoId {get; set;}
  public Empleado Empleado;
  public double Horas {get; set;}
  public double HorasExtra {get; set;}

  [DisplayFormat(DataFormatString = "{0:C}")]
  public double SalarioBruto {get; private set;}
  [DisplayFormat(DataFormatString = "{0:C}")]
  public double SalarioNeto {get; private set;}
  [DisplayFormat(DataFormatString = "{0:C}")]
  public double MontoDeduciones {get; private set;}
  public int PorcentajeDeduciones {get; private set;}



  public void CalcularSalario(){
    CalcularSalarioBruto();
    CalcularSalarioNeto();
  }
  private void CalcularSalarioBruto(){

    this.SalarioBruto = this.Empleado.SalarioBase * this.Horas;

    if(HorasExtra > 0){
            SalarioBruto += Empleado.SalarioBase * HorasExtra * 1.5;
        }

  }

  private void CalcularSalarioNeto()
  {
    CalcularDeducciones();
    MontoDeduciones = SalarioBruto * PorcentajeDeduciones/100;
    SalarioNeto = SalarioBruto - MontoDeduciones;
  }

  private void CalcularDeducciones(){

    if(SalarioBruto <= 250000){
        PorcentajeDeduciones = 9;
    }else{
        if(SalarioBruto > 250000 && SalarioBruto <= 380000){
        PorcentajeDeduciones = 12;
    }else PorcentajeDeduciones = 15;
    }
  }


}


