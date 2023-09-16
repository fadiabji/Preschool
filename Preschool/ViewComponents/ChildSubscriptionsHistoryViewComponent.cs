using Microsoft.AspNetCore.Mvc;
using Preschool.Services;

namespace Preschool.ViewComponents
{
    public class ChildSubscriptionsHistoryViewComponent : ViewComponent
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IChildService _childService;

        public ChildSubscriptionsHistoryViewComponent(ISubscriptionService subscriptionService, IChildService childService)
        {
            _subscriptionService = subscriptionService;
            _childService = childService;

        }
        public IViewComponentResult Invoke(int id)
        {
            //var Id = int.Parse(id);
            //var childSubscriptionsHistory = _subscriptionService.GetSubscriptions().Result.Where(c => c.Id == Id).ToList();
            var childSubscriptionsHistory = _childService.GetChildById(id).Result.Subscriptions.ToList();
            return View("Index", childSubscriptionsHistory);

        }
    }
}
