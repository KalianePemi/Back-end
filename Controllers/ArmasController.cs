using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {
       private readonly DataContext _context;
       public ArmasController(DataContext context)
       {
           _context = context; //inicialização do atributo
       }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Arma a = await _context.Armas.FirstOrDefaultAsync(aBusca => aBusca.Id == id);
                return Ok(a);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get ()
        {

            try
            {
                 List<Arma> lista = await _context.Armas.ToListAsync();
                 return Ok(lista);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add (Arma novaArma)
        {
            try
            {
                 await _context.Armas.AddAsync(novaArma);
                 await _context.SaveChangesAsync();
                 return Ok(novaArma.Id);
            }
            catch (Exception ex)
            {
                
                return BadRequest (ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma novaArma)
        {

            try
            {
                 _context.Armas.Update(novaArma); 
                 int linhasAfetadas = await _context.SaveChangesAsync();
                 return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {

            try
            {
                 Arma aRemover = await _context.Armas.FirstOrDefaultAsync(a => a.Id ==id);
                 _context.Armas.Remove(aRemover);
                 int linhasAfetadas = await _context.SaveChangesAsync();
                 return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        } 
        
    }
}