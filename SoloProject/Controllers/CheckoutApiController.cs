using System;
using System.Collections.Generic;
using SoloProject.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CheckoutApiController : Controller
{
    private readonly MyContext _context;

    public CheckoutApiController(MyContext context)
    {
        _context = context;
    }

    public void PayOrder(int id)
    {
        var order = _context.Orders.FirstOrDefault(x => x.OrderId == id);
        order.Status = true;
        _context.SaveChanges();
    }
    public Order GetOrderDetails(int id)
    {
        return _context.Orders.Include(x => x.Products).ThenInclude(y => y.Product).FirstOrDefault(x => x.OrderId == id);
    }

    [Route("create-checkout-session")]
    [HttpPost]
    public ActionResult CreateCheckOut([FromBody] Order order)
    {
        var domain = "http://localhost:5000";
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long?)(100 * (order.TotalPrice)),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                    Name = "Order"
                },
                },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "/success",
            CancelUrl = domain + "/cancel",
        };
        var service = new SessionService();
        Session session = service.Create(options);
        PayOrder(order.OrderId);
        return Json(new { id = session.Id });
    }


    [HttpGet("hello/{id}")]
    public IActionResult OrderCheckout(int id)
    {
        var model = GetOrderDetails(id);
        return View("Checkout", model);
    }

    [Route("success")]
    public IActionResult Success()
    {
        return View();
    }

    [Route("cancel")]
    public IActionResult CancelOrder()
    {
        return View();
    }
}