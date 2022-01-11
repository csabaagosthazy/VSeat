using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class UserVM
    {
        [DisplayName("User id")]
        public int UserId { get; set; }
        [DisplayName("User role")]
        public string UserRole{ get; set; }
    }
}
