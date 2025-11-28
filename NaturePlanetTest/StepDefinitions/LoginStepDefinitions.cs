using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using System.Threading.Tasks;
using Xunit;

namespace Test.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private string _email;
        private string _password;
        private SignInResult _result;

        public LoginStepDefinitions()
        {
            _userManager = TestServiceProvider.Instance.GetRequiredService<UserManager<ApplicationUser>>();
            _signInManager = TestServiceProvider.Instance.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        [Given("i enter the username {string} and password {string}")]
        public async Task GivenIEnterTheUsernameAndPassword(string email, string password)
        {
            _email = email;
            _password = password;

           
            var user = new ApplicationUser { Email = email, UserName = email };
            await _userManager.CreateAsync(user, password);
        }

        [When("i enter the login button")]
        public async Task WhenIEnterTheLoginButton()
        {
            _result = await _signInManager.PasswordSignInAsync(
                _email,
                _password,
                false,
                false
            );
        }

        [Then("i login succesfully")]
        public void ThenILoginSuccesfully()
        {
            Assert.IsTrue(_result.Succeeded);
        }
    }
}