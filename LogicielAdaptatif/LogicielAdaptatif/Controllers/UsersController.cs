using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LogicielAdaptatif.Models;
using LogicielAdaptatif.ViewModels;

namespace LogicielAdaptatif.Controllers
{
    public class UsersController : ApiController
    {
        private readonly YNOVEntities db = new YNOVEntities();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // GET: api/login/Users
        [Route("api/user/{login}/{mdp}")]
        public IHttpActionResult Get(string login, string mdp)
        {
            var errors = new List<string>();
            // Comparer les logins et mdp
            var res = db.Users.Where(u => u.user_mail == login && u.user_password == mdp).Select(e => new UserVM { IdUser = e.id_user, PrenomUser = e.user_first_name, NomUser = e.user_last_name, Role = e.user_role }) ;
            try
            {
                // Verifier si login et password sont bons
                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        //creation user
        [Route("api/user/create")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            try
            {
                //erreurs spé en cas de problèmes
                var errors = new List<string>();
                if (user.user_password == null) errors.Add("Aucun mot de passe renseigné");
                if (user.user_mail == null) errors.Add("Aucun login/email renseigné");
                if (user.user_first_name == null) errors.Add("Aucun prénom renseigné");
                if (user.user_last_name == null) errors.Add("Aucun nom renseigné");
                if (errors.Count() < 1)
                {
                    db.Users.Add(user);
                    db.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    return Ok(errors);
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        //pour supprimer user
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.id_user == id) > 0;
        }
    }
}