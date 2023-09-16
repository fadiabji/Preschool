using Microsoft.AspNetCore.Mvc;
using Preschool.Services;

namespace Preschool.ViewComponents
{
    public class ChildSubscriptionsHistoryViewComponents : ViewComponent
    {
        private readonly ISubscriptionService _subscriptionService;

        public ChildSubscriptionsHistoryViewComponents(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        public IViewComponentResult Invoke(int id)
        {

            var childSubscriptions = _subscriptionService.GetSubscriptions().Result.Where(c => c.Id == id).ToList();
            return View("Index", childSubscriptions);

        }
    }
}
