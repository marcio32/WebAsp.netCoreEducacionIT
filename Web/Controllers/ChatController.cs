using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Filters;
using Web.Models;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }

        public IActionResult Chat(int idChat)
        {
            return View(idChat);
        }
    }
}