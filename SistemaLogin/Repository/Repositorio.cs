using Microsoft.EntityFrameworkCore;
using SistemaLogin.Data;
using SistemaLogin.Models;
using SistemaLogin.Repository.Interface;
using System.Security.Authentication;
using System.Xml.Linq;

namespace SistemaLogin.Repository
{
    public class Repositorio : IUserInterface
    {
        private readonly SistemaLoginDBContext _context;

        public Repositorio(SistemaLoginDBContext context)
        {
            _context = context;
        }





        // Métodos --------------------------------------------------------------


        // Getters -------------

        public async Task<List<UserModel>> BuscaTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }



        public async Task<UserModel> BuscaPorMatricula(int matricula)
        {
            var response = await _context.Usuarios.FirstOrDefaultAsync(x => x.Matricula == matricula);
            if (response == null)
                throw new ArgumentNullException(nameof(matricula), "Matrícula não encontrada");
            return response;
        }


        public async Task<string> Login(int matricula, string senha)
        {
            UserModel userModel = await BuscaPorMatricula(matricula);

            if (userModel.Senha == senha)
                return "Login efetuado com sucesso!";
            throw new InvalidCredentialException("Matricula ou senha inválida!");
        }


        // Getters -------------



        // Setter --------------

        public async Task<UserModel> CadastrarUsuario(UserModel novoUsuario)
        {
            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
            return novoUsuario;
        }

        public async Task<UserModel> AlterarSenha(int matricula, string? senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new ArgumentNullException(nameof(senha), "O campo 'senha' não pode estar vazio.");
            if (matricula == 0)
                throw new ArgumentNullException(nameof(matricula), "O campo 'matricula' não pode estar vazio.");


            UserModel userModel = await BuscaPorMatricula(matricula);
            if (userModel == null)
                throw new ArgumentNullException(nameof(matricula), $"A matricula {matricula} não foi encontrada!");
            userModel.Senha = senha;
            _context.Usuarios.Update(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        // Setter --------------


        // Delete --------------
        public async Task<String> DeletarUsuario(int matricula)
        {
            UserModel userModel = await BuscaPorMatricula(matricula);

            if (userModel == null)
                throw new ArgumentNullException(nameof(matricula), $"Matrícula: {matricula} não encontrada");
            _context.Usuarios.Remove(userModel);
            await _context.SaveChangesAsync();
            return $"Usuário com matrícula: {matricula} Removido!";
        }
        // Delete --------------



       
    }
}
