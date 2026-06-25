# Unit Tests for Ademide Info Website

## Overview
This project contains comprehensive unit tests for the **AppointmentService** and **ProfileService** using:
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## Test Projects Structure

```
AdemideInfoWebsite.Tests/
├── Services/
│   ├── AppointmentServiceTests.cs  - Tests for appointment operations
│   └── ProfileServiceTests.cs      - Tests for profile/authentication operations
└── AdemideInfoWebsite.Tests.csproj
```

## Running Tests

### From Visual Studio
1. Open Test Explorer (`Test > Test Explorer` or `Ctrl+E, T`)
2. Click "Run All" to execute all tests
3. View results in the Test Explorer window

### From Command Line
```powershell
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test class
dotnet test --filter "FullyQualifiedName~AppointmentServiceTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~CreateAppointmentAsync_WithValidData"
```

## Test Coverage

### AppointmentServiceTests
Tests for appointment management functionality:

#### CreateAppointmentAsync
- ✅ Creates appointment with valid data and sends email
- ✅ Returns failure for invalid profile ID
- ✅ Marks existing active appointment as completed when creating new one

#### GetAppointmentsByProfileIdAsync
- ✅ Returns paginated appointments with valid data
- ✅ Returns failure for invalid profile ID  
- ✅ Applies different sorting options (title, date, etc.)

#### MarkAppointmentAsCompletedAsync
- ✅ Marks appointment as completed with valid ID
- ✅ Returns failure for invalid appointment ID

### ProfileServiceTests  
Tests for user profile and authentication:

#### LoginAsync
- ✅ Returns success with valid credentials
- ✅ Returns unauthorized with invalid password
- ✅ Returns not found for non-existent email
- ✅ Returns bad request for empty credentials

#### RegisterAsync
- ✅ Creates profile and sends welcome email
- ✅ Returns conflict for existing email
- ✅ Returns bad request for missing fields

#### EditProfileAsync
- ✅ Updates profile and sends notification email
- ✅ Returns not found for invalid profile ID

#### UpdateProfileLanguageAsync
- ✅ Updates profile language
- ✅ Returns not found for invalid profile ID

#### ChangePasswordAsync
- ✅ Changes password with valid old password
- ✅ Returns unauthorized with invalid old password

#### SendPasswordResetEmailAsync
- ✅ Sends reset email for valid email
- ✅ Returns not found for invalid email

## Mocking Strategy

### IUnitOfWork
- Mocks repository access
- Tracks `SaveChangesAsync` calls

### IRepository<T>
- Mocks CRUD operations
- Supports query filtering and pagination

### IEmailService
- Verifies email sending
- Checks email parameters (recipient, subject, body)

### IOptions<AppSettings>
- Provides test configuration
- Includes contact email

## Test Patterns Used

### Arrange-Act-Assert (AAA)
```csharp
[Fact]
public async Task MethodName_Condition_ExpectedBehavior()
{
	// Arrange - Setup test data and mocks
	var input = CreateTestData();

	// Act - Execute the method under test
	var result = await _sut.MethodAsync(input);

	// Assert - Verify the results
	result.Status.Should().BeTrue();
}
```

### Theory Tests
Used for testing multiple scenarios:
```csharp
[Theory]
[InlineData("value1", true)]
[InlineData("value2", false)]
public async Task TestMethod(string input, bool expected)
{
	// Test logic
}
```

## Best Practices

1. **One Assert Per Test** - Each test verifies one specific behavior
2. **Descriptive Names** - Test names describe what they test
3. **Independent Tests** - Tests don't depend on each other
4. **Mock Verification** - Verify that dependencies are called correctly
5. **Fluent Assertions** - Use readable assertion syntax

## Common Issues & Solutions

### Issue: Tests fail to build
**Solution**: Ensure all package references are restored
```powershell
dotnet restore
```

### Issue: Mocks not working as expected
**Solution**: Verify setup matches the actual method signature
```csharp
_mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
	.ReturnsAsync(expectedResult);
```

### Issue: Async tests timing out
**Solution**: Ensure all async operations use `await`

## Adding New Tests

1. Create test class in `Services/` folder
2. Inherit test naming convention: `{ServiceName}Tests`
3. Follow AAA pattern
4. Use descriptive test names: `MethodName_Condition_ExpectedResult`
5. Mock all dependencies
6. Verify interactions with mocks

Example:
```csharp
[Fact]
public async Task CreateResource_WithValidData_ShouldReturnSuccess()
{
	// Arrange
	var resource = CreateTestResource();

	// Act
	var result = await _sut.CreateResourceAsync(resource);

	// Assert
	result.Status.Should().BeTrue();
	result.StatusCode.Should().Be(201);
	_mockRepository.Verify(x => x.AddAsync(resource), Times.Once);
}
```

## Continuous Integration

Tests can be integrated into CI/CD pipelines:

```yaml
# Example GitHub Actions
- name: Run Tests
  run: dotnet test --no-build --verbosity normal
```

```yaml
# Example Azure Pipelines
- task: DotNetCoreCLI@2
  inputs:
	command: 'test'
	projects: '**/*Tests.csproj'
```

## NuGet Packages Used

- **xUnit** (2.9.1+) - Testing framework
- **xUnit.runner.visualstudio** - Visual Studio test runner
- **Moq** (4.20.72+) - Mocking framework  
- **FluentAssertions** (8.10.0+) - Fluent assertion library
- **Microsoft.Extensions.Options** (10.0.9) - Options pattern support

## Future Enhancements

- [ ] Add integration tests
- [ ] Add test coverage reporting
- [ ] Add performance tests
- [ ] Add mutation testing
- [ ] Mock database for integration tests

---

**Total Tests**: 26+
**Test Success Rate**: Target 100%
**Code Coverage**: Target 80%+
