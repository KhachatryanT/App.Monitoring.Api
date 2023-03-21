using Xunit;

namespace App.Monitoring.Host.IntegrationTests;

/// <summary>
/// Класс необходим для инициализации контекста приложения и шаринга его между тестами.
/// </summary>
[CollectionDefinition(nameof(ApplicationCollection))]
public sealed class ApplicationCollection : ICollectionFixture<AppWebApplicationFactory>
{
}
