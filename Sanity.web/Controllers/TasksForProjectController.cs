using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using Sanity.Data;

namespace Sanity.web.Controllers
{
    public class TasksForProjectController : ApiController
    {

       private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

       // GET api/tasks
       public HttpResponseMessage Get(int id)   
       {
           List<serTask> tasks = new List<serTask>();
           DataManager.ConnectionString = connectionString;
           HttpResponseMessage response = null;
           try
           {
               IEnumerable<Task> tsks = DataManager.GetTasksByProjectID(id);
               foreach (Task t in tsks)
               {
                   serTask tsk = new serTask();
                   tsk.ID = t.ID;
                   tsk.Created = t.Created;// == null ? DateTime.MinValue : (DateTime)t.Created;
                   tsk.Due = t.Due;// == null ? DateTime.MinValue : (DateTime)t.Due;
                   tsk.Completed = t.Completed;// == null ? DateTime.MinValue : (DateTime)t.Completed;
                   tsk.Description = t.Description;
                   tsk.IdeaID = (int)t.IdeaID;
                   tsk.Notes = t.Notes;
                   tsk.ProjectID = (int)t.ProjectID;

                   tasks.Add(tsk);
               }

               response = Request.CreateResponse(HttpStatusCode.OK, tasks);
               response.Headers.CacheControl = new CacheControlHeaderValue()
               {
                   MaxAge = TimeSpan.FromMinutes(20)
               };
           }
           catch (Exception ex)
           {
               response = Request.CreateResponse(HttpStatusCode.InternalServerError);
           }
           return response;

       }

    }
}
