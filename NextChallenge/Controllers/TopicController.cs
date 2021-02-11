using NextChallenge.Database.Repository;
using NextChallenge.Helpers;
using NextChallenge.Models;
using System;
using System.Net;
using System.Web.Mvc;

namespace NextChallenge.Controllers {
    public class TopicController : Controller {
        private TopicRepository _topicRepository;

        public TopicController()
        {
            _topicRepository = new TopicRepository();
        }

        public ActionResult Index()
        {
            if (Security.Permission((Guid)Session["IdUser"]) == HttpStatusCode.Forbidden)
                return RedirectToAction("Login", "AccountManage");

            return View(_topicRepository.GetTopics());
        }

        public ActionResult Create()
        {
            if (Security.Permission((Guid)Session["IdUser"]) == HttpStatusCode.Forbidden)
                return RedirectToAction("Login", "AccountManage");

            return View();
        }
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            return View(_topicRepository.GetTopic(id));
        }
        [HttpPost]
        public ActionResult Create(CreateTopicInput input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CreateError", "Please Write Title and Description");
                return View();
            }

            switch (_topicRepository.CreateTopic((Guid)Session["IdUser"], input))
            {
                case HttpStatusCode.InternalServerError:
                    ModelState.AddModelError("CreateError", "Try Again.");
                    return View();
                default: return RedirectToAction("Index", "Topic");

            }
        }
        [HttpPost]
        public ActionResult Edit(EditTopicInput input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("EditError", "Please Write Title and Description");
                return View();
            }
            switch (_topicRepository.EditTopic((Guid)Session["IdUser"], input))
            {
                case HttpStatusCode.InternalServerError:
                    ModelState.AddModelError("EditError", "Your Password or User is wrong. Please Try again.");
                    return View(_topicRepository.GetTopic(input.IdTopic));
                case HttpStatusCode.Unauthorized:
                    ModelState.AddModelError("EditError", "You don't have permission to edit");
                    return View(_topicRepository.GetTopic(input.IdTopic));
                default: return RedirectToAction("Index", "Topic");
            }
        }
    }
}