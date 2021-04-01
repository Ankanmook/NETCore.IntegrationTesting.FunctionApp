using Xunit;

namespace NETCore.IntegrationTesting.FunctionApp.IntegrationTests
{
    [CollectionDefinition(Name)]
    public class TestsCollection : ICollectionFixture<TestHost>
    {
        public const string Name = nameof(TestsCollection);
    }
}