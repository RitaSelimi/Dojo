using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoloProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SoloProject.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private MyContext _context;

        public OrderController(MyContext context)
        {
            _context = context;
        }

        private RegisterUser userIn
        {
            get
            {
                return _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
            }
        }

        [HttpGet("allorders")]
        public IActionResult AllOrders()
        {
            if (userIn == null)
            {
                return RedirectToAction("Login", "Access");
            }

            var orders = _context.Orders.OrderByDescending(x => x.CreatedAt).Take(10).ToList();
            var model = new OrderModel
            {
                Order = _context.Orders.FirstOrDefault(),
                ListOrders = orders,
            };
            @ViewBag.User = userIn.UserId;
            return View(model);
        }

        [HttpPost()]
        public IActionResult CreateOrder(Order o)
        {
            Order order = new Order()
            {
                CustomerId = userIn.UserId,
                TotalPrice = 0,
                Status = false,
                Delivery = false,
                CreatedAt = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("AllOrders");
        }

        [HttpGet("orderdetails/{orderId}")]
        public IActionResult Show(int orderId)
        {
            if (userIn == null)
                return RedirectToAction("Login", "Access");

            var viewModel = _context.Orders
                .Include(w => w.Products)
                .ThenInclude(r => r.Product)
                .FirstOrDefault(w => w.OrderId == orderId);

            var wedding = _context.Products.Include(w => w.Orders).Include(x => x.Users)
                .ToList();

            ViewBag.User = userIn.UserId;

            var model = new OrderModel()
            {
                ListProducts = wedding,
                Order = viewModel
            };
            return View(model);

        }

        [HttpGet("add/{productId}/{orderId}")]
        public IActionResult AddToCart(int productId, int orderId)
        {
            if (userIn == null)
            {
                return RedirectToAction("Login", "Access");
            }
            OrderProduct newProduct = new OrderProduct()
            {
                ProductId = productId,
                OrderId = orderId
            };

            _context.OrderProducts.Add(newProduct);
            _context.SaveChanges();
            Order toUpdate = _context.Orders.FirstOrDefault(d => d.OrderId == orderId);
            var products = _context.OrderProducts.Include(p => p.Order).Include(o => o.Product).Where(p => p.OrderId == orderId).ToList();
            double totalPrice = 0;
            foreach (var item in products)
            {
                totalPrice += item.Product.Price;
            }
            if (ModelState.IsValid)
            {
                toUpdate.TotalPrice = totalPrice;
                _context.SaveChanges();
                return RedirectToAction("Show");
            }

            return RedirectToAction("Show");
        }

        [HttpPost("updatePrice/{orderId}")]
        public IActionResult UpdatePrice(int orderId, Order order)
        {
            if (userIn == null)
            {
                return RedirectToAction("Login", "Access");
            }

            Order toUpdate = _context.Orders.FirstOrDefault(d => d.OrderId == orderId);
            var products = _context.OrderProducts.Include(p => p.Order).Include(o => o.Product).Where(p => p.OrderId == orderId).ToList();
            double totalPrice = 0;
            if (order.Delivery == true)
            {
                totalPrice += 2;
            }
            foreach (var item in products)
            {
                totalPrice += item.Product.Price;
            }
            // double tax = totalPrice * 0.16;
            // totalPrice += tax;
            if (ModelState.IsValid)
            {
                toUpdate.Delivery = order.Delivery;
                toUpdate.TotalPrice = totalPrice;
                _context.SaveChanges();
                return RedirectToAction("Show");
            }
            return View("Show");
        }

        [HttpGet("delete/{productId}/{orderId}")]
        public IActionResult Delete(int productId, int orderId)
        {
            OrderProduct toDelete = _context.OrderProducts.Include(x => x.Product).Include(y => y.Order).Where(x => x.OrderId == orderId && x.ProductId == productId).FirstOrDefault(x => x.ProductId == productId);
            if (toDelete == null)
                return RedirectToAction("Show");
            _context.OrderProducts.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Show");
        }

        [HttpGet("favorite/{productId}/{orderId}/{status}")]
        public IActionResult Favorite(int productId, string status, int orderId)
        {
            if (userIn == null)
                return RedirectToAction("Login", "Access");

            if (!_context.Products.Any(w => w.ProductId == productId))
                return RedirectToAction("Show", "Order");

            if (status == "add")
                AddRSVP(productId);
            else
                RemoveRSVP(productId);

            return RedirectToAction("Show", "Order");
        }
        private void AddRSVP(int productId)
        {
            ProductUser newFavorite = new ProductUser()
            {
                ProductId = productId,
                UserId = userIn.UserId
            };

            _context.ProductUsers.Add(newFavorite);
            _context.SaveChanges();
        }
        private void RemoveRSVP(int productId)
        {
            ProductUser rsvp = _context.ProductUsers.FirstOrDefault(r => r.UserId == userIn.UserId && r.ProductId == productId);

            if (rsvp != null)
            {
                _context.ProductUsers.Remove(rsvp);
                _context.SaveChanges();
            }
        }


        // [HttpGet("favoriteProduct/{productId}/{orderId}")]
        // public IActionResult Favorite(int productId, int orderId)
        // {
        //     if(userIn == null){
        //         return RedirectToAction("Login", "Access");
        //     }
        //     ProductUser newFavorite = new ProductUser()
        //     {
        //         ProductId = productId,
        //         UserId = userIn.UserId
        //     };
        //     _context.ProductUsers.Add(newFavorite);
        //     _context.SaveChanges();
        //     return RedirectToAction("Show");
        // }
    }
}