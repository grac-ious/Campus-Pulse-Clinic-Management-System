namespace Campus_pulse
{
    public class SystemUser
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
    }
}