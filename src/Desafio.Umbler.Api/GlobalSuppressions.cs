// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Not applicable", Scope = "type", Target = "~T:Desafio.Umbler.Api.Program")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not applicable", Scope = "member", Target = "~M:Desafio.Umbler.Api.Controllers.BaseController.Response(Desafio.Umbler.Application.Common.Result)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "Not applicable", Scope = "member", Target = "~M:Desafio.Umbler.Api.Controllers.BaseController.Response(Desafio.Umbler.Application.Common.Result)~Microsoft.AspNetCore.Mvc.IActionResult")]
