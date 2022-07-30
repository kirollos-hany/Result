# Result

A result abstraction that is inspired by [Ardalis.Result](https://www.nuget.org/packages/Ardalis.Result/) that can be mapped to HTTP response codes if needed.

## Sample Usage

You can use the `ToActionResult` helper method from [Kiro.Result.AspNetCore](https://github.com/kirollos-hany/Result/tree/master/Kiro.Result.AspNetCore) within an endpoint to map the result to an action result type corresponding to the appropriate status code:

```csharp
[HttpGet("/resource")]
public override ActionResult Handle(ResourceRequestDto requestDto)
{
    return this.ToActionResult(_resourceService.GetResource(requestDto));

    // alternately
    // return _resourceService.GetResource(requestDto).ToActionResult(this);
}
```

So, what does the `_resourceService.GetResource` method look like? 

```csharp
public Result<FailureType, SuccessType> GetResource(ResourceRequestDto requestDto)
{
    //resource not found
    
    if (resource == null) return Result<FailureType, SuccessType>.NotFound();
    
    //incase a response body is required to be sent
    
    //if (resource == null) return Result<FailureType, SuccessType>.NotFound(failureType);
    
    //incase of some validation errors
    return Result<FailureType, SuccessType>.BadRequest(FailureType);
    
    //or for an empty bad request result
    //return Result<FailureType, SuccessType>.BadRequest();

    //case of success
    return new Result<FailureType, SuccessType>(successType);
    
    
}
```