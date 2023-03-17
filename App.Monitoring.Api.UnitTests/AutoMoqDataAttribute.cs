using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace App.Monitoring.Api.UnitTests;

/// <summary>
/// Автогенерировать свойства, в т.ч. moq для внедряемых сервисов.
/// </summary>
internal sealed class AutoMoqDataAttribute : AutoDataAttribute
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public AutoMoqDataAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}
