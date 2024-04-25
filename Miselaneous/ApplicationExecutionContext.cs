using Demo.Domain;

namespace Demo.Miselaneous
{
    public class ApplicationExecutionContext
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public UserType UserType { get; set; }
    }
}
