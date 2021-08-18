using MoviesApp.Models;
using System.Data.Entity;
using MoviesApp.ViewModels;
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

		public ActionResult New()
		{
			var MembershipTypes = _context.MembershipTypes.ToList();
			var viewModel = new CustomerFormViewModel
			{
				MembershipTypes = MembershipTypes
			};
			return View("CustomerForm", viewModel);
		}

		[HttpPost]
		public ActionResult Save(Customer customer)
		{
			if (customer.Id==0)
				_context.Customer.Add(customer);
			else
			{
				var customerInDb = _context.Customer.Single(c => c.Id == customer.Id);
				customerInDb.Name = customer.Name;
				customerInDb.BirthDate = customer.BirthDate;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customer.IsSubscirbedToNewsLetter = customer.IsSubscirbedToNewsLetter;

			}
			_context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}

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


		public ActionResult Edit(int id)
		{
			var customer = _context.Customer.SingleOrDefault(c => c.Id == id);
			if (customer == null)
				return HttpNotFound();

			var viewModel = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};
			return View("CustomerForm", viewModel);
		}

	}
}