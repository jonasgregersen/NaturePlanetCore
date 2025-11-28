using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using DataAccessLayer.Model;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

public static class TestServiceProvider
{
    public static IServiceProvider Instance { get; private set; }

    public static void Initialize()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DatabaseContext>(options =>

        options.UseInMemoryDatabase("ProductTestDb")
        );


        var userStore = new Mock<IUserPasswordStore<ApplicationUser>>();

        userStore.Setup(x => x.CreateAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(IdentityResult.Success);

        userStore.Setup(x => x.SetPasswordHashAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        userStore.Setup(x => x.GetPasswordHashAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync("testhash");

        userStore.Setup(x => x.HasPasswordAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        userStore.Setup(x => x.FindByNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((string userName, CancellationToken _) =>
                new ApplicationUser { UserName = userName, Email = userName });

        // UserManager
        services.AddSingleton(provider =>
        {
            return new UserManager<ApplicationUser>(
                userStore.Object,
                null,
                new PasswordHasher<ApplicationUser>(),
                Array.Empty<IUserValidator<ApplicationUser>>(),
                Array.Empty<IPasswordValidator<ApplicationUser>>(),
                null,
                null,
                null,
                Mock.Of<ILogger<UserManager<ApplicationUser>>>()
            );
        });

        // SignInManager
        services.AddSingleton(provider =>
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();

            var ctxAccessor = new Mock<IHttpContextAccessor>();
            ctxAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            var mock = new Mock<SignInManager<ApplicationUser>>(
                userManager,
                ctxAccessor.Object,
                claimsFactory.Object,
                null, null, null, null
            );

            mock.Setup(m => m.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            return mock.Object;
        });

        Instance = services.BuildServiceProvider();
    }
}