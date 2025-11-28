using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[Binding]
public class RegisterUserSteps
{
    private readonly UserManager<ApplicationUser> _userManager;
    private string _email;
    private string _password;
    private IdentityResult _result;

    public RegisterUserSteps()
    {
        _userManager = TestServiceProvider.Instance.GetRequiredService<UserManager<ApplicationUser>>();
    }

    [Given("a user with email {string} and password {string}")]
    public void GivenAUserWithEmailAndPassword(string p0, string p1)
    {
        _email = p0;
        _password = p1;
    }

    [When("the user tries to register")]
    public async Task WhenUserRegisters()
    {
        var user = new ApplicationUser
        {
            Email = _email,
            UserName = _email
        };

        _result = await _userManager.CreateAsync(user, _password);
    }

    [Then("the registration should be successful")]
    public void ThenRegistrationSucceeds()
    {
        Assert.IsTrue(_result.Succeeded);
    }
}