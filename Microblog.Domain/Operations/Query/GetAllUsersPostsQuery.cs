namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Collections.Generic;
    using Incoding.CQRS;
    using System.Linq;

    #endregion
    public class GetAllUsersPostsQuery : QueryBase<List<GetAllUsersPostsQuery.Response>>
    {
        #region Nested Classes

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Text { get; set; }

            public string CreateDate { get; set; }

            public string OwnerName { get; set; }

            public string OwnerId { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new Post.Where.All())
                             .Select(r => new Response
                             {
                                 Id = r.Id,
                                 Text = r.Text,
                                 CreateDate = r.CreateDate.ToString(),
                                 OwnerName = r.Owner.FirstName + " " + r.Owner.LastName,
                                 OwnerId = r.Owner.Id
                             }).ToList();
        }
    }
}
