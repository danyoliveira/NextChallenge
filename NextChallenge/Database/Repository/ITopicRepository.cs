using NextChallenge.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace NextChallenge.Database.Repository {
    public interface ITopicRepository {
        IEnumerable<TopicOutput> GetTopics();
        TopicOutput GetTopic(Guid? id);
        HttpStatusCode CreateTopic(Guid IdUser, CreateTopicInput input);
        HttpStatusCode EditTopic(Guid IdUser, EditTopicInput input);
        void Dispose();
    }
}