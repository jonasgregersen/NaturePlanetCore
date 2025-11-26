using Reqnroll;

[Binding]
public class StartupHooks
{
    [BeforeTestRun]
    public static void Setup()
    {
        TestServiceProvider.Initialize();
    }
}