using Domain.Abstract;
using Domain.Model;
using Domain.Repository;
using System;
using System.Linq;

namespace Domain.Service
{
    public class UsuarioService : IBaseServiceUsuario
    {
        private IBaseRepository<Usuario> repository;

        public UsuarioService()
        {
            repository = new EFRepository<Usuario>();
        }

        public Usuario Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Não é possível excluir este usuário");
            }
        }

        public Usuario Find(int id)
        {
            return repository.Find(id);
        }

        public Usuario Gravar(Usuario item)
        {
            item.Nome = item.Nome.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();

            if (repository.Listar().Where(x => x.Email == item.Email).Count() > 0)
            {
                throw new ArgumentException("Já existe um usuário cadastrado com este e-mail");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item);
            }

            return repository.Alterar(item);
        }

        public IQueryable<Usuario> Listar()
        {
            return repository.Listar();
        }
    }
}
