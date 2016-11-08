using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Model;

namespace Domain.Service.Tests
{
    [TestClass()]
    public class UsuarioServiceTests
    {
        [TestMethod()]
        public void GravarUsuario()
        {
            // Arrange
            IBaseServiceUsuario service = new UsuarioService();
            var usuario = new Usuario() { Email = "jb.alessandro@gmail.com", Nome = "José Alessandro" };

            // Act
            var usuarioCadastrado = service.Gravar(usuario);

            // Assert

            Assert.IsTrue(usuario.Nome == usuarioCadastrado.Nome);
            Assert.IsTrue(usuarioCadastrado.Id > 0);
        }

        [TestMethod()]
        public void ListarTest()
        {
            IBaseServiceUsuario service = new UsuarioService();
            var usuarios = service.Listar().Where(x => x.Email.Contains("jb.alessandro")).ToList();

            Assert.IsTrue(usuarios.Count == 1);
        }
    }
}