using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Services;
using SQLitePCL;

namespace Preschool.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public SubscriptionsController(ISubscriptionService subscriptionService, ISubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionService = subscriptionService;
            _subscriptionTypeService = subscriptionTypeService;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            return View(await _subscriptionService.GetSubscriptions());
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Description");
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
               _subscriptionService.AddSubscription(subscription);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionTypeId"] = new SelectList(await Task.Run(()=>_subscriptionTypeService.GetSubscriptionTypes().Result), "Id", "Description", subscription.SubscriptionTypeId);
            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Description", subscription.SubscriptionTypeId);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(()=>_subscriptionService.UpdateSubscriptionRegistration(subscription));  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Description", subscription.SubscriptionTypeId);
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionById(id);
                if (subscription == null)
                {
                    return NotFound();
                }

                return View(subscription);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription != null)
            {
                await Task.Run(() => _subscriptionService.RemoveSubscription(subscription));
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
          return _subscriptionService.IsExists(id);
        }
    }
}
