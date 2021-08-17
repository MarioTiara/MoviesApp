using MoviesApp.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesApp.Controllers
{
	public class CustomersController : Controller
	{
		private ApplicationDbContext _context;
		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		//
		// GET: /Customers/
		public ActionResult Index()
		{
			var customers = _context.Customer.Include(c=>c.MembershipType).ToList();
			return View(customers);
		}

		public ActionResult Details(int id)
		{
			var customer = _context.Customer.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
			if (customer == null)
				return HttpNotFound();
			return View(customer);
		}

	}
}