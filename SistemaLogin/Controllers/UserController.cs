using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaLogin.Models;
using SistemaLogin.Repository.Interface;

namespace SistemaLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }


        [HttpGet("BuscarTodosUsuarios/")]
        public async Task<ActionResult<List<UserModel>>> BuscaTodosUsuarios() // EndPoint
        {
            List<UserModel> usuarios = await _userInterface.BuscaTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("BuscaPorMatricula/{matricula}")]
        public async Task<ActionResult<UserModel>> BuscaPorMatricula(int matricula)
        {
            UserModel usuario = await _userInterface.BuscaPorMatricula(matricula);
            return Ok(usuario);
        }

        [HttpGet("Login/{matricula} {senha}")]
        public async Task<ActionResult<String>> Login(int matricula, string senha)
        {
            return Ok(await _userInterface.Login(matricula, senha));
        }

        [HttpPost("CadastrarUsuario/")]
        public async Task<ActionResult<UserModel>> CadastrarUsuario([FromBody] UserModel usuario)
        {
            UserModel novoUsuario = await _userInterface.CadastrarUsuario(usuario);
            return Ok(novoUsuario);
        }
        [HttpPut("AlterarSenha/")]
        public async Task<ActionResult<UserModel>> AlterarSenha(int matricula, string? senha)
        {
            UserModel usuario = await _userInterface.AlterarSenha(matricula, senha);
            return Ok(usuario);
        }
        [HttpDelete("DeletarCadastro/{matricula}")]
        public async Task<ActionResult<bool>> DeletarUsuario(int matricula)
        {
            return Ok(await _userInterface.DeletarUsuario(matricula));
        }
    }
}
