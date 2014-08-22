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
    public class TasksController : ApiController
    {

       private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

       // GET api/tasks
       public HttpResponseMessage Get()   
       {
           List<serTask> tasks = new List<serTask>();
           DataManager.ConnectionString = connectionString;
           HttpResponseMessage response = null;
           try
           {
               IEnumerable<Task> tsks = DataManager.GetTasks();
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

       
        // GET api/tasks/5
       public HttpResponseMessage Get(int id)
        {
            DataManager.ConnectionString = connectionString;
            HttpResponseMessage response = null;
            try
            {
                    Task t = DataManager.GetTaskByID(id);

                    serTask tsk = new serTask();
                    tsk.ID = t.ID;
                    tsk.Created = t.Created;// == null ? DateTime.MinValue : (DateTime)t.Created;
                    tsk.Due = t.Due;// == null ? DateTime.MinValue : (DateTime)t.Due;
                    tsk.Completed = t.Completed;// == null ? DateTime.MinValue : (DateTime)t.Completed;
                    tsk.Description = t.Description;
                    tsk.IdeaID = (int)t.IdeaID;
                    tsk.Notes = t.Notes;
                    tsk.ProjectID = (int)t.ProjectID;

                response = Request.CreateResponse(HttpStatusCode.OK, tsk);
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

        // POST api/values
        public HttpResponseMessage Post([FromBody]serTask tsk)
        {
            DataManager.ConnectionString = connectionString;
            int id = -1;
            if (tsk != null)
            {
               Task t = new Task()
                {
                   Created = DateTime.Now,
                   Due = tsk.Due == DateTime.MinValue ? (DateTime?)null : tsk.Due,
                   Completed = tsk.Completed == DateTime.MinValue ? (DateTime?)null : tsk.Completed,
                   Description = tsk.Description,
                   IdeaID = tsk.IdeaID,
                   Notes = tsk.Notes,
                   ProjectID = tsk.ProjectID
                };

                id = DataManager.CreateTask(t);
            }

            if (id > -1)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]serTask tsk)
        {
            bool sucessful = false;
            DataManager.ConnectionString = connectionString;
            if (tsk != null)
            { 
               Task t = new Task()
                {
                    Created = tsk.Created == DateTime.MinValue ? (DateTime?)null : tsk.Created,
                    Due = tsk.Due == DateTime.MinValue ? (DateTime?)null : tsk.Due,
                    Completed = tsk.Completed == DateTime.MinValue ? (DateTime?)null : tsk.Completed,
                    Description = tsk.Description,
                    IdeaID = tsk.IdeaID,
                    Notes = tsk.Notes,
                    ProjectID = tsk.ProjectID
                };
               sucessful = DataManager.UpdateTask(t);
            }
            if (sucessful)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            bool sucessful = false;
            DataManager.ConnectionString = connectionString;
            sucessful = DataManager.DeleteTask(id);
            if (sucessful)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


    }
}
