using EstudoWebAPI.Helpers;
using EstudoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*using EstudoWebAPI.Database; não precisa mais usar pois não precisa mais importar o MIMICContext
*/
namespace EstudoWebAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavraController : ControllerBase
    {
        //Controler - mais focado para construir um site
        //ControllerBase - não é tão voltado para site
        // ActionResult - classe - IActionResult Interface

        private readonly MimicContext _banco;
        public PalavraController(MimicContext banco)
        {
            _banco = banco;
        }

        // APP
        // api/palavras?query.Data=2019-05-01?pagina
        //uso para atualizar as palavras no app instalado 
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery] PalavraUrlQuery query)
        {
            if (query.PagNumero > paginacao.TotalPaginas) // numero de paginas recebido é maior que o total
            {
                return NotFound();
            }
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginacao)); //mandar toda a paginacao

            return Ok(item.Tolist());
        }

        //WEB
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = 
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
           
            return Created($"/api/palavras/{palavra.Id}", palavra); // assim o objeto é retornado
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody]Palavra palavra)
        {
            var obj = 

            if (obj == null)
                return NotFound();

            palavra.Id = id;
            

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            /*_banco.Palavras.Remove(_banco.Palavras.Find(id));*/
            var palavra = _banco.Palavras.Find(id);
            if(palavra == null)
            {
                return NotFound();
            }
            palavra.Ativo = false;
           
            return NoContent();
        }
    }
}
