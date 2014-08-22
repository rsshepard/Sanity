using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Sanity.Data;

namespace Sanity.web.Controllers
{
    public class ProjectsController : ApiController
    {

       private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;


       public HttpResponseMessage Get()   
       {
           List<serProject> projects = new List<serProject>();
           DataManager.ConnectionString = connectionString;
           HttpResponseMessage response = null;
           try
           {
               IEnumerable<project> prjs = DataManager.GetProjects();
               foreach (project pr in prjs)
               {
                   serProject prj = new serProject();
                   prj.Created = pr.Created;// == null ? DateTime.MinValue : (DateTime)pr.Created;
                   prj.Description = pr.Description;
                   prj.ID = pr.ID;
                   prj.JobNumber = pr.JobNumber;
                   prj.Notes = pr.Notes;
                   projects.Add(prj);
               }

               response = Request.CreateResponse(HttpStatusCode.OK, projects);
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

       
        // GET api/values/5
       public HttpResponseMessage Get(int id)
        {
            DataManager.ConnectionString = connectionString;
            HttpResponseMessage response = null;
            try
            {
                    project pr = DataManager.GetProjectByID(id);

                    serProject prj = new serProject();
                    prj.Created = pr.Created;// == null ? DateTime.MinValue : (DateTime)pr.Created;
                    prj.Description = pr.Description;
                    prj.ID = pr.ID;
                    prj.JobNumber = pr.JobNumber;
                    prj.Notes = pr.Notes;

                response = Request.CreateResponse(HttpStatusCode.OK, prj);
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
        public HttpResponseMessage Post([FromBody]serProject prj)
        {
            DataManager.ConnectionString = connectionString;
            int id = -1;
            if (prj != null)
            {
                project project = new project()
                {
                    Description = prj.Description,
                    Notes = prj.Notes,
                    JobNumber = prj.JobNumber,
                    Created = DateTime.Now
                };

                id = DataManager.CreateProject(project);
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
        public HttpResponseMessage Put(int id, [FromBody]serProject prj)
        {
            bool sucessful = false;
            DataManager.ConnectionString = connectionString;
            if (prj != null)
            { 
                project project = new project()
                {
                    ID=prj.ID,
                    Description = prj.Description,
                    Notes = prj.Notes,
                    JobNumber = prj.JobNumber,
                    Created = prj.Created == DateTime.MinValue ? (DateTime?)null : prj.Created
                };
                sucessful = DataManager.UpdateProject(project);
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
            sucessful = DataManager.DeleteProject(id);
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
