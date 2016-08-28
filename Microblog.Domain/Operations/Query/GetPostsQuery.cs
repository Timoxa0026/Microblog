namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetPostsQuery : QueryBase<List<GetPostsQuery.Response>>
    {
        #region Properties

        public string OwnerId { get; set; }

        #endregion

        #region Nested Classes

        public class Response
        {
            #region Properties

            public string Text { get; set; }

            public string CreateDate { get; set; }

            public string OwnerName { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new Post.Where.ByOwnerId() { OwnerId = this.OwnerId})
                             .Select(r => new Response
                                          {
                                              Text = r.Text,
                                              CreateDate = r.CreateDate.ToString(),
                                              OwnerName = r.Owner.FirstName + " " + r.Owner.LastName
                                          }).ToList();
        }
    }
}