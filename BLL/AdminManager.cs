using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AdminManager : IAdminManager
    {
        private IAdminDB AdminDb { get; }
        public AdminManager(IAdminDB adminDb)
        {
            AdminDb = adminDb;
        }
        public int ResetDatabase()
        {
            return AdminDb.ResetDatabase();
        }
    }
}
