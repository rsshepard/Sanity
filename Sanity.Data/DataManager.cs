using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sanity.Data
{
   
    public static class DataManager
    {

         public static string ConnectionString;

        #region Project

        public static IEnumerable<project> GetProjects()
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from P in context.projects
                select P;

            return qry;
        }

        public static project GetProjectByID(int id)
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from P in context.projects
                      where P.ID == id
                      select P;

            return qry.FirstOrDefault();

        }

        public static int CreateProject(project prj)
        {
            int id = -1;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            try
            {
                context.projects.InsertOnSubmit(prj);
                context.SubmitChanges();
                id = prj.ID;
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public static bool UpdateProject(project prj)
        {
            bool sucessful = false;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            
            var qry = from P in context.projects
                      where P.ID == prj.ID
                      select P;

            project p = qry.FirstOrDefault();
            if(p!=null)
            {
                p.Created = prj.Created;
                p.Description = prj.Description;
                p.JobNumber = prj.JobNumber;
                p.Notes = prj.Notes;

                context.SubmitChanges();
                sucessful = true;
            }
          
            return sucessful;
            
        }

        public static bool DeleteProject(int id)
        {
            bool sucessful = false;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from P in context.projects
                      where P.ID == id
                      select P;

            project p = qry.FirstOrDefault();
            if (p != null)
            {

                context.projects.DeleteOnSubmit(p);
                context.SubmitChanges();
                sucessful = true;
            }

            return sucessful;

        }
        #endregion

        #region Tasks

        public static IEnumerable<Task> GetTasks()
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from T in context.Tasks
                      select T;

            return qry;
        }

        public static Task GetTaskByID(int id)
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from T in context.Tasks
                      where T.ID == id
                      select T;

            return qry.FirstOrDefault();

        }

        public static IEnumerable<Task> GetTasksByProjectID(int pid)
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from T in context.Tasks
                      where T.ProjectID == pid
                      select T;

            return qry;

        }
        public static IEnumerable<Task> GetTasksByIdeaID(int iid)
        {
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from T in context.Tasks
                      where T.IdeaID == iid
                      select T;

            return qry;

        }
        public static int CreateTask(Task tsk)
        {
            int id = -1;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            try
            {
                context.Tasks.InsertOnSubmit(tsk);
                context.SubmitChanges();
                id = tsk.ID;
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public static bool UpdateTask(Task tsk)
        {
            bool sucessful = false;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);

            var qry = from T in context.Tasks
                      where T.ID == tsk.ID
                      select T;

           Task t = qry.FirstOrDefault();
            if (t!= null)
            {
                t.Completed = tsk.Completed;
                t.Created = tsk.Created;
                t.Description = tsk.Description;
                t.Due = tsk.Due;
                t.IdeaID = tsk.IdeaID;
                t.Notes = tsk.Notes;
                t.ProjectID = tsk.ProjectID;
               
                context.SubmitChanges();
                sucessful = true;
            }

            return sucessful;

        }

        public static bool DeleteTask(int id)
        {
            bool sucessful = false;
            SanityDataModelDataContext context = new SanityDataModelDataContext(ConnectionString);
            var qry = from T in context.Tasks
                      where T.ID == id
                      select T;

            Task t = qry.FirstOrDefault();
            if (t != null)
            {

                context.Tasks.DeleteOnSubmit(t);
                context.SubmitChanges();
                sucessful = true;
            }

            return sucessful;

        }

        #endregion
    }
}
