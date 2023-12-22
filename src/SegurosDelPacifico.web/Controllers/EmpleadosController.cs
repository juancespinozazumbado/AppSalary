using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Application.Interfaces;
using Models.Domain;
using Models.Dtos;

namespace SegurosDelPacific.web.Controllers;

[Authorize]
public class EmpleadosController : Controller
{

   private readonly AppDbContext _context;
  // private readonly IRepositorio<Empleado> _empleados;

   public EmpleadosController(AppDbContext context)
   {
    this._context = context;
    //this._empleados = repositorio;
   }


    public async Task<IActionResult> Index()
    {
        var empleados = await _context.Empleados.Include(e => e.Salarios).ToListAsync();

        return View(empleados);

    }

    public async Task<IActionResult> Crear(){
        var empleado = new CrearEmpleado();
        return View(empleado);
    }

    [HttpPost, ActionName("Guardar")]
    public async Task<IActionResult> GuardarEmpleado(CrearEmpleado input)
    {
        if (ModelState.IsValid)
        {

            try
            {
                Empleado empleado = new Empleado()
                {
                    Nombre = input.Nombre,
                    SalarioBase = input.Salario
                };

               await _context.Empleados.AddAsync(empleado);
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }catch(Exception ex){
                return View(nameof(Crear));
            }

      }
      return View(nameof(Crear));

    }


    public async Task<IActionResult> Detalles(Guid? id){
        
        try{

            if(id.HasValue){
                var empleado = await _context.Empleados.Include(e => e.Salarios)
                   .FirstOrDefaultAsync(e => e.Id.Equals(id.Value));
                   ViewBag.Empleado = empleado;

            if(empleado != null) return View(empleado);

            return View();

            }else return View();

            


        }catch(Exception e){
            return View();
        }
    }

    public async Task<IActionResult> Editar(string idEmpleado)
    {

        try{

            var empleado = await  _context.Empleados.FindAsync(idEmpleado);
            ViewBag.Empleado = empleado;
            return View(empleado);
            
        }catch(Exception e){
            return View();
        }

    }


    [HttpPost]
    public async Task<IActionResult> Editar(string idEmpleado, Empleado datos)
    {
        
        try{
             
             if(ModelState.IsValid){

               var empleado = await _context.Empleados.FindAsync(idEmpleado);
               if(empleado != null){
               empleado.Nombre = datos.Nombre;
               empleado.SalarioBase = datos.SalarioBase;
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof (Index));

               }else return View();

               

             }else return View();


        }
        catch (Exception e)
        {
            return View();
        }
    }

    public async Task<IActionResult> AgregarSalario(Guid? id)
    {

        try
        {

            if (id.HasValue)
            {
                var empleado = await _context.Empleados.FindAsync(id);
                ViewBag.Empleado = empleado;
                return View(new AgregarSalario());

            }else return View();

        }
        catch (Exception e)
        {
            return View();
        }

    }


    [HttpPost]
    public async Task<IActionResult> AgregarSalario( AgregarSalario datos)
    {
        
        try{
             
             if(ModelState.IsValid){

               var empleado = await _context.Empleados.Include(e=> e.Salarios)
                 .Where(e=> e.Id.Equals(new Guid(datos.EmpleadoId.ToString())))
                     .FirstOrDefaultAsync();
               if(empleado != null){
                 var Salario = new Salario(){
                    FechaSalario = datos.FechaSalario,
                    EmpleadoId = new Guid(datos.EmpleadoId.ToString()),
                    Empleado = empleado,
                    Horas = datos.Horas,
                    HorasExtra = datos.HorasExtra

                    
                };
                Salario.CalcularSalario();
                empleado.Salarios.Add(Salario);
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Detalles), empleado);

               }else return View();

               

             }else return View();
        

        }catch(Exception e){
            return View();
        }
       

    }

   [HttpDelete]
    public async Task<IActionResult> Eliminar(Guid IdEmpleado){

       var empleado = await _context.Empleados.FindAsync(IdEmpleado);

        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync();

        return View(nameof(Index));        

    }

}