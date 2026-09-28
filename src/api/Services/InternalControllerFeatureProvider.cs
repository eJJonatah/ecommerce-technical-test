using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace TEcomerc.Api.Services;


sealed class InternalControllerFeatureProvider
    : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
		return (typeInfo.IsClass &&
			!typeInfo.IsAbstract &&
			typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)) || base.IsController(typeInfo);
	}
}