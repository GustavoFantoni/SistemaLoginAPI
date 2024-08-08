using SistemaLogin.Models;

namespace SistemaLogin.Repository.Interface
{
    public interface IUserInterface
    {
            Task<List<UserModel>> BuscaTodosUsuarios();
            Task<UserModel> BuscaPorMatricula(int matricula);
            Task<String> Login(int matricula, string senha);
            Task<UserModel> AlterarSenha(int matricula, string? senha);
            Task<UserModel> CadastrarUsuario(UserModel novoUsuario);
            Task<string> DeletarUsuario(int matricula);
    }
}
