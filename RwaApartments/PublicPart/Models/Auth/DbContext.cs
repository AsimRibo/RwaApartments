using RwaUtilities.DAL;
using RwaUtilities.Models.MVCUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicPart.Models.Auth
{
    public class DbContext : IDisposable
    {
        public IList<User> Users { get; set; }

        private DbContext(IList<User> users)
        {
            Users = users;
        }

        public static DbContext Create()
        {
            var users = RepositoryFactory.GetRepository().GetMvcUsers();

            return new DbContext(users);
        }

        public void Dispose()
        {
            
        }
    }
}