using Itis.DropDataBase.Api.UnitTests;
using Itis.Playhost.Api.Contracts.Enums;
using Itis.Playhost.Api.Core.Entities;
using Xunit;

namespace Itis.MyTrainings.Api.UnitTests.Requests.User;

/// <summary>
/// TODO: База тестов написана, осталось написать тесты
/// </summary>
public class Test : UnitTestBase
{
    private UserProfile _userProfile;
    
    public Test()
    {
        _userProfile = new UserProfile()
        {
            Gender = Genders.Man
        };
    }
    
    [Fact]
    public async Task Handle_QueryWithFilters_ShouldReturnFilteredEntitiesAsync()
    {
        var dbcontext = CreateInMemoryContext(
            x => x.Add(_userProfile)
            );
        Assert.True(true);
    }
}