using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using System.Security.Claims;

namespace RpgApi.Controllers
{
    // [Authorize(Roles="Jogador, Admin")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context; //Declaração
        public PersonagensController(DataContext context)
        {
            _context = context; //inicialização do atributo
        }

        [HttpGet("{id}")] // Buscar pelo id
        //async assincrona este metodo possibilita que outra coisa esteja acontecendo com o mesmo metodo em outro lugar da aplicacao
        public async Task<IActionResult> GetSingle(int id)
        {
            try // serve para tratar o erro, com mensagem 
            {
                Personagem p = await _context.Personagens
                .Include(ar => ar.Arma) //Inclui na propriedade Arma do objeto p
                .Include(us => us.Usuario)//Inclui na propriedade Usuario do objeto p
                .Include(ph => ph.PersonagemHabilidades)
                    .ThenInclude(h => h.Habilidade) //Inclui na lista de PersonagemHabilidade de p
                .FirstOrDefaultAsync(pBusca => pBusca.Id == id);
                return Ok(p);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.Personagens.ToListAsync();
                return Ok(lista); 
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

    
        }

        [HttpPost]
            public async Task<IActionResult> Add(Personagem novoPersonagem)
            {
                try
                {

                    if(novoPersonagem.PontosVida > 100)
                    {
                        throw new System.Exception("Pontos de vida não pode ser maior que 100");
                    }
                    // int usuarioId = int.Parse(_httpContextoAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    // novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == usuarioId);
                    //novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == ObterUsuarioId());


                    await _context.Personagens.AddAsync(novoPersonagem);
                    await _context.SaveChangesAsync();
                    return Ok(novoPersonagem.Id);
                }
                catch (System.Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }



            }

        [HttpPut]
                public async Task<IActionResult> Update(Personagem novoPersonagem)
                {

                    try
                    {
                        if(novoPersonagem.PontosVida > 100)
                    {
                        throw new System.Exception("Pontos de vida não pode ser maior que 100");
                    }
                    // int usuarioId = int.Parse(_httpContextoAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    // novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == usuarioId);
                    //novoPersonagem.Usuario = _context.Usuarios.FirstOrDefault(uBusca => uBusca.Id == ObterUsuario());

                        _context.Personagens.Update(novoPersonagem);
                        int linhaAfetadas = await _context.SaveChangesAsync();
                        return Ok(linhaAfetadas);
                    }
                    catch (System.Exception ex)
                    {
                        
                        return BadRequest(ex.Message);
                    }

                }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                Personagem pRemover = await _context.Personagens.FirstOrDefaultAsync(p => p.Id == id);
                _context.Personagens.Remove(pRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();
                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }  

        // private string ObterPerfilUsuario()
        // {
        //     return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        // }    

        // [HttpGet("GetByPerfil")]
        // public async Task<IActionResult> GetByPerfilAsync()
        // {
        //     try
        //     {
        //         List<Personagem> lista = new List<Personagem>(); 
        //         if(ObterPerfilUsuario() == "Admin")
        //         {
        //             lista = await _context.Personagens.ToListAsync();
        //         }
        //         else
        //         {
        //             lista = await _context.Personagens.ToListAsync 
        //                     .Where(p => p.Usuario.Id == ObterUsuarioId()).ToListAsync(); 
        //         }
        //         return Ok (lista)
        //     }
        //     catch (System.Exception ex)
        //     {
                
        //         return BadRequest(ex.Message);
        //     }
        // } 
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUserAsync()
        {
            try
            {
                 int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                 List<Personagem> lista = await _context.Personagens
                    .Where(u => u.Usuario.Id == id).ToListAsync();
                    return Ok (lista);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}