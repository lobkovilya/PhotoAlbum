using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.WebAPI.Controllers;

namespace PhotoAlbum.Test.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void RegisterTest()
        {
            var mock = new Mock<IUserService>();

            mock.Setup(a => a.CreateAsync(It.IsAny<UserCreateDto>())).Returns(Task.FromResult(IdentityResult.Success));

            var controller = new AccountController(mock.Object);

            
            var userModel = new UserCreateDto()
            {
                Login = "login",
                Password = "password",
                Email = "a@b.ru",
                FirstName = "fName",
                LastName = "lName"
            };

            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            var result = controller.Register(userModel).Result;

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}