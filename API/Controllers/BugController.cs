using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers;

public class BugController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]

    public ActionResult<string> GetAuth() 
    {
        return "smth";
    }

    [HttpGet("not-found")]

    public ActionResult<AppUser> GetNotFound()
    {
        var test = context.Users.Find(-1);

        if (test == null) return NotFound();

        return test;
    }

    [HttpGet("server-error")]

    public ActionResult<AppUser> GetServerError()
    {

        var smth = context.Users.Find(-1) ?? throw new Exception("Smth went wrong");
        return smth;          

    }

    [HttpGet("bad-request")]

    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("ErrorError");
    }

}