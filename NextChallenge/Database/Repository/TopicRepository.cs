using NextChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace NextChallenge.Database.Repository {
    public class TopicRepository : ITopicRepository {
        private readonly NextChallengeEntities _database;
        public TopicRepository()
        {
            _database = new NextChallengeEntities();
        }
        [HttpGet]
        public IEnumerable<TopicOutput> GetTopics()
        {
            return _database.Topics.Where(x => x.IsActive == true).OrderBy(x => x.User.Username).Select(t => new TopicOutput
            {
                IdTopic = t.IdTopic,
                Username = t.User.Username,
                Title = t.Title,
                Body = t.Body,
                CreateDate = t.CreateDate,
                IdUser = t.IdUser
            }).ToList();
        }
        [HttpGet]
        public TopicOutput GetTopic(Guid? id)
        {
            return _database.Topics.Where(x => x.IdTopic == id).Select(t => new TopicOutput
            {
                IdTopic = t.IdTopic,
                Username = t.User.Username,
                Title = t.Title,
                Body = t.Body,
                CreateDate = t.CreateDate
            }).SingleOrDefault();
        }
        [HttpPost]
        public HttpStatusCode CreateTopic(Guid IdUser, CreateTopicInput input)
        {
            var newTopic = new Topic()
            {
                Title = input.Title,
                Body = input.Body,
                IdUser = IdUser
            };
            try
            {
                _database.Topics.Add(newTopic);
                _database.SaveChanges();
                return HttpStatusCode.OK;

            }
            catch (System.Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
        [HttpPut]
        public HttpStatusCode EditTopic(Guid IdUser, EditTopicInput input)
        {
            var editTopic = _database.Topics.SingleOrDefault(x => x.IdTopic == input.IdTopic);
            editTopic.Title = input.Title;
            editTopic.Body = input.Body;

            if (editTopic.IdUser != IdUser)
                return HttpStatusCode.Unauthorized;

            try
            {
                _database.SaveChanges();
                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }


        }

        public new void Dispose()
        {
            _database.Dispose();
        }

    }
}
