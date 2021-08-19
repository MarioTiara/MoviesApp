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
		//call db from Identity model
		private ApplicationDbContext _context;

		//getter _context which is private
		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		//to Release Db form application DbContext
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		//method for New() Action
		public ActionResult New()
		{
			//Assign MembershipTypes Model from _context to Membership var in List format
			var MembershipTypes = _context.MembershipTypes.ToList();

			//Make Instance for CustomerFormViewModel
			var viewModel = new CustomerFormViewModel
			{
				//Customer Object as a Field For viewModel
                Customer=new Customer(),

				//Assign MembeshipTypes Model to MembeshipTypes field of CustomerFormViewModel
				MembershipTypes = MembershipTypes
			};
			return View("CustomerForm", viewModel);
		}


		[HttpPost] //define post URL
        [ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer)
		{
			//if ModelState is not valid
			//ModelState is relate with data anotation that we also use for validation
			// Read more about ModelState=> https://www.exceptionnotfound.net/asp-net-mvc-demystified-modelstate/
			if (!ModelState.IsValid)
			{
				//Then Save Action will still return "CustomerForm" and contain viewModel data which objek of CustomerFormViewModel
				var viewModel = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};

				return View("CustomerForm", viewModel);
			}

			//if Id of data in CustomerForm is 0
			if (customer.Id==0)
				//then data from customer that inherit from Customer model will be added to _context.Customer
				//in other hand, a new data will be added to db through ApplicationDbContext
				_context.Customer.Add(customer);
			else
			{
				//if Id isn't nol then data in DB (ApplicationDbContext)==Data in Customer Model
				//_context.Customer.Single(c => c.Id == customer.Id) => if _context.Customer.Id==customer.Id then Each propertie in _context.Customer. Id will be instance to customerInDb var
				var customerInDb = _context.Customer.Single(c => c.Id == customer.Id);
				customerInDb.Name = customer.Name;
				customerInDb.BirthDate = customer.BirthDate;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customer.IsSubscirbedToNewsLetter = customer.IsSubscirbedToNewsLetter;

			}

			//All Canges in _Context will be saved to Db
			_context.SaveChanges();

			//will redirect to Customers Controller with Index Action
			return RedirectToAction("Index", "Customers");
		}

		//Index Action
		public ActionResult Index()
		{
			//Instances _context.Customer Attributes including membershipType into customers var with list format
			var customers = _context.Customer.Include(c=>c.MembershipType).ToList();
			//return View with name name as Action name (Index) and contain customers var as data
			return View(customers);
		}

		//Details Action=> Provide Details data for each Customer
		public ActionResult Details(int id)
		{
			//if id in Url == id in _context.Customer then _context.Customer including MembershipTypes will be added to customer var
			var customer = _context.Customer.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
			
			//if customer var is null that means there is nothing match id with Id in _context.Customer
			if (customer == null)
				//then system will return HttpNotFound() page
				return HttpNotFound();
			
			//As default, this action will return a view which has name as this Action Name (Details) that contains customer var as  data
			return View(customer);
		}


		//Edit Action
		public ActionResult Edit(int id)
		{
			//if id in URL== id in _context.Customer then Single or Default attributes in _context.Customer will instances to customer var
			var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

			//if customer var is null that measn id in url is not match to any id in _context.Customer
			if (customer == null)
				return HttpNotFound();

			//instances CustomerFormViewModel Atributes to viewModel var
			var viewModel = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};
			return View("CustomerForm", viewModel);
		}

	}
}